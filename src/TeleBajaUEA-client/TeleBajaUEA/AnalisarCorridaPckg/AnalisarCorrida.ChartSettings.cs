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
        // logo, havérão LABELS_INTERVAL_RATE+1 labels no gráfico
        // deve ner no Mínimo igual a ***** 3 *****!
        private readonly double X_SUBDIVISIONS = 9d;//7d;

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

        // Cores
        private readonly Color SPEED_COLOR = Color.Red;
        private readonly Color SPEED_MARKER_COLOR = Color.Gold;//Yellow;//OrangeRed;//Color.LightCoral;

        private readonly Color RPM_COLOR = Color.Yellow;
        private readonly Color RPM_MARKER_COLOR = Color.Red;//Orange;//Color.Gold;

        private readonly Color BRAKE_COLOR = Color.Green;
        private readonly Color BRAKE_MARKER_COLOR = Color.Lime;

        private readonly double AUX_SPEED_LINE_WIDTH = 1;
        private readonly Color AVG_SPEED_COLOR = Color.Orange;
        private readonly Color MAX_SPEED_COLOR = Color.Blue;
        private readonly Color MIN_SPEED_COLOR = Color.Olive;

        // -------------------- Outras Configurações ------------------------//
        private readonly double MIN_SELECTION_RANGE = 20000;

        // ------------------ Variáveis de controle interno ----------------//
        // variável para controlar o quanto os limites vão variar quando apertar "<" ou ">"
        //private double XIncreaseRate; // TODO verificar essa variável
        private double xInterval;
        private double ScaleViewSize
        {
            get { return xInterval * X_SUBDIVISIONS; }
            set { xInterval = value / X_SUBDIVISIONS; }
        }

        /// <summary>
        /// Encapsula configuração dos gráficos
        /// </summary>
        public void ConfigureCharts()
        {
            // configura barras de rolagem / zoom
            SetScrollBars();

            // configura fundo e linhas de apoio dos gráficos
            SetCommonProperties();
            UpdateMajorGrids();

            // configura linhas dos gráficos
            SetSeriesStyle();
            SetLegendsStyle();

            // configura marcadores (seleção de ponto)
            SetMarkerSeries();

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
        private void CreateStripLines(double pMaxSpeed, double pAvgSpeed)
        {
            StripLine maxSpeedLine = new StripLine();
            StripLine avgSpeedLine = new StripLine();

            maxSpeedLine.Interval = avgSpeedLine.Interval = 0;
            maxSpeedLine.StripWidth = avgSpeedLine.StripWidth = AUX_SPEED_LINE_WIDTH;

            maxSpeedLine.BackColor = MAX_SPEED_COLOR;
            avgSpeedLine.BackColor = AVG_SPEED_COLOR;

            // IntervalOffset é o valor das velocidades max, min e média, que serão setados em AnalisarCorrida
            maxSpeedLine.IntervalOffset = pMaxSpeed;
            avgSpeedLine.IntervalOffset = pAvgSpeed;

            chartsNew.ChartAreas["Speed"].AxisY.StripLines.Add(maxSpeedLine);
            chartsNew.ChartAreas["Speed"].AxisY.StripLines.Add(avgSpeedLine);

            chartsNew.Update();
        }

        private void SetSeriesStyle()
        {
            // Configurando a plotagem da velocidade
            chartsNew.Series["Speed"].Color = SPEED_COLOR;
            chartsNew.Series["Speed"].MarkerColor = SPEED_COLOR;
            chartsNew.Series["Speed"].BorderWidth = LINE_WIDTH;

            // Configurando a plotagem do RPM
            chartsNew.Series["RPM"].Color = RPM_COLOR;
            chartsNew.Series["RPM"].MarkerColor = RPM_COLOR;
            chartsNew.Series["RPM"].BorderWidth = LINE_WIDTH;

            // Configurando a plotagem do RPM
            chartsNew.Series["Brake"].Color = BRAKE_COLOR;
            chartsNew.Series["Brake"].MarkerColor = BRAKE_COLOR;
            chartsNew.Series["Brake"].BorderWidth = BRAKE_LINE_WIDTH;
        }

        private void SetMarkerSeries()
        {
            // Cores
            chartsNew.Series["SpeedMarker"].Color = SPEED_MARKER_COLOR;
            chartsNew.Series["RPMMarker"].Color = RPM_MARKER_COLOR;
            chartsNew.Series["BrakeMarker"].Color = BRAKE_MARKER_COLOR;

            // Configurações comum a todos
            foreach (string markerName in new List<string>() { "SpeedMarker", "RPMMarker", "BrakeMarker" })
            {
                chartsNew.Series[markerName].MarkerStyle = MarkerStyle.Circle;
                lastFocusedPoint[markerName] = new DataPoint(0, 0);
                chartsNew.Series[markerName].Points.Add(lastFocusedPoint[markerName]);
            }
        }

        private void SetXAxis()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // Configurando o X
                chartArea.AxisX.Minimum = 0;
                chartArea.AxisX.Maximum = LastPointXValue;
                chartArea.AxisX.Interval = xInterval;
            }

            // define labels do eixo X iniciais
            UpdateXLabels();
        }

        private void SetScrollBars()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // permitir usuário selecionar e dar zoom
                chartArea.CursorX.AutoScroll = true;
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.AxisX.ScaleView.Zoomable = true;

                // Configurando a aparência da scrollbar
                chartArea.AxisX.ScrollBar.BackColor = Color.FromArgb(64, 64, 64);
                chartArea.AxisX.ScrollBar.ButtonColor = Color.Silver;
                chartArea.AxisX.ScrollBar.Size = 16;
                chartArea.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;

                if(raceData.DataList.Last().xValue >= 20000)
                {
                    chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Number;
                    chartArea.AxisX.ScaleView.MinSize = 20000;
                }
            }
            // o gráfico exibirá a princípio todos os pontos e, então,
            // é dado um zoom inicial do tamanho mínimo predefinido
            chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.Zoom(0, INITIAL_VIEW_SIZE, DateTimeIntervalType.Number, true);
            zoomedAreaStack.Push(chartsNew.ChartAreas[0]);
            ScaleViewSize = INITIAL_VIEW_SIZE;
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

        private void SetCommonProperties()
        {
            foreach (ChartArea chartArea in chartsNew.ChartAreas)
            {
                // Cor de fundo
                chartArea.BackColor = Color.Black;

                // Cor das linhas de trás/apoio (grid)
                chartArea.AxisY.MajorGrid.LineColor = GRID_COLOR;
                chartArea.AxisX.MajorGrid.LineColor = GRID_COLOR;

                // Intervalo entre as linhas de trás/apoio/fundo (grid)
                chartArea.AxisX.MajorGrid.Interval = xInterval;
            }
        }

        #endregion

        //-----------------------------------------------------------------------
        #region Funções de atualização, chamadas sempre que o zoom/scroll/view mudar
        
        // TODO botar os COLORS fora daqui, em algum Set...()
        private void UpdateMajorGrids()
        {
            foreach(ChartArea chartArea in chartsNew.ChartAreas)
            {
                // Intervalo entre as linhas de trás/apoio/fundo (grid)
                chartArea.AxisX.MajorGrid.Interval = xInterval;
                chartArea.AxisX.MajorTickMark.Interval = xInterval;
            }
        }

        private void UpdateXLabels()
        {
            chartsNew.ChartAreas[XLabelChartArea].AxisX.CustomLabels.Clear();

            double startPointX = chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.ViewMinimum;
            double endPointX = chartsNew.ChartAreas[XLabelChartArea].AxisX.ScaleView.ViewMaximum;

            double fromPosition, toPosition;
            string text;
            for (double currentXLabel = startPointX; currentXLabel <= endPointX; currentXLabel += xInterval)
            {
                fromPosition = currentXLabel - 5 * (xInterval / 10); // TODO trocar de 10 para UPDATE_RATE... ?
                toPosition = currentXLabel + 5 * (xInterval / 10);

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
