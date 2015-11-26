using System;
using System.Windows.Forms;
using System.Collections.Concurrent;
using TeleBajaUEA.RaceDataStructs;
using System.Collections.Generic;

namespace TeleBajaUEA
{
    // Configuração de gráficos na na partial class ChartSettings
    public partial class GravarCorrida : FormPrincipal
    {
        private ConcurrentQueue<SensorsData> CarDataQueue;
        private Timer timerCheckIncomeData;
        private SensorsData newData;
        private List<FileSensorsData> dataList;
        private RaceParameters parameters;

        // TODO criar timer para a cada 5min salvar os dados no arquivo temporario
        private Timer timerBackupData;

        // ------------- temporário para testar -------------------//
        //public TESTEJanelaSensores formTesteMQSQ;
        //--------------------------------------------------------//

        private float currentXValue;

        public GravarCorrida(RaceParameters pParameters)
        {
            InitializeComponent();

            parameters = pParameters;
            dataList = new List<FileSensorsData>();

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

        private void TickCheckIncomeData(object source, EventArgs e)
        {
            CheckNewData();
        }

        private void CheckNewData()
        {
            if (CarDataQueue.TryDequeue(out newData))
                UpdateData(newData);
        }

        private void UpdateData(SensorsData newData)
        {
            // --------------------- teste ---------------------
            //UpdateTESTEform(newData);

            // TODO capturar último X impresso no gráfico real
            //double lastX = chartDinamic.Series[0].Points.Last().XValue;
            double lastX = currentXValue;

            // TODO verificar necessidade de remover pontos não mais mostrados
            // acho que NÃO precisa... chartDinamic.Series[0].Points.RemoveAt(0);
            double currentMinimum = chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum;
            double currentMaximumX = chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum;
            double interval = chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval;

            if (lastX >= currentMaximumX)
            {
                UpdateGraphLimits(currentMinimum, currentMaximumX, interval);
            }

            AddNewDataToGraph(newData);
            UpdateGauges(newData);
        }

        private void UpdateGauges(SensorsData newData)
        {
            aGaugeTemperature.Value = newData.EngineTemperature;
            aGaugeFuel.Value = newData.Fuel;
        }

        private void AddNewDataToGraph(SensorsData newData)
        {
            currentXValue += ((float) UPDATE_RATE) / 1000;

            // ajusta valor do RPM para ficar proporcional à altura do gráfico
            double newRPMData = newData.RPM / (RPM_MAXIMUM / Y_AXIS_MAXIMUM);

            double brakePosition;
            if (newData.BreakState)
                brakePosition = (Y_AXIS_MAXIMUM / 2) + Y_AXIS_INTERVAL;
            else
                brakePosition = (Y_AXIS_MAXIMUM / 2) - Y_AXIS_INTERVAL;

            chartDinamic.Series["Speed"].Points.AddXY(currentXValue, newData.Speed);
            chartDinamic.Series["RPM"].Points.AddXY(currentXValue, newRPMData);
            chartDinamic.Series["Brake"].Points.AddXY(currentXValue, brakePosition);

            FileSensorsData fileNewData = new FileSensorsData
            {
                xValue = currentXValue,
                speed = newData.Speed,
                rpm = newData.RPM,
                breakState = newData.BreakState,
            };
            dataList.Add(fileNewData);
        }

        private void UpdateGraphLimits(double currentMinimum,
                                        double currentMaximumX, double _interval)
        {
            minX += (long) UPDATE_LIMITS_INTERVAL;
            maxX += (long) UPDATE_LIMITS_INTERVAL;

            chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = minX;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = maxX;
            
            UpdateLabels();
            chartDinamic.Update();
        }

        // --------------- teste ------------------
        //private void UpdateTESTEform(SensorsData newData)
        //{
        //    formTesteMQSQ.SetData(this, newData);
        //}

        public void AddData(SensorsData data)
        {
            CarDataQueue.Enqueue(data);
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

            //---------- temporário para teste ---------------------- //
            //formTesteMQSQ.Close();
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCheckIncomeData.Dispose();
            timerCheckIncomeData = null;
        }

        private async void btEncerrar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Arquivos TeleBajaUEA (*.tbu)|*.tbu|Todos os Arquivos (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                // TODO verificar real necessidade de ser async...
                await RaceFile.SaveToFile(saveDialog.FileName, new RaceData(dataList, parameters));
            }

            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }
    }
}
