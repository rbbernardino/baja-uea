using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        public static GravarCorrida FormGravarCorrida { get; set; }

        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        // ----------------------- Tempor�rio para Teste --------------------//
        //private static RandomDataGenerator DataGenerator;
        private static DataGenMachine DataGenerator;
        // ------------------------------------------------------------------//

        // private static USBConnection USBConnection;

        // public static USBConnection GetInstance(){ return USBConnection }

        public async static Task<bool> ConnectToCar()
        {
            // tempo para conectar com o carro
            await Task.Delay(500);

            return true;
        }

        public static void StartListen()
        {
            NewDataArrived += new NewDataHandler(NewDataHandler_Arrived);
        }

        public static void CloseConnection()
        {
            // TODO timers ficam mais lentos ao reabrir janela de gravar corrida
            DataGenerator.Stop();
            DataGenerator = null;
        }

        // TODO temporario para teste
        // ----------------------- Tempor�rio para Teste --------------------//
        public static void StartDataGenerator()
        {
            DataGenerator = new DataGenMachine();
            DataGenerator.Start();
        }
        // ------------------------------------------------------------------//

        public static void Send(object source, SensorsData newData)
        {
            NewDataArrived(source, newData);
        }

        private static void NewDataHandler_Arrived(object _source, SensorsData newData)
        {
            FormGravarCorrida.AddData(newData);
        }
    }
}
