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

namespace TeleBajaUEA
{
    // Configuração de gráficos na na partial class ChartSettings
    public partial class GravarCorrida : FormPrincipal
    {
        private ConcurrentQueue<SensorsData> CarDataQueue;
        private Timer timerCheckIncomeData;
        private static SensorsData newData;

        // define quantos pontos mover para a direita quando pontos plotados
        // atingirem o limite de plotagem (na direita)
        private readonly double UPDATE_LIMITS_INTERVAL = 10;

        // TODO apenas para teste
        // ------------- temporário para testar -------------------//
        public TESTEJanelaSensores formTesteMQSQ;
        //--------------------------------------------------------//

        private float currentXValue;

        public GravarCorrida()
        {
            InitializeComponent();

            // Prepara timer que vai atualizar o gráfico a cada UPDATE_RATE ms
            timerCheckIncomeData = new Timer();
            timerCheckIncomeData.Interval = UPDATE_RATE;
            timerCheckIncomeData.Tick += new EventHandler(TickCheckIncomeData);

            CarDataQueue = new ConcurrentQueue<SensorsData>();
        }

        public void StartUpdateCharts()
        {
            currentXValue = 0;
            timerCheckIncomeData.Enabled = true;
        }

        private async void TickCheckIncomeData(object source, EventArgs e)
        {
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

            // TODO capturar último X impresso no gráfico real
            //double lastX = chartDinamic.Series[0].Points.Last().XValue;
            double lastX = currentXValue;

            // TODO chartDinamic.Series[0].Points.RemoveAt(0);
            double currentMinimum = chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum;
            double currentMaximumX = chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum;
            double interval = chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval;

            if (lastX >= currentMaximumX)
            {
                UpdateGraphLimits(currentMinimum, currentMaximumX, interval);
            }

            UpdateGraphPoints(newData);
        }

        private void UpdateGraphPoints(SensorsData newData)
        {
            currentXValue += ((float) UPDATE_RATE) / 1000;

            if (this.InvokeRequired)
                Invoke(new MethodInvoker(() =>
                {
                    chartDinamic.Series[0].Points.AddXY(currentXValue, newData.Speed);
                }));
            else
            {
                chartDinamic.Series[0].Points.AddXY(currentXValue, newData.Speed);
            }
        }

        private void UpdateGraphLimits(double currentMinimum,
                                        double currentMaximumX, double _interval)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() =>
                {
                    chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = currentMinimum + UPDATE_LIMITS_INTERVAL;
                    chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = currentMaximumX + UPDATE_LIMITS_INTERVAL;
                    UpdateLabels();
                    chartDinamic.Update();
                }));
            }
            else
            {
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = currentMinimum + UPDATE_LIMITS_INTERVAL;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = currentMaximumX + UPDATE_LIMITS_INTERVAL;
                UpdateLabels();
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
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }

        private void GravarCorrida_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop timer
            timerCheckIncomeData.Stop();
            timerCheckIncomeData.Tick -= new EventHandler(TickCheckIncomeData);

            // Encerra conexões
            CarConnection.CloseConnection();

            // TODO apenas para teste
            //---------- temporário para teste ---------------------- //
            formTesteMQSQ.Close();
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCheckIncomeData.Dispose();
            timerCheckIncomeData = null;
        }
    }
}
