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
    }
}
