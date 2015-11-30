using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        private static SerialPortBaja portXBee;

        //private static RandomDataGenerator DataGenerator;
        //private static DataGenMachine DataGenerator;

        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task<bool> ConnectToCar()
        {
            // tempo para conectar com o carro
            // TODO fazer catch para tratar falha ao conectar com o USB
            portXBee = new SerialPortBaja(ProgramSettings.PortXBee);
            await Task.Run(() =>{ portXBee.Open(); });

            // TODO melhorar retorno de erro ao abrir porta
            if (!portXBee.IsOpen)
                return false;

            return await portXBee.TryHandshake();
        }

        public static void StartListen()
        {
            portXBee.StartReceiveData();
        }

        // TODO implentar encerrar conexão
        public static void CloseConnection()
        {
            // TODO timers ficam mais lentos ao reabrir janela de gravar corrida
            
            //DataGenerator.Stop();
            //DataGenerator = null;
        }

        public async static Task<SensorsData> GetNextData()
        {
            return await portXBee.GetNextPacket();
        }

        // ----------------------- Tempor�rio para Teste --------------------//
        //public static void StartDataGenerator()
        //{
        //    DataGenerator = new DataGenMachine();
        //    DataGenerator.Start();
        //}
        // ------------------------------------------------------------------//
    }
}
