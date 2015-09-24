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
    public partial class GravarCorrida
    {
        private readonly static int UPDATE_RATE = 50;

        // -------------------- Configurações do eixo X ---------------------//
        private readonly double X_AXIS_MINIMUM = 0;
        private readonly double X_AXIS_MAXIMUM = 50;
        private readonly double X_AXIS_INTERVAL = 10;

        private readonly double X_AXIS_GRID_INTERVAL = 10;

        // -------------------- Configurações do eixo Y ---------------------//
        private readonly double Y_AXIS_MINIMUM = 0;
        private readonly double Y_AXIS_MAXIMUM = 60;
        private readonly double Y_AXIS_INTERVAL = 10;

        private readonly double Y_AXIS_GRID_INTERVAL = 10;

        // -------------------- Configurações de Cor ---------------------//
        private readonly Color BACKGROUND_COLOR = Color.Black;
        private readonly Color GRID_COLOR = ColorTranslator.FromHtml("#686868");
        private readonly Color SPEED_COLOR = Color.Red;

        // -------------------- Configurações de Linha ------------------//
        private readonly int SPEED_LINE_WIDTH = 1;

        /// <summary>
        /// Encapsula configuração dos gráficos
        /// </summary>
        public async Task ConfigureCharts()
        {
            await Task.Run(() =>
            {
                chartDinamic.Legends["Legend1"].Title = "Legend";
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

                // linhas de trás/apoio
                chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.Interval = X_AXIS_GRID_INTERVAL;
                chartDinamic.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = GRID_COLOR;

                // Configurando a plotagem da velocidade
                chartDinamic.Series["Speed"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
                chartDinamic.Series["Speed"].Color = SPEED_COLOR;
                chartDinamic.Series["Speed"].BorderWidth = SPEED_LINE_WIDTH;
            });
        }
    }
}
