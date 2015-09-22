using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace TeleBajaUEA
{
    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        public static GravarCorrida FormGravarCorrida { get; set; }

        private readonly static long CHECK_RATE = 500;
        private static Timer timerCheckIncomeData;
        private static ConcurrentQueue<SensorsData> CarDataQueue;
        private static SensorsData newData;

        // ----------------------- Tempor�rio para Teste --------------------//
        private static CarDataGenerator DataGenerator;
        // ------------------------------------------------------------------//

        // private static USBConnection USBConnection;

        // public static USBConnection GetInstance(){ return USBConnection }

        public async static Task<bool> ConnectToCar()
        {
            // tempo para conectar com o carro
            await Task.Delay(500);

            // se obteve sucesso, cria queue
         CarDataQueue = new ConcurrentQueue<SensorsData>();

            return true;
        }

        public static void StartListen()
        {
             timerCheckIncomeData = new Timer(TickCheckNewMessage,
                                        null, CHECK_RATE, Timeout.Infinite);

            // --------------------- Tempor�rio para Teste ------------------//
            DataGenerator = new CarDataGenerator(CarDataQueue);
            // --------------------------------------------------------------//
        }

        // ----------------------- Tempor�rio para Teste --------------------//
        public static void StartDataGenerator()
        {
            DataGenerator.StartGenerateData();
        }
        // ------------------------------------------------------------------//

        private static void TickCheckNewMessage(Object state)
        {
            timerCheckIncomeData.Change(CHECK_RATE, Timeout.Infinite);
            CheckNewMessage();
        }

        private static void CheckNewMessage()
        {
            if (CarDataQueue.TryDequeue(out newData))
                FormGravarCorrida.AddData(newData);
            // else n�o faz nada, espera pr�ximo tick
        }
    }
}
