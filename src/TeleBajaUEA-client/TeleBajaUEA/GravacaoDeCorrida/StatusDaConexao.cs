using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA.GravacaoDeCorrida
{
    public partial class StatusDaConexao : Form
    {
        private readonly int CHECK_SPEED_INTERVAL = 1000;
        private Timer timerCheckSpeed;
        private double prevPointsCount;
        private double currentSpeed;

        private GravarCorrida formGravarCorrida;

        public StatusDaConexao(GravarCorrida pFormGravarCorrida)
        {
            InitializeComponent();

            timerCheckSpeed = new Timer();
            timerCheckSpeed.Interval = CHECK_SPEED_INTERVAL;
            timerCheckSpeed.Tick += TimerCheckSpeed_Tick;
            timerCheckSpeed.Enabled = true;

            formGravarCorrida = pFormGravarCorrida;

            prevPointsCount = currentSpeed = 0;
        }

        private void TimerCheckSpeed_Tick(object sender, EventArgs e)
        {
            double curPointsCount = formGravarCorrida.PointsCount;

            currentSpeed = curPointsCount - prevPointsCount;

            prevPointsCount = curPointsCount;
            UpdateMeters();
        }

        private void UpdateMeters()
        {
            labelIncome.Text = currentSpeed + " Pontos/s";
            labelByteRate.Text = CarConnection.IncomeByteRate + " B/s";
            labelTotalPontos.Text = formGravarCorrida.PointsCount + "Pts";
        }

        private void StatusDaConexao_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerCheckSpeed.Stop();
        }

        private void StatusDaConexao_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(timerCheckSpeed != null)
            {
                timerCheckSpeed.Dispose();
                timerCheckSpeed = null;
            }
        }
    }
}
