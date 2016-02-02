using System.IO.Ports;
using System.Timers;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;
using System;
using System.Collections.Generic;

namespace TeleBajaUEA
{
    public enum SignalStrg { Off, Low, Medium, Good, Excelent }

    // Essa classe � um Singleton para manter a conex�o com o Carro
    // Tamb�m encapsula a tradu��o entre Formato bytes XBee ---> Objeto do C#
    public sealed class CarConnection
    {
        public static bool AvaiablePortExists { get { return SerialPort.GetPortNames().Length > 0; } }
        public static int IncomeByteRate { get; private set; }
        public static int BytesToRead { get { return portXBee.BytesToReadExt; } }
        public static uint TotalReceivedBytes { get { return portXBee.TotalReceivedBytes; } }
        public static SignalStrg ConnStatus { get; private set; } = SignalStrg.Off;
        public static int IgnoredDataPacket { get; private set; }

        #region communication settings
        private static int UPDATE_BYTE_RATE_INTERVAL = 1000;
        private static int CONNECT_RETRY = 10; // número máximo de tentativas para enviar "READY"
        private static int CONNECT_RETRY_INTERVAL = 1000; // tempo entre cada tentativa de enviar "READY"
        #endregion

        private static event NewDataHandler NewDataArrived;
        private delegate void NewDataHandler(object source, SensorsData newData);

        private static SerialPortExt portXBee;

        private static Timer timerUpdateByteRate;
        private static uint prevReceivedBytes;

        #region Protocol
        //private static SemaphoreSlim canReceiveDataSignal;
        private static string XB_READY = "READY"; // ENVIA: checa se XBee pode iniciar envio
        private static string XB_RESET = "RESET"; // ENVIA: reinicia execução do arduino
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

        // ----------- TESTE apenas------------------------------
        //private static RandomDataGenerator DataGenerator = new RandomDataGenerator();
        //------------------------

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
            timerUpdateByteRate.Elapsed += TickUpdateByteRate;
            timerUpdateByteRate.Start();
            prevReceivedBytes = 0;

            // inicia leitura/salvamento dos dados recebidos pela porta USB
            portXBee.WriteLine(XB_START);
        }

        private static void TickUpdateByteRate(object sender, ElapsedEventArgs e)
        {
            uint curReceivedBytes = portXBee.TotalReceivedBytes;

            IncomeByteRate = (int) (curReceivedBytes - prevReceivedBytes);

            prevReceivedBytes = curReceivedBytes;
        }

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
            // ---------- para TESTE ---------
            //return await DataGenerator.GetNextPacket();
            // -----------------------

            Tuple<bool, SensorsData> result;
            do
            {
                result = await GetNextPacket();
            } while (!result.Item1);

            return result.Item2;
        }

        // TODO implementar um next packet mais específico (total de 10 bytes + '\r')
        // lê os dados, armazena em variáveis temporárias e gera um novo objeto SensorsData (newData)
        public async static Task<Tuple<bool, SensorsData>> GetNextPacket()
        {
            string rcvMsg = await portXBee.ReadLineExt();

            await SyncNextPacket();

            tmpMillis = await portXBee.NextUInt32();
            tmpBreakState = await portXBee.NextChar();
            tmpTemperature = await portXBee.NextInt16();
            tmpRpm = await portXBee.NextInt16();
            tmpSpeed = await portXBee.NextInt8();

            ConnStatus = RssiToQuality(-1*(await portXBee.NextInt8()));

            rcvMsg = await portXBee.ReadLineExt();

            if (rcvMsg.Equals(XB_END))
            {
                SensorsData newData = new SensorsData(
                    tmpMillis, tmpSpeed, tmpTemperature, tmpRpm, tmpBreakState);
                return Tuple.Create(true, newData);
            }
            else
            {
                IgnoredDataPacket++;
                return Tuple.Create(false, new SensorsData());
                //throw new Exception("Protocolo incorreto. Esperava END('E'), " +
                //    "mas recebeu ' " + rcvMsg + " '");
            }
        }

        // Espera pelo próximo BEGIN("B\r") para iniciar leitura dos dados
        // pode ser que  a conexao se perca e seja reestabelecida. Nesse caso,
        // muito provavelmente o pacote virá quebrado, logo é preciso sincronizar
        // Outro caso é quando dá o timeout para verificar a qualidade do sinal,
        // onde o recebimento de dados é interrompido para fazer isso
        private async static Task SyncNextPacket()
        {
            string rcvMsg = await portXBee.ReadLineExt();
            while (!(rcvMsg).Equals(XB_BEGIN))
            {
                if (rcvMsg.Equals(XB_END))
                    IgnoredDataPacket++;
                rcvMsg = await portXBee.ReadLineExt();
            }
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
            string rcvMsg = "";

            portXBee.WriteLine(XB_RESET);
            await Task.Delay(1000);
            portXBee.ClearInBuffer();

            await ATcmd();
            if (await ATConnectedToCar())
            {
                bool ready_ok = false;
                int tryCount = 0;
                while (!ready_ok)
                {
                    portXBee.WriteLine(XB_READY);
                    try
                    {
                        rcvMsg = await portXBee.ReadLineExt(CONNECT_RETRY_INTERVAL);
                        ready_ok = true;
                    }
                    catch (ErrorMessage.ReceiveDataTimeoutException e)
                    {
                        if (tryCount > CONNECT_RETRY)
                            throw (e);
                        else
                            tryCount++;
                    }
                }

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

        private static SignalStrg RssiToQuality(int rssi)
        {
            if (rssi <= -96) return SignalStrg.Off;
            else if (rssi <= -85) return SignalStrg.Low;
            else if (rssi <= -75) return SignalStrg.Medium;
            else if (rssi < -50) return SignalStrg.Good;
            else if (rssi >= -50) return SignalStrg.Excelent;
            else
                throw (new System.Exception(
                    "Erro ao atualizar qualidade do sinal, RSSI inválido!"));
        }

        #region funcoes de interface com o XBee
        // Obs: O XBee sai do command mode automaticamente após um ATDN
        private async static Task<bool> ATConnectedToCar()
        {
            string rcv_msg = "";

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
        // entra no command mode do XBee
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
