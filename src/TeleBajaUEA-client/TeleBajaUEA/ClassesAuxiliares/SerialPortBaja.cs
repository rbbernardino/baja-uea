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
        // TODO criar timeout nos TryDequeue
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
        private static float tmpFuel;
        private static char tmpBreakState;
        private void ResetTmpVars()
        {
            tmpSpeed = tmpTemperature = tmpRpm = tmpFuel = -1;
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

        // acumula dados recebidos na porta serial em um buffer/fila para serem lidos depois
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[BytesToRead];
            Read(data, 0, data.Length);
            data.ToList().ForEach(b => receivedDataQueue.Enqueue(b));
        }

        // tenta trocar mensagens com o XBee do carro para checar conexão
        // e sincronizar estados
        public async Task<bool> TryHandshake()
        {
            // começa a ouvir a porta e guardar os dados recebido na queue
            DataReceived += port_DataReceived;

            // trocar por TaskCompletionSource?? (http://stackoverflow.com/a/12858633)
            //canReceiveDataSignal = new SemaphoreSlim(0, 1);
            //await canReceiveDataSignal.WaitAsync();

            // quando sair do await é porque conectou ou deu falha...
            char msg = await NextChar();
            if (msg == (char)SerialMsg.CONNECT)
            {
                WriteChar((char)SerialMsg.OK);

                // TODO colocar timeout...
                // ignora todas as mensagens até receber READY (haverá vários (C)onnect)
                msg = await NextChar();
                while (msg != (char)SerialMsg.READY)
                    msg = await NextChar();

                return true;
            }
            else
                return false; // TODO reportar ERRO: a 1a msg deve ser 'S' ou algo está errado
        }

        public void StartReceiveData()
        {
            WriteChar((char)SerialMsg.START);
        }

        // TODO implementar GetNextPacket (total de 10 bytes excluindo o 'B'
        // lê os dados, armazena em variáveis temporárias e gera um novo objeto SensorsData (newData)
        public async Task<SensorsData> GetNextPacket()
        {
            char msg = await NextChar();

            // flag BEGIN usada para garantir que dados estarão sincronizados
            if (msg == (char)SerialMsg.BEGIN)
            {
                tmpMillis = await NextUInt32();
                tmpBreakState = await NextChar();
                tmpFuel = await NextInt8();
                tmpTemperature = await NextInt16();
                tmpRpm = await NextInt16();

                return new SensorsData(tmpMillis, tmpSpeed, tmpTemperature,
                                        tmpRpm, tmpFuel, tmpBreakState);
            }
            else
            {
                // TODO tratar melhor esse erro
                MessageBox.Show("ERRO! Esperava BEGIN ('B'), mas recebeu '" + msg + "'");
                return new SensorsData();
            }
        }

        private async Task<byte> NextByte()
        {
            byte rcvByte;
            while (!receivedDataQueue.TryDequeue(out rcvByte)) { await Task.Delay(300); }
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
    }
}
