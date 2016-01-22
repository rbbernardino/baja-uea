using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA.ClassesAuxiliares
{
    class SerialPortExt : SerialPort
    {
        public uint TotalReceivedBytes { get { return byteCount; } }

        // tempo em milisegundos até "NextByte()" lance uma exceção se não receber dados
        private readonly double NEXT_BYTE_TIMEOUT = 15000;
        private System.Timers.Timer timeoutRcvTimer;
        private bool timeoutRcvExceeded;

        #region Temporary received data buffer / Queue
        private byte[] intBuffer = new byte[4];
        private ConcurrentQueue<byte> receivedDataQueue = new ConcurrentQueue<byte>();
        private uint byteCount = 0;
        #endregion

        public SerialPortExt() : base()
        {
            DataReceived += port_DataReceived;
        }

        #region eventos (event handlers) {...}
        // acumula dados recebidos na porta serial em um buffer/fila para serem lidos depois
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[BytesToRead];
            Read(data, 0, data.Length);
            data.ToList().ForEach(b => { receivedDataQueue.Enqueue(b); byteCount++; } );
        }

        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeoutRcvExceeded = true;
            StopTimeoutTimer();
        }
        #endregion

        // lê linha a partir do buffer ConcurrentQueue
        // considera um char como "NewLine", ao invés de uma string
        public async Task<string> ReadLineExt()
        {
            string line = "";

            char readChar = await NextChar();
            while (readChar != NewLine[0])
            {
                line += readChar;
                readChar = await NextChar();
            }
            return line;
        }

        // lê byte da ConcurrentQueue
        public async Task<byte> NextByte()
        {
            StartTimeoutTimer();

            byte rcvByte;
            while (!receivedDataQueue.TryDequeue(out rcvByte))
            {
                if (timeoutRcvExceeded)
                    throw new ErrorMessage.ReceiveDataTimeoutException();
                else
                    await Task.Delay(300);
            }

            // quando sair do while, sucesso, logo desativa o timer
            StopTimeoutTimer();

            return rcvByte;
        }

        public async Task<char> NextChar()
        {
            return (char) (await NextByte());
        }

        public async Task<uint> NextUInt32()
        {
            intBuffer[0] = intBuffer[1] = intBuffer[2] = intBuffer[3] = 0;

            intBuffer[0] = await NextByte();
            intBuffer[1] = await NextByte();
            intBuffer[2] = await NextByte();
            intBuffer[3] = await NextByte();

            return BitConverter.ToUInt32(intBuffer, 0);
        }

        public async Task<int> NextInt16()
        {
            intBuffer[0] = intBuffer[1] = intBuffer[2] = intBuffer[3] = 0;

            intBuffer[0] = await NextByte(); // ler o lowByte, a direita
            intBuffer[1] = await NextByte(); // ler o highByte, a esquerda

            return BitConverter.ToInt16(intBuffer, 0);
        }

        public async Task<int> NextInt8()
        {
            return await NextByte();
        }

        public void WriteChar(char c)
        {
            Write(c.ToString());
        }

        // TODO verificar se é melhor reutilizar um mesmo timer
        // ou instanciar um novo sempre não impacta muito na performance
        public void StartTimeoutTimer()
        {
            timeoutRcvExceeded = false;

            timeoutRcvTimer = new System.Timers.Timer();
            timeoutRcvTimer.Interval = NEXT_BYTE_TIMEOUT;
            timeoutRcvTimer.Elapsed += TimeoutTimer_Elapsed;
            timeoutRcvTimer.AutoReset = false;
            timeoutRcvTimer.Enabled = true;
            timeoutRcvTimer.Start();
        }

        private void StopTimeoutTimer()
        {
            if(timeoutRcvTimer != null)
            {
                timeoutRcvTimer.Enabled = false;
                timeoutRcvTimer.Stop();
                timeoutRcvTimer.Dispose();
                timeoutRcvTimer = null;
            }
        }
    }
}
