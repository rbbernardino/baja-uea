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
        private SensorsData data;
        private List<FileSensorsData> dataList;
        private RaceParameters parameters;

        // TODO criar timer para a cada 5min salvar os dados no arquivo temporario
        private Timer timerBackupData;

        // ------------- temporário para testar -------------------//
        public TESTEJanelaSensores formTesteMQSQ;
        //--------------------------------------------------------//

        // o primeiro Millis será usado como referência (zero) do gráfico no eixo X
        private uint zeroMillis;
        private bool gotZeroMillis; // após receber primeiro dado vai usá-lo como referência
        private Timer timerWaitZeroMillis;

        public GravarCorrida(RaceParameters pParameters)
        {
            InitializeComponent();

            parameters = pParameters;
            dataList = new List<FileSensorsData>();

            // Prepara timer que vai atualizar o gráfico a cada UPDATE_RATE ms
            timerCheckIncomeData = new Timer();
            timerCheckIncomeData.Interval = UPDATE_RATE;
            timerCheckIncomeData.Tick += new EventHandler(TickCheckIncomeData);

            // Prepara timer que vai esperar pelo primeiro dado
            timerWaitZeroMillis = new Timer();
            timerWaitZeroMillis.Interval = 100;
            timerWaitZeroMillis.Tick += new EventHandler(TickWaitZeroMillis);

            CarDataQueue = new ConcurrentQueue<SensorsData>();
        }

        public void StartUpdateCharts()
        {
            gotZeroMillis = false;
            timerWaitZeroMillis.Enabled = true;
        }

        private void TickWaitZeroMillis(object source, EventArgs e)
        {
            if (!gotZeroMillis && CarDataQueue.TryDequeue(out data))
            {
                // TODO simplificar / eliminar redundância ao parar o timer
                //      (flag booleana, enabled, stop, dispose e null....)
                gotZeroMillis = true;
                timerWaitZeroMillis.Enabled = false;
                timerWaitZeroMillis.Stop();
                timerWaitZeroMillis.Dispose();
                timerWaitZeroMillis = null;

                // salva millis de referência e inicia recepção dos dados seguintes
                zeroMillis = data.Millis;
                UpdateData(data);
                timerCheckIncomeData.Enabled = true;
            }
        }

        private void TickCheckIncomeData(object source, EventArgs e)
        {
            CheckNewData();
        }

        private void CheckNewData()
        {
            while(CarDataQueue.TryDequeue(out data))
            {
                UpdateData(data);
                UpdateTESTEformMillis(data.Millis - zeroMillis);
            }
        }

        private void UpdateData(SensorsData pNewData)
        {
            // --------------------- teste ---------------------
            //UpdateTESTEformMillis(newData.Millis);


            // TODO verificar necessidade de remover pontos não mais mostrados
            // acho que NÃO precisa... chartDinamic.Series[0].Points.RemoveAt(0);
            double currentMinimum = chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum;
            double currentMaximumX = chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum;
            double interval = chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval;

            // TODO capturar último X impresso no gráfico real
            //double lastX = chartDinamic.Series[0].Points.Last().XValue;
            //double lastX = previousMillis;
            //if (lastX >= currentMaximumX)
            if (pNewData.Millis >= currentMaximumX)
            {
                // TODO corrigir atualizar limites UpdateGraphLimits(currentMinimum, currentMaximumX, interval);
            }

            AddNewDataToGraph(pNewData);
            UpdateGauges(pNewData);
        }

        private void UpdateGauges(SensorsData pNewData)
        {
            aGaugeTemperature.Value = pNewData.EngineTemperature;
            aGaugeFuel.Value = pNewData.Fuel;
        }

        private void AddNewDataToGraph(SensorsData pNewData)
        {
            uint currentXValue = pNewData.Millis - zeroMillis;
            
            // ajusta valor do RPM para ficar proporcional à altura do gráfico
            double newRPMData = pNewData.RPM / (RPM_MAXIMUM / Y_AXIS_MAXIMUM);

            double brakePosition;
            if (pNewData.BreakState)
                brakePosition = (Y_AXIS_MAXIMUM / 2) + Y_AXIS_INTERVAL;
            else
                brakePosition = (Y_AXIS_MAXIMUM / 2) - Y_AXIS_INTERVAL;

            chartDinamic.Series["Speed"].Points.AddXY(currentXValue, pNewData.Speed);
            chartDinamic.Series["RPM"].Points.AddXY(currentXValue, newRPMData);
            chartDinamic.Series["Brake"].Points.AddXY(currentXValue, brakePosition);

            FileSensorsData fileNewData = new FileSensorsData
            {
                xValue = currentXValue,
                speed = pNewData.Speed,
                rpm = pNewData.RPM,
                breakState = pNewData.BreakState,
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
            
            UpdateXLabels();
            chartDinamic.Update();
        }

        // --------------- teste ------------------
        //private void UpdateTESTEform(SensorsData newData)
        //{
        //    formTesteMQSQ.SetData(this, newData);
        //}
        private void UpdateTESTEformMillis(uint millis)
        {
            formTesteMQSQ.SetMillis(this, millis, CarDataQueue.Count);
        }

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
