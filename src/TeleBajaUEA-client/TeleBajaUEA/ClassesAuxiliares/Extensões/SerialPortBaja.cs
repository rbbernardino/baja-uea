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
        public uint TotalReceivedBytes { get { return byteCount; } }

        // tempo em milisegundos até "NextByte()" lance uma exceção se não receber dados
        private readonly double NEXT_BYTE_TIMEOUT = 15000;
        private System.Timers.Timer timeoutTimer;
        private bool timeoutExceeded;

        #region XBee settings and state
        private readonly int XBEE_CMD_DELAY = 100; // valor do param GT do XBee
        private readonly char XBEE_LINE_END = '\r';
        private readonly string XBEE_CAR_ID = "XBEE_CARRO";
        #endregion

        #region Protocol
        //private static SemaphoreSlim canReceiveDataSignal;
        private readonly string XB_READY = "READY"; // ENVIA: checa se XBee pode iniciar envio
        private readonly string XB_START = "START"; // ENVIA: pede por inicio do envio de dados
        private readonly string XB_BEGIN = "B"; // RECEBE: inicio de pacote
        private readonly string XB_END = "E"; // RECEBE: fim de pacote

        private enum SerialMsg
        {
            // recebe da Serial
            CONNECT = 'C',
            READY = 'R',
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
        private uint byteCount = 0;
        #endregion

        public SerialPortBaja() : base()
        {
            NewLine = "" + XBEE_LINE_END;
            DataReceived += port_DataReceived;
        }

        public SerialPortBaja(string pPortName) : base()
        {
            NewLine = "" + XBEE_LINE_END;
            PortName = pPortName;
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
        #endregion

        #region métodos públicos {...}
        // TODO colocar essa função no CarConnection
        // tenta trocar mensagens com o XBee do carro para checar conexão
        // e sincronizar estados
        public async Task<bool> TryHandshake()
        {
            string rcvMsg;
            if (await ATConnectedToCar())
            {
                WriteLine(XB_READY);

                StartTimeoutTimer();
                rcvMsg = await XBReadLine();
                StopTimeoutTimer();

                if (rcvMsg.Equals("OK"))
                    return true;
                else
                {
                    string errorMsg =
                        "Protocolo incorreto (Handshake). Esperava 'OK', " + 
                        "mas recebeu '" + rcvMsg + "'";
                    throw new Exception(errorMsg);
                }
            }
            else
            {
                string errorMsg =
                    "Sinal não encontrado, verifique o dispositivo no carro " +
                    "e tente novamente";
                throw new Exception(errorMsg);
            }
        }

        public void StartReceiveData()
        {
            // avisa arduino no carro que pode iniciar envio de mensagens de dados
            WriteLine(XB_START);
        }

        // TODO colocar essa função no CarConnection
        // TODO implementar GetNextPacket (total de 10 bytes excluindo o 'B'
        // pode gerar a exceção NextByteTimeoutException
        // lê os dados, armazena em variáveis temporárias e gera um novo objeto SensorsData (newData)
        public async Task<SensorsData> GetNextPacket()
        {
            bool rcvOK = false;
            string rcvMsg = await XBReadLine();

            // flag BEGIN/END usadas para garantir que dados estarão sincronizados
            if (rcvMsg.Equals(XB_BEGIN))
            {
                tmpMillis = await NextUInt32();
                tmpBreakState = await NextChar();
                tmpTemperature = await NextInt16();
                tmpRpm = await NextInt16();
                tmpSpeed = await NextInt8();

                rcvMsg = await XBReadLine();
                if (rcvMsg.Equals(XB_END))
                    rcvOK = true;
            }

            if (rcvOK)
                return new SensorsData(tmpMillis, tmpSpeed, tmpTemperature,
                                        tmpRpm, tmpBreakState);
            else
                throw new Exception("Protocolo incorreto. Esperava BEGIN('B'), " +
                    "mas recebeu '" + rcvMsg);
        }

        public async Task<bool> ATConnectedToCar()
        {
            string rcv_msg = "";

            // entrar no command mode
            await ATcmd();

            // verifica conexao
            WriteLine("ATDN" + XBEE_CAR_ID);
            rcv_msg = await XBReadLine();

            if (rcv_msg.Equals("OK"))
                return true;
            else if (rcv_msg.Equals("ERROR"))
                return false;
            else
            {
                string errorMsg =
                    "Resposta do comando ATDN inesperada: \"" + rcv_msg + "\"";

                throw (new Exception(errorMsg));
            }
        }

        #endregion

        // TODO considerar acrescentar um timeout quando esperando pelo OK
        private async Task ATcmd()
        {
            await Task.Delay(XBEE_CMD_DELAY);
            Write("+++");

            // quando entrar no modo de comando do XBee durante o recebimento de
            // dados, é preciso descartar os dados que estão na fila até a chegada do OK
            string readLine = await XBReadLine();
            while (!readLine.Equals("OK"))
                readLine = await XBReadLine();
        }

        // lê linha a partir do buffer ConcurrentQueue
        private async Task<string> XBReadLine()
        {
            string line = "";

            char readChar = await NextChar();
            while (readChar != XBEE_LINE_END)
            {
                line += readChar;
                readChar = await NextChar();
            }
            return line;
        }

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
