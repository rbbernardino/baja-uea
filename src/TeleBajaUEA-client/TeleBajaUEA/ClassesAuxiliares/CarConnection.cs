using System.IO.Ports;
using System.Timers;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        public static bool AvaiablePortExists { get { return SerialPort.GetPortNames().Length > 0; } }
        public static int IncomeByteRate { get; private set; }

        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        private static SerialPortBaja portXBee;

        private static int UPDATE_BYTE_RATE_INTERVAL = 1000;
        private static Timer timerUpdateByteRate;
        private static uint prevReceivedBytes;

        //private static RandomDataGenerator DataGenerator; // TODO TESTE

        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task ConnectToCar()
        {
            //DataGenerator = new RandomDataGenerator();// TODO TESTE
            //return;// TODO TESTE

            // tempo para conectar com o carro
            portXBee = new SerialPortBaja(Program.Settings.PortXBee);
            await Task.Run(() =>{ portXBee.Open(); });

            if (!portXBee.IsOpen)
                throw new System.Exception("Porta não pôde ser aberta por razões desconhecidas.");

            await portXBee.TryHandshake();
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
            portXBee.StartReceiveData();
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
            return await portXBee.GetNextPacket();
            //return await DataGenerator.GetNextPacket();// TODO TESTE
        }

        public static bool IsPortAvaiable(string pPortName)
        {
            string[] portList = SerialPort.GetPortNames();

            foreach (string portName in portList)
                if (portName.Equals(pPortName))
                    return true;

            return false;
        }
    }
}
