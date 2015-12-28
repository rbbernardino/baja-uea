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
        private ToolTip toolTipPoint = new ToolTip();
        private Point? prevPosition = null;

        private RaceData raceData;
        
        // DataSize indica a quantidade de pontos e o valor máximo do eixo X
        private double DataSize { get; }

        public AnalisarCorrida(RaceData pRaceData)
        {
            InitializeComponent();

            // configura a quantidade de pontos e valor máximo do eixo X
            DataSize = pRaceData.DataList.Last().xValue;
            raceData = pRaceData;

            AddDataToCharts();

            ConfigureCharts();

            // ativa ou desativa botões para permitir "andar" para  dir/esq
            UpdateButtonsState();

            // evento para detectar quando o ponteiro do mouse estiver em cima de algum gráfico
            chartsNew.MouseMove += new MouseEventHandler(chartsNew_MouseMove);
            toolTipPoint.AutomaticDelay = 10;
        }

        // obtido a partir de: http://pastebin.com/PzhHtfMu
        private void chartsNew_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            toolTipPoint.RemoveAll();
            prevPosition = pos;
            var results = chartsNew.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);

            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around)
                        if (Math.Abs(pos.X - pointXPixel) < 2 &&
                            Math.Abs(pos.Y - pointYPixel) < 2)
                        {
                            toolTipPoint.Show("X=" + prop.XValue + ", Y=" + prop.YValues[0], chartsNew,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
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

        #region Button Events {...}
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
        }

        private void btMinus_Click(object sender, EventArgs e)
        {
        }

        private void btVerSetup_Click(object sender, EventArgs e)
        {
            AnalisarCorridaSetup formSetup = new AnalisarCorridaSetup(raceData.Parameters);
            formSetup.Show();
            formSetup.FormClosed += FormSetup_FormClosed;
            btVerSetup.Enabled = false;
        }

        private void btZoomIn_Click(object sender, EventArgs e)
        {
            ChartArea c = chartsNew.ChartAreas[XLabelChartArea];

            double newMin = c.AxisX.CustomLabels[1].FromPosition + 5 * ((XInterval / 10));
            double newMax = newMin + XInterval * 2;//(LABELS_INTERVAL_RATE-3);

            //double decreaseRate = (LABELS_INTERVAL_RATE - 1) * scaleViewSize / LABELS_INTERVAL_RATE;
            // = c.AxisX.ScaleView.ViewMinimum;
            //double curMax = c.AxisX.ScaleView.ViewMaximum;

            //MessageBox.Show("isso: " + curMin + "\n" + "e isso: " + curMax);
            //c.AxisX.ScaleView.Zoom(curMin + decreaseRate, curMax - decreaseRate, DateTimeIntervalType.Number, true);
            c.AxisX.ScaleView.Zoom(newMin, newMax,    DateTimeIntervalType.Number,true );

            UpdateCharts();
            i++;
        }
        int i=0; // TODO melhorar, corrigir, mudar estratégia desse contador...
        private void btZoomOut_Click(object sender, EventArgs e)
        {
            if (i == 0)
            {
                chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.Zoom(0, DataSize);
                btZoomOut.Enabled = false;
            }
            else
            {
                chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.ZoomReset();
                i--;
            }
            UpdateCharts();
        }
        #endregion


        private void UpdateButtonsState()
        {
            // verifica se o primeiro ponto está plotado, impedindo o gráfico
            // de ir mais a esquerda, onde não existem mais pontos
            FileSensorsData firstPoint = raceData.DataList.First();
            if (firstPoint.xValue >= 0)
                btMinus.Enabled = false;
            else
                btMinus.Enabled = true;

            // verifica se o último ponto está plotado, impedindo o gráfico
            // de ir mais a direita, onde não existem pontos
            FileSensorsData lastPoint = raceData.DataList.Last();
            if (lastPoint.xValue <= DataSize)
                btPlus.Enabled = false;
            else
                btPlus.Enabled = true;
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

        private void chartsNew_AxisViewChanged(object sender, ViewEventArgs e)
        {
            UpdateCharts();
        }

        private void chartsNew_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            UpdateCharts();
        }

        private void UpdateCharts()
        {
            UpdateAllCharts();
            UpdateXLabels();
            UpdateButtonsState();
        }
    }
}
