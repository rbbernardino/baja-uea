using System;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // apenas para teste
    public partial class TESTEJanelaSensores : Form
    {
        private long timeStamp = 0;

        private event DataShowHandler NewDataArrived;
        private delegate void DataShowHandler(object source, SensorsData data);

        // apenas millis
        private event ShowMillisHandler ShowMillis;
        private delegate void ShowMillisHandler(object source, uint millis);

        private Timer timer;

        public TESTEJanelaSensores()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TickTimer);

            //NewDataArrived += new DataShowHandler(ShowDataHandler_NewData);
            ShowMillis += new ShowMillisHandler(UpdateMillis_NewMillis);
        }

        public void StartCountTime()
        {
            timer.Enabled = true;
        }

        // TODO tempo não está sincronizado com exibição no gráfico (aqui está mais rápido)
        private void TickTimer(object _source, EventArgs _e)
        {
            timeStamp++;
        }

        public void SetMillis(object source, uint millis)
        {
            ShowMillis(source, millis); // dispara evento de mostar millis
        }
        
        private void UpdateMillis_NewMillis(object _source, uint millis)
        {
            labelData.Text = "Current Millis\n" +
                                millis;
        }


        public void SetData(object source, SensorsData data)
        {
            NewDataArrived(source, data);
        }

        // atualização de dados implementada com eventos para evitar problemas
        // de cross-thread call, logo essas chamadas são thread-safe!
        private void ShowDataHandler_NewData(object _source, SensorsData Data)
        {
             labelData.Text =
                //"Qtd. de Pontos: " + Data.DataCount + "\n" +
                "Tempo: " + FormatTimestamp(timeStamp) + "\n" +
                "Velocidade: " + (int) Data.Speed + "\n" +
                "Temp do Motor: " + Data.EngineTemperature + "°C\n" +
                "Combustível: " + (int) Data.Fuel + "%\n" +
                "RPM: " + (int) Data.RPM + "\n" +
                "Freio: " + FormatBreakState(Data.BreakState);
        }

        private string FormatBreakState(bool breakOn)
        {
            if (breakOn)
                return "<<<ON>>>";
            else
                return "OFF"; 
        }

        private string FormatTimestamp(long timestamp)
        {
            long minutes = timestamp / 60;
            string minutesStr = minutes.ToString();

            long seconds = timestamp % 60;
            string secondsStr = seconds.ToString();

            if (minutes < 10) minutesStr = "0" + minutes;
            if (seconds < 10) secondsStr = "0" + seconds;
            return "00:" + minutesStr + ":" + secondsStr;
        }

        private void TESTEJanelaSensores_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop timer
            timer.Stop();
            timer.Tick -= new EventHandler(TickTimer);
        }

        private void TESTEJanelaSensores_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Dispose();
            timer = null;
        }
    }
}
