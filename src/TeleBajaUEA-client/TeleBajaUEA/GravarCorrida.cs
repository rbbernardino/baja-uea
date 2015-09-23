using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Threading;


namespace TeleBajaUEA
{
    public partial class GravarCorrida : FormPrincipal
    {
        private ConcurrentQueue<SensorsData> CarDataQueue;
        private System.Threading.Timer timerCheckIncomeData;
        private TESTEJanelaSensores formTesteMQSQ;
        private static SensorsData newData;

        private readonly static long UPDATE_RATE = 100;
        private double X_AXIS_INTERVAL = 10;

        private int currentXValue;

        public GravarCorrida()
        {

            InitializeComponent();
            CarDataQueue = new ConcurrentQueue<SensorsData>();


        // temporário para testar envio de mensagem
        formTesteMQSQ = new TESTEJanelaSensores();
            formTesteMQSQ.Show();

            // temporário para testar atualização de gráficos
            
            chartDinamic.Legends[0].Title= "Parameters";
            chartDinamic.ChartAreas["ChartArea1"].BackColor = Color.Black;

            // ---------------- Configurando o Y ----------------
            chartDinamic.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            chartDinamic.ChartAreas["ChartArea1"].AxisY.Maximum = 60;
            chartDinamic.ChartAreas["ChartArea1"].AxisY.Interval = 10;

            // linhas de trás/apoio
            chartDinamic.ChartAreas["ChartArea1"].AxisY.MajorGrid.Interval = 10;
            chartDinamic.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#686868");
            // --------------------------------------------------

            // ---------------- Configurando o X ---------------- 
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = 50;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval = X_AXIS_INTERVAL;

            // linhas de trás/apoio
            chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = X_AXIS_INTERVAL;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml("#686868");
            // --------------------------------------------------

            chartDinamic.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chartDinamic.Series[0].Color = Color.Red;
            chartDinamic.Series[0].BorderWidth = 1;
            chartDinamic.Series[0].Name = "Speed";
        }

        public void StartUpdateGraph()
        {
            currentXValue = 0;
            timerCheckIncomeData =
                new System.Threading.Timer(TickCheckIncomeData, null,
                                                UPDATE_RATE, Timeout.Infinite);
        }

        private async void TickCheckIncomeData(Object state)
        {
            timerCheckIncomeData.Change(UPDATE_RATE, Timeout.Infinite);
            await CheckNewData();
        }

        private async Task CheckNewData()
        {
            await Task.Run(() =>
            {
                if (CarDataQueue.TryDequeue(out newData))
                    UpdateGraph(newData);
            });
        }

        private void UpdateGraph(SensorsData newData)
        {
            UpdateTESTEform(newData);

            //double lastX = chartDinamic.Series[0].Points.Last().XValue;
            double lastX = currentXValue;

            //chartDinamic.Series[0].Points.RemoveAt(0);
            double currentMinimum = chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum;
            double currentMaximumX = chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum;
            double interval = chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval;

            if (lastX == currentMaximumX + 1)
            {
                UpdateGraphLimits(currentMinimum, currentMaximumX, interval);
            }

            UpdateGraphPoints(newData);
        }

        private void UpdateGraphPoints(SensorsData newData)
        {
            if (this.InvokeRequired)
                Invoke(new MethodInvoker(() =>
                {
                    chartDinamic.Series[0].Points.AddXY(currentXValue++, newData.Speed);
                }));
            else
                chartDinamic.Series[0].Points.AddXY(currentXValue++, newData.Speed);
        }

        private void UpdateGraphLimits(double currentMinimum,
                                        double currentMaximumX, double _interval)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = currentMinimum + 1;//interval;
                    chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = currentMaximumX + 1;//interval;
                    chartDinamic.Update();
                }));
            }
            else
            {
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = currentMinimum + 1;//interval;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = currentMaximumX + 1;//interval;
                chartDinamic.Update();
            }
        }

        // Usando invoker para tratar cross-thread method call e atualizar
        // o gráfico de forma "thread-safe"
        private void UpdateTESTEform(SensorsData newData)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    formTesteMQSQ.SetData(newData);
                }));
            }
            else
            {
                formTesteMQSQ.SetData(newData);
            }
        }

        public void AddData(SensorsData data)
        {
            CarDataQueue.Enqueue(data);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chartDinamic.ChartAreas[0].Area3DStyle.Enable3D = true;
            }
            else
            {
                chartDinamic.ChartAreas[0].Area3DStyle.Enable3D = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MenuPrincipal aux = new MenuPrincipal();           
            aux.Show();
        }

        private void GravarCorrida_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop timer
            timerCheckIncomeData.Change(Timeout.Infinite, Timeout.Infinite);
            timerCheckIncomeData.Dispose();
            timerCheckIncomeData = null;
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
