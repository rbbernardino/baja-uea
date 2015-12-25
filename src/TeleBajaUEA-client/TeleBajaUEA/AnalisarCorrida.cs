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
            double currentSpeed, maxSpeed = 0, minSpeed = 999, avgSpeed = 0;

            foreach (FileSensorsData pointData in raceData.DataList)
            {
                // Velocidade
                AddPoint("Speed", pointData.xValue, pointData.speed);

                // Velocidade max, media e min
                currentSpeed = pointData.speed;
                if (currentSpeed > maxSpeed) maxSpeed = currentSpeed;
                if (currentSpeed < minSpeed) minSpeed = currentSpeed;
                avgSpeed += currentSpeed / raceData.DataList.Count;

                // RPM
                AddPoint("RPM", pointData.xValue, pointData.rpm);

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

        private void AddPoint(string serie, double x, double y)
        {
            // a unidade "fundamental" do gráfico para ser incrementado
            // a ideia é evitar que quando y seja muito pequeno ele não seja visto no gráfico
            double chartUnity = chartsNew.ChartAreas[serie].AxisY.Maximum / 100;

            if (y <= chartUnity)
                chartsNew.Series[serie].Points.AddXY(x, chartUnity);
            else
                chartsNew.Series[serie].Points.AddXY(x, y);
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

        // esse código existe apenas para remover as legendas geradas automaticamente
        // que são da velocidade, rpm e freio...
        private void chartsNew_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            int customItems = ((Chart)sender).Legends[0].CustomItems.Count();
            if (customItems > 0)
            {
                int numberOfAutoItems = e.LegendItems.Count() - customItems;
                for (int i = 0; i < numberOfAutoItems; i++)
                {
                    e.LegendItems.RemoveAt(0);
                }
            }
        }
    }
}
