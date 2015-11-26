using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
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

        #region Temporary Variables
        // variáveis temporárias dos dados de um pacote que está sendo recebido
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

        //private static RandomDataGenerator DataGenerator;
        //private static DataGenMachine DataGenerator;

        #region XBee communication variables
        private static bool receivedSTART;

        private static SemaphoreSlim canReceiveDataSignal; //TODO repensar necessidade disso...
        private static SerialPortExt portXBee;
        // public static USBConnection GetInstance(){ return USBConnection }
        private enum SerialMsg
        {
            // recebe da Serial
            START = 'S',
            BEGIN = 'B',
            END = 'E',

            // envia para Serial
            OK = 'K',
        }

        private static rcvState currentState;
        private enum rcvState
        {
            BEGIN,
            Freio, NivelComb, Temperatura, RPM,
            END,
        }
        #endregion

        // TODO fazer await aqui e ali? realmetne necessário async??
        public async static Task<bool> ConnectToCar()
        {
            // tempo para conectar com o carro
            // TODO fazer catch para tratar falha ao conectar com o USB
            portXBee = new SerialPortExt(ProgramSettings.PortXBee);
            await Task.Run(() =>{ portXBee.Open(); portXBee.GetHashCode(); });

            // TODO melhorar retorno de erro ao abrir porta
            if (!portXBee.IsOpen)
                return false;

            // TODO trocar por TaskCompletionSource?? (http://stackoverflow.com/a/12858633)
            canReceiveDataSignal = new SemaphoreSlim(0, 1);
            receivedSTART = false;
            portXBee.DataReceived += ReceiveUntilStart;
            await canReceiveDataSignal.WaitAsync();

            // quando sair do await é porque conectou ou deu falha...
            if(receivedSTART)
                return true;
            else
                return false; // TODO melhorar retorno de erro..
        }

        // cara handler representa um estado, nesse está esperando o START
        // se, ao receber mensagem, esta for START dá continuidade, senão ignora
        private static void ReceiveUntilStart(object sender, SerialDataReceivedEventArgs e)
        {
            if (portXBee.ReadCharCasted() == (char)SerialMsg.START)
            {
                // TODO indiciar que conexao com o carro está ok e está pronto
                //      para iniciar recepção de dados. Possivelmente através
                //      de um event que enviará mensagem para a interface principal
                portXBee.DataReceived -= ReceiveUntilStart;
                receivedSTART = true;
                canReceiveDataSignal.Release();
            }
            // TODO tratar else, recebeu algo diferente de START (possível erro de sincronia?)
        }

        // neste handler vários estados estão encapsulados, recebe os dados um
        // por um...
        private static void portXBee_NewDataArrived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (currentState)
            {
                // flag BEGIN usada para garantir que dados estarão sincronizados
                case rcvState.BEGIN:
                    if (portXBee.ReadCharCasted() == (char)SerialMsg.BEGIN)
                    {
                        currentState = rcvState.Freio;
                    }
                    break;

                case rcvState.Freio:
                    tmpBreakState = portXBee.ReadCharCasted();
                    currentState = rcvState.NivelComb;
                    break;
                case rcvState.NivelComb:
                    tmpFuel = portXBee.ReadInt8();
                    currentState = rcvState.Temperatura;
                    break;
                case rcvState.Temperatura:
                    tmpTemperature = portXBee.ReadInt16();
                    currentState = rcvState.RPM;
                    break;
                case rcvState.RPM:
                    tmpRpm = portXBee.ReadInt16();
                    Send(sender, new SensorsData(
                            tmpSpeed, tmpTemperature,
                            tmpRpm, tmpFuel, tmpBreakState));
                    currentState = rcvState.BEGIN;
                    break;
            }
        }

        public static void StartListen()
        {
            portXBee.DataReceived += portXBee_NewDataArrived;
            NewDataArrived += new NewDataHandler(NewDataHandler_Arrived);

            portXBee.WriteChar((char)SerialMsg.OK);
        }

        public static void CloseConnection()
        {
            // TODO timers ficam mais lentos ao reabrir janela de gravar corrida
            
            //DataGenerator.Stop();
            //DataGenerator = null;
        }

        // ----------------------- Tempor�rio para Teste --------------------//
        //public static void StartDataGenerator()
        //{
        //    DataGenerator = new DataGenMachine();
        //    DataGenerator.Start();
        //}
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
