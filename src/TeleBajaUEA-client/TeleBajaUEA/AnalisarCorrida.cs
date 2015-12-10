using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class AnalisarCorrida : FormPrincipal
    {
        private RaceData raceData;

        public AnalisarCorrida(RaceData pRaceData)
        {
            InitializeComponent();

            ConfigureCharts();

            raceData = pRaceData;

            AddDataToCharts();

            // ativa ou desativa botões para permitir "andar" para  dir/esq
            UpdateButtonsState();
        }

        private void AddDataToCharts()
        {
            // popula gráfico com os pontos
            double brakePosition;
            double maxSpeed = 0, avgSpeed = 0, minSpeed = 999;

            int i = 0;
            double t;

            foreach (FileSensorsData pointData in raceData.DataList)
            {
                t = pointData.speed;// + i++;
                //if (t > SPEED_MAXIMUM-5) t = SPEED_MAXIMUM-5;
                // Velocidade
                chartsNew.Series["Speed"].Points.AddXY(pointData.xValue, pointData.speed);

                // Velocidade max, media e min
                if (pointData.speed > maxSpeed) maxSpeed = pointData.speed;
                if (pointData.speed < minSpeed) minSpeed = pointData.speed;
                //avgSpeed += pointData.speed / raceData.DataList.Count;
                if (t > maxSpeed)
                    maxSpeed = t;
                if (t < minSpeed) minSpeed = t;
                avgSpeed += t / raceData.DataList.Count;

                // RPM
                chartsNew.Series["RPM"].Points.AddXY(pointData.xValue, pointData.rpm);

                // Freio
                if (pointData.breakState)
                    brakePosition = (BRAKE_MAXIMUM / 2) + (BRAKE_MAXIMUM / 4);
                else
                    brakePosition = (BRAKE_MAXIMUM / 2) - (BRAKE_MAXIMUM / 4);

                chartsNew.Series["Brake"].Points.AddXY(pointData.xValue, brakePosition);

            }

            // seta a velocidade max/media/min
            CreateStripLines(maxSpeed, avgSpeed, minSpeed);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MenuPrincipal aux = new MenuPrincipal();
            aux.Show();
        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }

        private void btPlus_Click(object sender, EventArgs e)
        {
            IncreaseXLimits();
        }

        private void btMinus_Click(object sender, EventArgs e)
        {
            DecreaseXLimits();
        }

        private void IncreaseXLimits()
        {
            minX += (long)INCREASE_LIMITS_INTERVAL;
            maxX += (long)INCREASE_LIMITS_INTERVAL;

            foreach (ChartArea chart in chartsNew.ChartAreas)
            {
                chart.AxisX.Minimum = minX;
                chart.AxisX.Maximum = maxX;
            }

            UpdateXLabels();
            UpdateButtonsState();
        }

        private void DecreaseXLimits()
        {
            minX -= (long)INCREASE_LIMITS_INTERVAL;
            maxX -= (long)INCREASE_LIMITS_INTERVAL;

            foreach (ChartArea chart in chartsNew.ChartAreas)
            {
                chart.AxisX.Minimum = minX;
                chart.AxisX.Maximum = maxX;
            }

            UpdateXLabels();
            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            // verifica se o primeiro ponto está plotado, impedindo o gráfico
            // de ir mais a esquerda, onde não existem mais pontos
            FileSensorsData firstPoint = raceData.DataList.First();
            if (firstPoint.xValue >= minX)
            {
                btMinus.Enabled = false;
            }
            else
            {
                btMinus.Enabled = true;
            }

            // verifica se o último ponto está plotado, impedindo o gráfico
            // de ir mais a direita, onde não existem pontos
            FileSensorsData lastPoint = raceData.DataList.Last();
            if (lastPoint.xValue <= maxX)
            {
                btPlus.Enabled = false;
            }
            else
            {
                btPlus.Enabled = true;
            }
        }

        private void btVerSetup_Click(object sender, EventArgs e)
        {
            AnalisarCorridaSetup formSetup = new AnalisarCorridaSetup(raceData.Parameters);
            formSetup.Show();
            formSetup.FormClosed += FormSetup_FormClosed;
            btVerSetup.Enabled = false;
        }

        private void FormSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            btVerSetup.Enabled = true;
        }
    }
}
