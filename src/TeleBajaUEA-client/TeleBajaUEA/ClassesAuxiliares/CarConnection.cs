using System.IO.Ports;
using System.Timers;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;
using System;

namespace TeleBajaUEA
{
    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        public static bool AvaiablePortExists { get { return SerialPort.GetPortNames().Length > 0; } }
        public static int IncomeByteRate { get; private set; }
        public static bool ConnectedToCar { get; private set; }

        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        private static SerialPortExt portXBee;

        private static int UPDATE_BYTE_RATE_INTERVAL = 1000;
        private static Timer timerUpdateByteRate;
        private static uint prevReceivedBytes;

        #region Protocol
        //private static SemaphoreSlim canReceiveDataSignal;
        private static string XB_READY = "READY"; // ENVIA: checa se XBee pode iniciar envio
        private static string XB_START = "START"; // ENVIA: pede por inicio do envio de dados
        private static string XB_BEGIN = "B"; // RECEBE: inicio de pacote
        private static string XB_END = "E"; // RECEBE: fim de pacote
        #endregion

        #region XBee settings
        private static int XBEE_CMD_DELAY = 100; // valor do param GT do XBee
        private static char XBEE_LINE_END = '\r';
        private static string XBEE_CAR_ID = "XBEE_CARRO";
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

        //private static RandomDataGenerator DataGenerator; // TODO TESTE

        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task<bool> ConnectToCar()
        {
            //DataGenerator = new RandomDataGenerator();// TODO TESTE
            //return;// TODO TESTE

            // tempo para conectar com o carro
            portXBee = new SerialPortExt();
            portXBee.PortName = Program.Settings.PortXBee;
            portXBee.NewLine = XBEE_LINE_END.ToString();

            await Task.Run(() =>{ portXBee.Open(); });

            if (!portXBee.IsOpen)
                throw new System.Exception("Porta não pôde ser aberta por razões desconhecidas.");

            return await TryHandshake();
        }

        public static void StartListen()
        {
            // inicia timer que vai atualizar a taxa de transferência;
            timerUpdateByteRate = new Timer();
            timerUpdateByteRate.AutoReset = true;
            timerUpdateByteRate.Interval = UPDATE_BYTE_RATE_INTERVAL;
            timerUpdateByteRate.Elapsed += TimerUpdateByteRate_Elapsed;
            timerUpdateByteRate.Start();
            prevReceivedBytes = 0;

            // inicia leitura/salvamento dos dados recebidos pela porta USB
            portXBee.WriteLine(XB_START);
            //DataGenerator.StartReceiveData();// TODO TESTE
        }

        private static void TimerUpdateByteRate_Elapsed(object sender, ElapsedEventArgs e)
        {
            uint curReceivedBytes = portXBee.TotalReceivedBytes;

            IncomeByteRate = (int) (curReceivedBytes - prevReceivedBytes);

            prevReceivedBytes = curReceivedBytes;
        }

        // TODO implentar encerrar conexão
        public static void CloseConnection()
        {
            if (portXBee != null && portXBee.IsOpen)
                portXBee.Close();

            if(timerUpdateByteRate != null)
            {
                timerUpdateByteRate.Stop();
                timerUpdateByteRate.Dispose();
                timerUpdateByteRate = null;
            }
        }

        public async static Task<SensorsData> GetNextData()
        {
            return await GetNextPacket();
            //return await DataGenerator.GetNextPacket();// TODO TESTE
        }

        // TODO implementar GetNextPacket (total de 10 bytes excluindo o 'B'
        // pode gerar a exceção NextByteTimeoutException
        // lê os dados, armazena em variáveis temporárias e gera um novo objeto SensorsData (newData)
        public async static Task<SensorsData> GetNextPacket()
        {
            bool rcvOK = false;
            string rcvMsg = await portXBee.ReadLineExt();

            // flag BEGIN/END usadas para garantir que dados estarão sincronizados
            if (rcvMsg.Equals(XB_BEGIN))
            {
                tmpMillis = await portXBee.NextUInt32();
                tmpBreakState = await portXBee.NextChar();
                tmpTemperature = await portXBee.NextInt16();
                tmpRpm = await portXBee.NextInt16();
                tmpSpeed = await portXBee.NextInt8();

                rcvMsg = await portXBee.ReadLineExt();
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

        public static bool IsPortAvaiable(string pPortName)
        {
            string[] portList = SerialPort.GetPortNames();

            foreach (string portName in portList)
                if (portName.Equals(pPortName))
                    return true;

            return false;
        }

        // verifica estado da conexao do XBee e confirma se o carro está pronto
        // para enviar dados
        private async static Task<bool> TryHandshake()
        {
            string rcvMsg;
            if (await ATConnectedToCar())
            {
                portXBee.WriteLine(XB_READY);

                rcvMsg = await portXBee.ReadLineExt();

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

        #region funcoes de interface com o XBee
        private async static Task<bool> ATConnectedToCar()
        {
            string rcv_msg = "";

            // entrar no command mode
            await ATcmd();

            // verifica conexao
            portXBee.WriteLine("ATDN" + XBEE_CAR_ID);
            rcv_msg = await portXBee.ReadLineExt();

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

        // TODO considerar acrescentar um timeout quando esperando pelo OK
        private async static Task ATcmd()
        {
            await Task.Delay(XBEE_CMD_DELAY);
            portXBee.Write("+++");

            // quando entrar no modo de comando do XBee durante o recebimento de
            // dados, é preciso descartar os dados que estão na fila até a chegada do OK
            string readLine = await portXBee.ReadLineExt();
            while (!readLine.Equals("OK"))
                readLine = await portXBee.ReadLineExt();
        }
        #endregion
    }
}
