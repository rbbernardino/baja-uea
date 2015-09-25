using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    // TODO apenas para teste
    public partial class TESTEJanelaSensores : Form
    {
        private long timeStamp = 0;

        private Timer timer;

        public TESTEJanelaSensores()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TickTimer);
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

        public void SetData(SensorsData Data)
        {
             labelData.Text =
                "Qtd. de Pontos: " + Data.DataCount + "\n" +
                "Tempo: " + FormatTimestamp(timeStamp) + "\n" +
                "Velocidade: " + Data.Speed + "\n" +
                "Temp do Motor: " + Data.EngineTemperature + "°C\n" +
                "RPM: " + Data.RPM + "\n" +
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
