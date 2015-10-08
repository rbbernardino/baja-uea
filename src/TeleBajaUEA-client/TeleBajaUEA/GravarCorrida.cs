﻿using System;
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
            UpdateTESTEform(newData);

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
        }

        private void UpdateGraphLimits(double currentMinimum,
                                        double currentMaximumX, double _interval)
        {
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum =
                currentMinimum + UPDATE_LIMITS_INTERVAL;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum =
                currentMaximumX + UPDATE_LIMITS_INTERVAL;
            UpdateLabels();
            chartDinamic.Update();
        }

        private void UpdateTESTEform(SensorsData newData)
        {
            formTesteMQSQ.SetData(this, newData);
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
