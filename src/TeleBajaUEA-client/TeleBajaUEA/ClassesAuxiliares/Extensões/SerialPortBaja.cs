using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA.ClassesAuxiliares
{
    class SerialPortBaja : SerialPort
    {
        // tempo em milisegundos até "NextByte()" lance uma exceção se não receber dados
        private readonly double NEXT_BYTE_TIMEOUT = 5000;
        private System.Timers.Timer timeoutTimer;
        private bool timeoutExceeded;

        #region Protocol
        //private static SemaphoreSlim canReceiveDataSignal;

        private enum SerialMsg
        {
            // recebe da Serial
            CONNECT = 'C',
            READY = 'R',
            START = 'S',
            BEGIN = 'B',
            //END = 'E',

            // envia para Serial
            OK = 'K',
        }
        #endregion

        #region Temporary Sensor Measurements
        // variáveis temporárias dos dados de um pacote que está sendo recebido
        private static uint tmpMillis; // tempo da medicao em milisegundos
        private static float tmpSpeed;
        private static float tmpTemperature;
        private static float tmpRpm;
        private static char tmpBreakState;
        private void ResetTmpVars()
        {
            tmpSpeed = tmpTemperature = tmpRpm = -1;
            tmpBreakState = 'N'; // (N)ull
        }
        #endregion

        #region Temporary received data buffer / Queue
        private byte[] intBuffer = new byte[4];
        private ConcurrentQueue<byte> receivedDataQueue = new ConcurrentQueue<byte>();
        #endregion

        public SerialPortBaja() : base()
        {
            DataReceived += port_DataReceived;
        }

        public SerialPortBaja(string pPortName) : base()
        {
            PortName = pPortName;
        }

        #region eventos (event handlers) {...}
        // acumula dados recebidos na porta serial em um buffer/fila para serem lidos depois
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[BytesToRead];
            Read(data, 0, data.Length);
            data.ToList().ForEach(b => receivedDataQueue.Enqueue(b));
        }
        #endregion

        #region métodos públicos {...}
        // TODO colocar essa função no CarConnection
        // tenta trocar mensagens com o XBee do carro para checar conexão
        // e sincronizar estados
        public async Task TryHandshake()
        {
            // começa a ouvir a porta e guardar os dados recebido na queue
            DataReceived += port_DataReceived;

            char msg = await NextChar();
            if (msg == (char)SerialMsg.CONNECT)
            {
                WriteChar((char)SerialMsg.OK);

                // TODO colocar timeout...
                // ignora todas as mensagens até receber (R)EADY (talvez haja vários (C)onnect)
                StartTimeoutTimer();
                msg = await NextChar();
                while (msg != (char)SerialMsg.READY)
                {
                    if (timeoutExceeded)
                        throw new ErrorMessage.ReceiveDataTimeoutException();
                    else
                    {
                        if (msg != (char)SerialMsg.CONNECT)
                        {
                            string errorMsg =
                                "Erro de protocolo durante Handshake. Esperava '" +
                                (char)SerialMsg.CONNECT + "', mas recebeu '" + msg + "'";
                            throw new Exception(errorMsg);
                        }

                        msg = await NextChar();
                    }
                }
                StopTimeoutTimer();
            }
            else
            {
                string errorMsg =
                    "Erro de protocolo durante Handshake. Esperava '" +
                    (char)SerialMsg.CONNECT + "', mas recebeu '" + msg + "'";

                throw new Exception(errorMsg);
            }
        }

        public void StartReceiveData()
        {
            // avisa arduino no carro que pode iniciar envio de mensagens de dados
            WriteChar((char)SerialMsg.START);
        }

        // TODO colocar essa função no CarConnection
        // TODO implementar GetNextPacket (total de 10 bytes excluindo o 'B'
        // pode gerar a exceção NextByteTimeoutException
        // lê os dados, armazena em variáveis temporárias e gera um novo objeto SensorsData (newData)
        public async Task<SensorsData> GetNextPacket()
        {
            char msg = await NextChar();

            // flag BEGIN usada para garantir que dados estarão sincronizados
            if (msg == (char)SerialMsg.BEGIN)
            {
                tmpMillis = await NextUInt32();
                tmpBreakState = await NextChar();
                tmpTemperature = await NextInt16();
                tmpRpm = await NextInt16();
                tmpSpeed = await NextInt8();

                return new SensorsData(tmpMillis, tmpSpeed, tmpTemperature,
                                        tmpRpm, tmpBreakState);
            }
            else
            {
                // TODO tratar melhor esse erro
                //MessageBox.Show("ERRO! Esperava BEGIN ('B'), mas recebeu '" + msg + "'");
                throw new Exception("Esperava BEGIN('B'), mas recebeu '" + msg + "'. Bytes lidos: " + readbytes);
                //return new SensorsData();
            }
        }
        #endregion
        private uint readbytes = 0;
        // pode gerar a exceção NextByteTimeoutException
        private async Task<byte> NextByte()
        {
            StartTimeoutTimer();

            byte rcvByte;
            while (!receivedDataQueue.TryDequeue(out rcvByte))
            {
                // quando o Timer disparar, será automaticamente desativado (AutoReset = false)
                if (timeoutExceeded)
                    throw new ErrorMessage.ReceiveDataTimeoutException();
                else
                    await Task.Delay(300);
            }

            // quando sair do while, sucesso, logo desativa o timer
            StopTimeoutTimer();

            readbytes++; // TODO organizar isso

            return rcvByte;
        }

        // TODO acrescentar timeout -> retorna falso ou throw exceção
        private async Task<char> NextChar()
        {
            return (char) (await NextByte());
        }

        private async Task<uint> NextUInt32()
        {
            intBuffer[0] = intBuffer[1] = intBuffer[2] = intBuffer[3] = 0;

            intBuffer[0] = await NextByte();
            intBuffer[1] = await NextByte();
            intBuffer[2] = await NextByte();
            intBuffer[3] = await NextByte();

            return BitConverter.ToUInt32(intBuffer, 0);
        }

        private async Task<int> NextInt16()
        {
            intBuffer[0] = intBuffer[1] = intBuffer[2] = intBuffer[3] = 0;

            intBuffer[0] = await NextByte(); // ler o lowByte, a direita
            intBuffer[1] = await NextByte(); // ler o highByte, a esquerda

            return BitConverter.ToInt16(intBuffer, 0);
        }

        private async Task<int> NextInt8()
        {
            return await NextByte();
        }

        private void WriteChar(char c)
        {
            Write(c.ToString());
        }

        private void StartTimeoutTimer()
        {
            timeoutExceeded = false;

            timeoutTimer = new System.Timers.Timer();
            timeoutTimer.Interval = NEXT_BYTE_TIMEOUT;
            timeoutTimer.Elapsed += TimeoutTimer_Elapsed;
            timeoutTimer.AutoReset = false;
            timeoutTimer.Enabled = true;
            timeoutTimer.Start();
        }

        // TODO verificar se a info abaixo procede ou porque dá erro durante execução
        // Aparentemente com o AutoReset = false ele automaticamente faz o
        // Dispose após disparar
        private void StopTimeoutTimer()
        {
            if(timeoutTimer != null)
            {
                timeoutTimer.Enabled = false;
                timeoutTimer.Stop();
                timeoutTimer.Dispose();
                timeoutTimer = null;
            }
        }

        private void TimeoutTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeoutExceeded = true;
            StopTimeoutTimer();
        }
    }
}
