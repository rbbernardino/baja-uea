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
        private ConcurrentQueue<SensorsData> CarMessageQueue;
        private System.Threading.Timer timerCheckIncomeData;
        private readonly static long UPDATE_RATE = 1000;
        private TESTEJanelaSensores formTesteMQSQ;
        private static SensorsData newData;

        public GravarCorrida()
        {
            InitializeComponent();
            CarMessageQueue = new ConcurrentQueue<SensorsData>();

            // temporário para testar envio de mensagem
            formTesteMQSQ = new TESTEJanelaSensores();
            formTesteMQSQ.Show();

            // temporário para testar atualização de gráficos
            chartDinamic.Legends.Clear();
            chartDinamic.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
        }

        public void StartUpdateGraph()
        {
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
                if (CarMessageQueue.TryDequeue(out newData))
                    formTesteMQSQ.SetData(newData);
            });
        }

        public void AddData(SensorsData data)
        {
            CarMessageQueue.Enqueue(data);
        }

        // --------------------- temporario para atualização de gráficos ----------------------------- //
        int x = 2000;

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

        private void timer_Tick(object sender, EventArgs e)
        {
            //   timer1 = new System.Threading.Timer(_ => OnCallBack(), null, 0, 1000 * 10); //every 10 seconds

            if (chartDinamic.Series[0].Points.Count > 5)
            {
                chartDinamic.Series[0].Points.RemoveAt(0);
            }

            chartDinamic.Series[0].Points.AddXY(x++, new Random().NextDouble());
        }
    }
}
