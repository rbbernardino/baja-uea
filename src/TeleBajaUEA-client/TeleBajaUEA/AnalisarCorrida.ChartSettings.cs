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
        private readonly double INITIAL_VIEW_SIZE = 300 * 1000; // 300 = 5min

        // indica em quantas partes o eixo X será dividido para mostar as labels
        // logo, havérão LABELS_INTERVAL_RATE-1 labels no gráfico
        private readonly double LABELS_INTERVAL_RATE = 7d;

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

        private readonly double AUX_SPEED_LINE_WIDTH = 1;
        private readonly Color AVG_SPEED_COLOR = Color.Orange;
        private readonly Color MAX_SPEED_COLOR = Color.Blue;
        private readonly Color MIN_SPEED_COLOR = Color.Olive;

        // ------------------ Variáveis de controle interno ----------------//
        // variável para controlar o quanto os limites vão variar quando apertar "<" ou ">"
        //private double XIncreaseRate; // TODO verificar essa variável
        private double XInterval;
        private double scaleViewSize;

        /// <summary>
        /// Encapsula configuração dos gráficos
        /// </summary>
        public void ConfigureCharts()
        {
            scaleViewSize = INITIAL_VIEW_SIZE;

            // configura barras de rolagem / zoom
            SetScrollBars();

            // configura fundo e linhas de apoio dos gráficos
            UpdateAllCharts();

            // configura linhas dos gráficos
            SetSeriesStyle();
            SetLegendsStyle();

            // define valores, intervalos e labels dos eixos
            SetXAxis();
            SetYAxis();
        }

        #region Funções de setup (configuração inicial) {...}
        private void SetLegendsStyle()
        {
            foreach (LegendItem leg in chartsNew.Legends[0].CustomItems)
            {
                if (leg.Name == "min") leg.Color = MIN_SPEED_COLOR;
                if (leg.Name == "med") leg.Color = AVG_SPEED_COLOR;
                if (leg.Name == "max") leg.Color = MAX_SPEED_COLOR;
            }
        }

        // essa função é chamada no analisar corrida ao inserir os pontos
        private void CreateStripLines(double pMaxSpeed, double pAvgSpeed, double pMinSpeed)
        {
            StripLine maxSpeedLine = new StripLine();
            StripLine avgSpeedLine = new StripLine();
            StripLine minSpeedLine = new StripLine();

            maxSpeedLine.Interval = avgSpeedLine.Interval = minSpeedLine.Interval = 0;
            maxSpeedLine.StripWidth = avgSpeedLine.StripWidth = minSpeedLine.StripWidth = AUX_SPEED_LINE_WIDTH;

            maxSpeedLine.BackColor = MAX_SPEED_COLOR;
            avgSpeedLine.BackColor = AVG_SPEED_COLOR;
            minSpeedLine.BackColor = MIN_SPEED_COLOR;

            // IntervalOffset é o valor das velocidades max, min e média, que serão setados em AnalisarCorrida
            maxSpeedLine.IntervalOffset = pMaxSpeed;
            avgSpeedLine.IntervalOffset = pAvgSpeed;
            minSpeedLine.IntervalOffset = pMinSpeed;

            chartsNew.ChartAreas["Speed"].AxisY.StripLines.Add(maxSpeedLine);
            chartsNew.ChartAreas["Speed"].AxisY.StripLines.Add(avgSpeedLine);
            chartsNew.ChartAreas["Speed"].AxisY.StripLines.Add(minSpeedLine);

            chartsNew.Update();
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

        private void SetXAxis()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // Configurando o X
                chartArea.AxisX.Minimum = 0;
                chartArea.AxisX.Maximum = DataSize;
                chartArea.AxisX.Interval = XInterval;
            }

            // define labels do eixo X iniciais
            UpdateXLabels();
        }

        private void SetScrollBars()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // permitir usuário selecionar para dar zoom na seleção
                chartArea.CursorX.AutoScroll = true;
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;

                chartArea.AxisX.ScaleView.Zoomable = true;

                // Configurando a aparência da scrollbar
                chartArea.AxisX.ScrollBar.BackColor = Color.FromArgb(64, 64, 64);
                chartArea.AxisX.ScrollBar.ButtonColor = Color.Silver;
                chartArea.AxisX.ScrollBar.Size = 16;
                chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;

                // o gráfico exibirá a princípio todos os pontos e, então,
                // é dado um zoom inicial do tamanho predefinido
                chartArea.AxisX.Maximum = DataSize;
                chartArea.AxisX.ScaleView.Zoom(0, INITIAL_VIEW_SIZE, DateTimeIntervalType.Number, true);
            }
        }

        // TODO fazer como no SetYAxisTitle? Vai reduzir bastante linhas?
        private void SetYAxis()
        {
            // Configurando os limites e intervales dos eixos Y de cada grafico
            chartsNew.ChartAreas["Speed"].AxisY.Minimum = SPEED_MINIMUM;
            chartsNew.ChartAreas["Speed"].AxisY.Maximum = SPEED_MAXIMUM;
            chartsNew.ChartAreas["Speed"].AxisY.Interval = SPEED_Y_INTERVAL;
            chartsNew.ChartAreas["Speed"].AxisY.MajorGrid.Interval = SPEED_Y_INTERVAL / 2;

            chartsNew.ChartAreas["RPM"].AxisY.Minimum = RPM_MINIMUM;
            chartsNew.ChartAreas["RPM"].AxisY.Maximum = RPM_MAXIMUM;
            chartsNew.ChartAreas["RPM"].AxisY.Interval = RPM_Y_INTERVAL;
            chartsNew.ChartAreas["RPM"].AxisY.MajorGrid.Interval = RPM_Y_INTERVAL;

            chartsNew.ChartAreas["Brake"].AxisY.Minimum = BRAKE_MINIMUM;
            chartsNew.ChartAreas["Brake"].AxisY.Maximum = BRAKE_MAXIMUM;
            chartsNew.ChartAreas["Brake"].AxisY.Interval = BRAKE_Y_INTERVAL;
            chartsNew.ChartAreas["Brake"].AxisY.MajorGrid.Interval = 25;

            chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(BRAKE_MAXIMUM / 2, BRAKE_MAXIMUM, "ON", 0, LabelMarkStyle.None);
            chartsNew.ChartAreas["Brake"].AxisY.CustomLabels.Add(0, BRAKE_MAXIMUM / 2, "OFF", 0, LabelMarkStyle.None);

            // define labels de título do eixo Y
            SetYAxisTitle("Speed", SPEED_MINIMUM, SPEED_MAXIMUM, "Velocidade");
            SetYAxisTitle("RPM", RPM_MINIMUM, RPM_MAXIMUM, "RPM");
            SetYAxisTitle("Brake", BRAKE_MINIMUM, BRAKE_MAXIMUM, "Freio");
        }

        // labels laterais, escritas na vertical
        private void SetYAxisTitle(string name, double fromPosition, double toPosition, string text)
        {
            CustomLabel label = new CustomLabel()
            {
                FromPosition = fromPosition,
                ToPosition = toPosition,
                Text = text,
                RowIndex = 1,
                LabelMark = LabelMarkStyle.None
            };

            chartsNew.ChartAreas[name].AxisY.CustomLabels.Add(label);
        }

        #endregion

        //-----------------------------------------------------------------------
        #region Funções de atualização, chamadas sempre que o zoom/scroll/view mudar
        
        // TODO botar os COLORS fora daqui, em algum Set...()
        private void UpdateAllCharts()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // Cor de fundo
                chartArea.BackColor = Color.Black;

                // Cor das linhas de trás/apoio (grid)
                chartArea.AxisY.MajorGrid.LineColor = GRID_COLOR;
                chartArea.AxisX.MajorGrid.LineColor = GRID_COLOR;

                // Intervalo entre as linhas de trás/apoio/fundo (grid)
                XInterval = chartArea.AxisX.ScaleView.Size / LABELS_INTERVAL_RATE;
                chartArea.AxisX.MajorGrid.Interval = XInterval;
            }
        }

        private void UpdateXLabels()
        {
            chartsNew.ChartAreas[XLabelChartArea].AxisX.CustomLabels.Clear();

            double startPointX = chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.ViewMinimum;
            double endPointX = chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.ViewMaximum;

            double fromPosition, toPosition;
            string text;
            for (double currentXLabel = startPointX; currentXLabel <= endPointX; currentXLabel += XInterval)
            {
                fromPosition = currentXLabel - 5 * (XInterval / 10); // TODO trocar de 10 para UPDATE_RATE...
                toPosition = currentXLabel + 5 * (XInterval / 10);

                // contagem é feita em milisegundos
                text = SecondsToTime((long)currentXLabel / 1000);

                chartsNew.ChartAreas[XLabelChartArea].AxisX.CustomLabels.Add(fromPosition, toPosition, text);
            }
        }
        #endregion

        //-----------------------------------------------------------------------
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
