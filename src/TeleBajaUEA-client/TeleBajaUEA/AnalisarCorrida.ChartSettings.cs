using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace TeleBajaUEA
{
    public partial class AnalisarCorrida
    {
        // -------------------- Configurações do eixo X ---------------------//
        // TODO unificar com os valores do GravarCorrida em um arquivo de config
        // valores em segundos
        // sugestão de config.:
        //     máximo como múltiplo de 30    (60, 90, 120...)
        //     intervalo como múltiplo de 10 (10, 20, 30...)
        private readonly double X_AXIS_MINIMUM = 0;
        private readonly double X_AXIS_MAXIMUM = 300; // 300 = 5min
        private readonly double INCREASE_LIMITS_INTERVAL = 300;

        private readonly double X_AXIS_INTERVAL = 50;
        private readonly double X_AXIS_GRID_INTERVAL = 50;


        // -------------------- Configurações do eixo Y ---------------------//
        private readonly double SPEED_MINIMUM = 0;
        private readonly double SPEED_MAXIMUM = 80; // velocidade máxima
        private readonly double SPEED_Y_INTERVAL = 20;

        private readonly double RPM_MINIMUM = 0;
        private readonly double RPM_MAXIMUM = 3000; // RPM máximo
        private readonly double RPM_Y_INTERVAL = 600;

        private readonly double BRAKE_MINIMUM = 0;
        private readonly double BRAKE_MAXIMUM = 100; // BRAKE máximo
        private readonly double BRAKE_Y_INTERVAL = 25;

        // Define qual ChartArea exibirá o eixo Y
        private readonly string XLabelChartArea = "Brake";

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
        private long minX;
        private long maxX;

        /// <summary>
        /// Encapsula configuração dos gráficos
        /// </summary>
        public async Task ConfigureCharts()
        {
            await Task.Run(() =>
            {
                minX = (long) X_AXIS_MINIMUM;
                maxX = (long) X_AXIS_MAXIMUM;

                // configura fundo e linhas de apoio dos gráficos
                foreach (ChartArea chartArea in chartsNew.ChartAreas)
                    SetChartsShared(chartArea);

                // configura linhas dos gráficos
                SetSeriesStyle();

                // define labels do eixo X iniciais
                UpdateXLabels();

                // define os valores e intervalos do eixo Y
                SetYAxisValues();
                
                // define labels de título do eixo Y
                SetYAxisTitle("Speed", SPEED_MINIMUM, SPEED_MAXIMUM, "Velocidade");
                SetYAxisTitle("RPM",   RPM_MINIMUM,   RPM_MAXIMUM,   "RPM");
                SetYAxisTitle("Brake", BRAKE_MINIMUM, BRAKE_MAXIMUM, "Freio");
            });
        }

        private void SetSeriesStyle()
        {
            // Configurando a plotagem da velocidade
            chartsNew.Series["Speed"].ChartType = SeriesChartType.FastLine;
            chartsNew.Series["Speed"].Color = SPEED_COLOR;
            chartsNew.Series["Speed"].BorderWidth = LINE_WIDTH;

            // Configurando a plotagem do RPM
            chartsNew.Series["RPM"].ChartType = SeriesChartType.FastLine;
            chartsNew.Series["RPM"].Color = RPM_COLOR;
            chartsNew.Series["RPM"].BorderWidth = LINE_WIDTH;

            // Configurando a plotagem do RPM
            chartsNew.Series["Brake"].ChartType = SeriesChartType.FastLine;
            chartsNew.Series["Brake"].Color = BRAKE_COLOR;
            chartsNew.Series["Brake"].BorderWidth = BRAKE_LINE_WIDTH;
        }

        // Aplica as configurações que são comuns a todas as CharAreas
        private void SetChartsShared(ChartArea chartArea)
        {
            chartArea.BackColor = Color.Black;

            // linhas de trás/apoio (grid)
            chartArea.AxisY.MajorGrid.LineColor = GRID_COLOR;

            // Configurando o X
            chartArea.AxisX.Minimum = X_AXIS_MINIMUM;
            chartArea.AxisX.Maximum = X_AXIS_MAXIMUM;
            chartArea.AxisX.Interval = X_AXIS_INTERVAL;

            // linhas de trás/apoio/fundo
            chartArea.AxisX.MajorGrid.Interval = X_AXIS_GRID_INTERVAL;
            chartArea.AxisX.MajorGrid.LineColor = GRID_COLOR;
        }

        // TODO fazer como no SetYAxisTitle? Vai reduzir bastante linhas?
        private void SetYAxisValues()
        {
            // Configurando os limites e intervales dos eixos Y de cada g[rafico
            chartsNew.ChartAreas["Speed"].AxisY.Minimum = SPEED_MINIMUM;
            chartsNew.ChartAreas["Speed"].AxisY.Maximum = SPEED_MAXIMUM;
            chartsNew.ChartAreas["Speed"].AxisY.Interval = SPEED_Y_INTERVAL;
            chartsNew.ChartAreas["Speed"].AxisY.MajorGrid.Interval = SPEED_Y_INTERVAL/2;

            chartsNew.ChartAreas["RPM"].AxisY.Minimum = RPM_MINIMUM;
            chartsNew.ChartAreas["RPM"].AxisY.Maximum = RPM_MAXIMUM;
            chartsNew.ChartAreas["RPM"].AxisY.Interval = RPM_Y_INTERVAL;
            chartsNew.ChartAreas["RPM"].AxisY.MajorGrid.Interval = RPM_Y_INTERVAL;

            chartsNew.ChartAreas["Brake"].AxisY.Minimum = BRAKE_MINIMUM;
            chartsNew.ChartAreas["Brake"].AxisY.Maximum = BRAKE_MAXIMUM;
            chartsNew.ChartAreas["Brake"].AxisY.Interval = BRAKE_Y_INTERVAL;
            chartsNew.ChartAreas["Brake"].AxisY.MajorGrid.Interval = 25;

            //chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(65, 85, "ON", 0, LabelMarkStyle.None);
            //chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(15, 35, "OFF", 0, LabelMarkStyle.None);
            chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(BRAKE_MAXIMUM / 2, BRAKE_MAXIMUM, "ON", 0, LabelMarkStyle.None);
            chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(0, BRAKE_MAXIMUM/2, "OFF", 0, LabelMarkStyle.None);
        }

        // labels laterais, escritas na vertical
        private void SetYAxisTitle(string name, double fromPosition, double toPosition, string text)
        {
            CustomLabel label = new CustomLabel()
            {
                FromPosition = fromPosition,
                ToPosition   = toPosition,
                Text = text,
                RowIndex = 1,
                LabelMark = LabelMarkStyle.None
            };

            chartsNew.ChartAreas[name].AxisY.CustomLabels.Add(label);
        }

        private void UpdateXLabels()
        {
            chartsNew.ChartAreas[XLabelChartArea].AxisX.CustomLabels.Clear();

            long fromPosition, toPosition;
            string text;
            for (long currentXLabel = minX;
                currentXLabel <= maxX; currentXLabel += (long)X_AXIS_INTERVAL)
            {
                fromPosition = currentXLabel - 5 * ((long)X_AXIS_INTERVAL / 10); // TODO trocar de 10 para UPDATE_RATE...
                toPosition = currentXLabel + 5 * ((long)X_AXIS_INTERVAL / 10);
                text = SecondsToTime(currentXLabel);

                chartsNew.ChartAreas[XLabelChartArea].AxisX.CustomLabels.Add(fromPosition, toPosition, text);
            }
        }

        private string FormatFuelNumbers(float number)
        {
            switch ((int)number)
            {
                case 0:
                    return ""; // fica no CapsText
                case 25:
                    return "";
                case 50:
                    return "1/2";
                case 75:
                    return "";
                default:
                    return "F";
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
