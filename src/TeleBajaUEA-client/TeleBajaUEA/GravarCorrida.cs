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

        private int xAxis;


        public GravarCorrida()
        {

            InitializeComponent();
            CarMessageQueue = new ConcurrentQueue<SensorsData>();


        // temporário para testar envio de mensagem
        formTesteMQSQ = new TESTEJanelaSensores();
            formTesteMQSQ.Show();

            // temporário para testar atualização de gráficos
            
            chartDinamic.Legends[0].Title= "Parameters";

            chartDinamic.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            chartDinamic.Series[0].Color = Color.Red;
            chartDinamic.Series[0].BorderWidth=5;
            chartDinamic.Series[0].Name = "Speed";
           
        }

        public void StartUpdateGraph()
        {
            xAxis = 0;
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
                    UpdateGraph(newData);
            });
        }

        private void UpdateGraph(SensorsData newData)
        {
            formTesteMQSQ.SetData(newData);

            if (chartDinamic.Series[0].Points.Count > 9)
            {
                chartDinamic.Series[0].Points.RemoveAt(0);
                chartDinamic.Update();
            }

            else
            {
                chartDinamic.Series[0].Points.AddXY(xAxis++, newData.Speed);

            }


        }

        public void AddData(SensorsData data)
        {
            CarMessageQueue.Enqueue(data);
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
    }
}
