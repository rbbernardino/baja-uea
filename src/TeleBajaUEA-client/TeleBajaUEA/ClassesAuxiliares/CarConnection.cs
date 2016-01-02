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

        // TODO ------------------TESTE-------------------
        private static RandomDataGenerator DataGenerator;
        //private static DataGenMachine DataGenerator;
        
        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task<bool> ConnectToCar()
        {
            DataGenerator = new RandomDataGenerator();
            return true;
            
            // TODO remover ^ ------------------TESTE-------------------

            // tempo para conectar com o carro
            // TODO fazer catch para tratar falha ao conectar com o USB
            portXBee = new SerialPortBaja(Program.Settings.PortXBee);
            await Task.Run(() =>{ portXBee.Open(); });

            // TODO melhorar retorno de erro ao abrir porta
            if (!portXBee.IsOpen)
                return false;

            return await portXBee.TryHandshake();
        }

        public static void StartListen()
        {
            //portXBee.StartReceiveData();
            // TODO ------------------TESTE-------------------
            DataGenerator.StartReceiveData();
        }

        // TODO implentar encerrar conexão
        public static void CloseConnection()
        {
        }

        public async static Task<SensorsData> GetNextData()
        {
            //return await portXBee.GetNextPacket();
        // TODO ------------------TESTE-------------------
            return await DataGenerator.GetNextPacket();
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
