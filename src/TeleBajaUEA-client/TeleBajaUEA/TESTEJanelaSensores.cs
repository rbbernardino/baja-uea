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
    public partial class TESTEJanelaSensores : Form
    {
        public TESTEJanelaSensores()
        {
            InitializeComponent();
        }

        public void SetData(SensorsData Data)
        {
            labelData.Text =
                "Tempo: " + FormatTimestamp(Data.TimeStamp) + "\n" +
                "Velocidade: " + Data.Speed + "\n" +
                "Temp do Motor: " + Data.EngineTemperature + "°C\n" +
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
    }
}
