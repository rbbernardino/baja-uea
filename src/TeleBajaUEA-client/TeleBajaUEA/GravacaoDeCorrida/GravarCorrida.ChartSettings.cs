﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Windows.Forms.DataVisualization.Charting;

namespace TeleBajaUEA.GravacaoDeCorrida
{
    public partial class GravarCorrida
    {
        private readonly static int UPDATE_RATE = 150; // TODO acrescentar valor do ping?
        private readonly static int UPDATE_BACKUP_RATE = 1000;//3 *(60*1000); // TODO configurar nas configs

        // -------------------- Configurações do eixo X ---------------------//
        // valores em segundos
        // sugestão de config.:
        //     máximo como múltiplo de 30    (60, 90, 120...)
        //     intervalo como múltiplo de 10 (10, 20, 30...)
        private readonly double X_AXIS_MINIMUM = 0;
        private readonly double X_AXIS_MAXIMUM = 300 *1000; // 300 = 5min

        private readonly double X_AXIS_INTERVAL = 60 *1000;
        private readonly double X_AXIS_GRID_INTERVAL = 60 *1000;

        // define quantos pontos mover para a direita quando pontos plotados
        // atingirem o limite de plotagem (na direita)
        private readonly double UPDATE_LIMITS_INTERVAL = 300 *1000; // 300 = 5min

        // -------------------- Configurações do eixo Y ---------------------//
        private readonly double Y_AXIS_MINIMUM = 0;
        private readonly double Y_AXIS_MAXIMUM = 80; // velocidade máxima
        private readonly double RPM_MAXIMUM = 3000; // RPM máximo
        private readonly double Y_AXIS_INTERVAL = 10;

        private readonly double Y_AXIS_GRID_INTERVAL = 10;

        // -------------------- Configurações de Cor do fundo ---------------------//
        private readonly Color BACKGROUND_COLOR = Color.Black;
        private readonly Color GRID_COLOR = ColorTranslator.FromHtml("#686868");

        // -------------------- Configurações de Linha ------------------//
        private readonly int LINE_WIDTH = 1;
        private readonly int BRAKE_LINE_WIDTH = 2;
        private readonly Color SPEED_COLOR = Color.Red;
        private readonly Color RPM_COLOR = Color.Yellow;
        private readonly Color BRAKE_COLOR = Color.Green;

        // ------------------ Variáveis de controle interno ----------------//
        private double minX;
        private double maxX;

        /// <summary>
        /// Encapsula configuração dos gráficos
        /// </summary>
        public async Task ConfigureCharts()
        {
            await Task.Run(() =>
            {
                minX = X_AXIS_MINIMUM;
                maxX = X_AXIS_MAXIMUM;

                //chartDinamic.Legends["Legend1"].Title = "Legend";
                chartDinamic.ChartAreas["ChartArea1"].BackColor = Color.Black;

                // Configurando o Y
                chartDinamic.ChartAreas["ChartArea1"].AxisY.Minimum = Y_AXIS_MINIMUM;
                chartDinamic.ChartAreas["ChartArea1"].AxisY.Maximum = Y_AXIS_MAXIMUM;
                chartDinamic.ChartAreas["ChartArea1"].AxisY.Interval = Y_AXIS_INTERVAL;

                // linhas de trás/apoio (grid)
                chartDinamic.ChartAreas["ChartArea1"].AxisY.MajorGrid.Interval = Y_AXIS_GRID_INTERVAL;
                chartDinamic.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = GRID_COLOR;

                // Configurando o X
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = X_AXIS_MINIMUM;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = X_AXIS_MAXIMUM;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval = X_AXIS_INTERVAL;

                // linhas de trás/apoio/fundo
                chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = X_AXIS_GRID_INTERVAL;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = GRID_COLOR;

                // Configurando a plotagem da velocidade
                chartDinamic.Series["Speed"].ChartType = SeriesChartType.FastLine;
                chartDinamic.Series["Speed"].Color = SPEED_COLOR;
                chartDinamic.Series["Speed"].BorderWidth = LINE_WIDTH;

                // Configurando a plotagem do RPM
                chartDinamic.Series["RPM"].ChartType = SeriesChartType.FastLine;
                chartDinamic.Series["RPM"].Color = RPM_COLOR;
                chartDinamic.Series["RPM"].BorderWidth = LINE_WIDTH;

                // Configurando a plotagem do RPM
                chartDinamic.Series["Brake"].ChartType = SeriesChartType.FastLine;
                chartDinamic.Series["Brake"].Color = BRAKE_COLOR;
                chartDinamic.Series["Brake"].BorderWidth = BRAKE_LINE_WIDTH;

                // define labels do X e Y iniciais
                UpdateXLabels();
                SetYLabels();
            });
        }

        public void SetSeriesDisabled()
        {
            chartDinamic.Series.ToList().ForEach(s => s.Color = Color.Gray);
        }

        private void SetYLabels()
        {
            // TODO automatiza posicionamento das labels com as constantes V V
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(-5, 5, "0rpm -------- 0km/h", 0, LabelMarkStyle.None);
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(35, 45, RPM_MAXIMUM / 2 + "rpm -------- " + Y_AXIS_MAXIMUM / 2 + "km/h", 0, LabelMarkStyle.None);
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(75, 85, RPM_MAXIMUM + "rpm -------- " + Y_AXIS_MAXIMUM + "km/h", 0, LabelMarkStyle.None);
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(45, 55, "Break ON--------", 0, LabelMarkStyle.None);
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(25, 35, "Break OFF--------", 0, LabelMarkStyle.None);

            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(15, 25, 2*RPM_MAXIMUM / 8 + "rpm -------- " + 2*Y_AXIS_MAXIMUM/8 + "km/h", 0, LabelMarkStyle.None);
            chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(55, 65, 6*RPM_MAXIMUM/8 + "rpm -------- " + 6*Y_AXIS_MAXIMUM /8 + "km/h", 0, LabelMarkStyle.None);
            //750, 2200

            //TODO melhorar exibição de labels do Y --->------>---------->-------->-----ROW---V
            //   chartDinamic.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(65, 55, RPM_MAXIMUM + "", 2, LabelMarkStyle.None);
        }

        private void UpdateXLabels()
        {
            chartDinamic.ChartAreas["ChartArea1"].AxisX.CustomLabels.Clear();

            double fromPosition, toPosition;
            string text;
            for (double currentXLabel = minX;
                currentXLabel <= maxX; currentXLabel += X_AXIS_INTERVAL)
            {
                fromPosition = currentXLabel - 5*(X_AXIS_INTERVAL /10);
                toPosition = currentXLabel + 5*(X_AXIS_INTERVAL/10);
                
                // contagem é feita em milisegundos
                text = SecondsToTime((long) currentXLabel/1000);

                chartDinamic.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(fromPosition, toPosition, text);
            }
        }

        private string SecondsToTime(long totalSeconds)
        {
            long seconds = totalSeconds % 60;
            string secondsStr = seconds.ToString();

            long totalMinutes = totalSeconds / 60;
            long minutes = totalMinutes % 60;
            string minutesStr = minutes.ToString();

            long hours = totalMinutes / 60;
            string hoursStr = hours.ToString();

            if (hours < 10) hoursStr = "0" + hours;
            if (minutes < 10) minutesStr = "0" + minutes;
            if (seconds < 10) secondsStr = "0" + seconds;

            return hoursStr + ":" + minutesStr + ":" + secondsStr;
        }
    }
}
