using System.IO.Ports;
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

        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        private static SerialPortBaja portXBee;

        //private static RandomDataGenerator DataGenerator;

        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task ConnectToCar()
        {
            //DataGenerator = new RandomDataGenerator();
            //return;

            // tempo para conectar com o carro
            portXBee = new SerialPortBaja(Program.Settings.PortXBee);
            await Task.Run(() =>{ portXBee.Open(); });

            if (!portXBee.IsOpen)
                throw new System.Exception("Porta não pôde ser aberta por razões desconhecidas.");

            await portXBee.TryHandshake();
        }

        public static void StartListen()
        {
            portXBee.StartReceiveData();
            //DataGenerator.StartReceiveData();
        }

        // TODO implentar encerrar conexão
        public static void CloseConnection()
        {
            if (portXBee != null && portXBee.IsOpen)
                portXBee.Close();
        }

        public async static Task<SensorsData> GetNextData()
        {
            return await portXBee.GetNextPacket();
            //return await DataGenerator.GetNextPacket();
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
