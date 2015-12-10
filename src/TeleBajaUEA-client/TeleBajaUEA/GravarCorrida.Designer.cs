namespace TeleBajaUEA
{
    partial class GravarCorrida
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GravarCorrida));
            this.chartDinamic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.aGaugeTemperature = new TeleBajaUEA.AGauge();
            this.aGaugeFuel = new TeleBajaUEA.AGauge();
            this.btEncerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartDinamic)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDinamic
            // 
            this.chartDinamic.BackColor = System.Drawing.Color.Transparent;
            this.chartDinamic.BorderlineWidth = 3;
            chartArea1.Name = "ChartArea1";
            this.chartDinamic.ChartAreas.Add(chartArea1);
            this.chartDinamic.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chartDinamic.Legends.Add(legend1);
            this.chartDinamic.Location = new System.Drawing.Point(0, 0);
            this.chartDinamic.Name = "chartDinamic";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "Brake";
            this.chartDinamic.Series.Add(series1);
            this.chartDinamic.Series.Add(series2);
            this.chartDinamic.Series.Add(series3);
            this.chartDinamic.Size = new System.Drawing.Size(1064, 475);
            this.chartDinamic.TabIndex = 1;
            this.chartDinamic.Text = "chart1";
            // 
            // aGaugeTemperature
            // 
            this.aGaugeTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.aGaugeTemperature.BaseArcColor = System.Drawing.Color.Gray;
            this.aGaugeTemperature.BaseArcRadius = 40;
            this.aGaugeTemperature.BaseArcStart = 0;
            this.aGaugeTemperature.BaseArcSweep = -90;
            this.aGaugeTemperature.BaseArcWidth = 2;
            this.aGaugeTemperature.Cap_Idx = ((byte)(1));
            this.aGaugeTemperature.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.aGaugeTemperature.CapPosition = new System.Drawing.Point(10, 85);
            this.aGaugeTemperature.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 40),
        new System.Drawing.Point(10, 85),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(35, 65)};
            this.aGaugeTemperature.CapsText = new string[] {
        "H",
        "Temperature",
        "",
        "",
        "C"};
            this.aGaugeTemperature.CapText = "Temperature";
            this.aGaugeTemperature.Center = new System.Drawing.Point(15, 70);
            this.aGaugeTemperature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aGaugeTemperature.Location = new System.Drawing.Point(938, 340);
            this.aGaugeTemperature.MaxValue = 300F;
            this.aGaugeTemperature.MinValue = 60F;
            this.aGaugeTemperature.Name = "aGaugeTemperature";
            this.aGaugeTemperature.NeedleColor1 = TeleBajaUEA.AGauge.NeedleColorEnum.Gray;
            this.aGaugeTemperature.NeedleColor2 = System.Drawing.Color.Black;
            this.aGaugeTemperature.NeedleRadius = 40;
            this.aGaugeTemperature.NeedleType = 0;
            this.aGaugeTemperature.NeedleWidth = 2;
            this.aGaugeTemperature.Range_Idx = ((byte)(0));
            this.aGaugeTemperature.RangeColor = System.Drawing.Color.DeepSkyBlue;
            this.aGaugeTemperature.RangeEnabled = true;
            this.aGaugeTemperature.RangeEndValue = 100F;
            this.aGaugeTemperature.RangeInnerRadius = 32;
            this.aGaugeTemperature.RangeOuterRadius = 40;
            this.aGaugeTemperature.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.DeepSkyBlue,
        System.Drawing.Color.Red,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.aGaugeTemperature.RangesEnabled = new bool[] {
        true,
        true,
        false,
        false,
        false};
            this.aGaugeTemperature.RangesEndValue = new float[] {
        100F,
        304F,
        0F,
        0F,
        0F};
            this.aGaugeTemperature.RangesInnerRadius = new int[] {
        32,
        32,
        0,
        0,
        0};
            this.aGaugeTemperature.RangesOuterRadius = new int[] {
        40,
        40,
        0,
        0,
        0};
            this.aGaugeTemperature.RangesStartValue = new float[] {
        50F,
        250F,
        0F,
        0F,
        0F};
            this.aGaugeTemperature.RangeStartValue = 50F;
            this.aGaugeTemperature.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGaugeTemperature.ScaleLinesInterInnerRadius = 45;
            this.aGaugeTemperature.ScaleLinesInterOuterRadius = 50;
            this.aGaugeTemperature.ScaleLinesInterWidth = 2;
            this.aGaugeTemperature.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGaugeTemperature.ScaleLinesMajorInnerRadius = 40;
            this.aGaugeTemperature.ScaleLinesMajorOuterRadius = 50;
            this.aGaugeTemperature.ScaleLinesMajorStepValue = 60F;
            this.aGaugeTemperature.ScaleLinesMajorWidth = 2;
            this.aGaugeTemperature.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.aGaugeTemperature.ScaleLinesMinorInnerRadius = 45;
            this.aGaugeTemperature.ScaleLinesMinorNumOf = 4;
            this.aGaugeTemperature.ScaleLinesMinorOuterRadius = 50;
            this.aGaugeTemperature.ScaleLinesMinorWidth = 1;
            this.aGaugeTemperature.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGaugeTemperature.ScaleNumbersFormat = null;
            this.aGaugeTemperature.ScaleNumbersFormatter = null;
            this.aGaugeTemperature.ScaleNumbersRadius = 62;
            this.aGaugeTemperature.ScaleNumbersRotation = 90;
            this.aGaugeTemperature.ScaleNumbersStartScaleLine = 1;
            this.aGaugeTemperature.ScaleNumbersStepScaleLines = 2;
            this.aGaugeTemperature.Size = new System.Drawing.Size(92, 107);
            this.aGaugeTemperature.TabIndex = 17;
            this.aGaugeTemperature.Text = "aGaugeTemperature";
            this.aGaugeTemperature.Value = 60F;
            // 
            // aGaugeFuel
            // 
            this.aGaugeFuel.BackColor = System.Drawing.SystemColors.Control;
            this.aGaugeFuel.BaseArcColor = System.Drawing.Color.Gray;
            this.aGaugeFuel.BaseArcRadius = 40;
            this.aGaugeFuel.BaseArcStart = 180;
            this.aGaugeFuel.BaseArcSweep = 90;
            this.aGaugeFuel.BaseArcWidth = 2;
            this.aGaugeFuel.Cap_Idx = ((byte)(1));
            this.aGaugeFuel.CapColors = new System.Drawing.Color[] {
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black,
        System.Drawing.Color.Black};
            this.aGaugeFuel.CapPosition = new System.Drawing.Point(20, 85);
            this.aGaugeFuel.CapsPosition = new System.Drawing.Point[] {
        new System.Drawing.Point(10, 14),
        new System.Drawing.Point(20, 85),
        new System.Drawing.Point(10, 10),
        new System.Drawing.Point(5, 5),
        new System.Drawing.Point(5, 60)};
            this.aGaugeFuel.CapsText = new string[] {
        "",
        "Fuel",
        "",
        "",
        "E"};
            this.aGaugeFuel.CapText = "Fuel";
            this.aGaugeFuel.Center = new System.Drawing.Point(70, 70);
            this.aGaugeFuel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aGaugeFuel.Location = new System.Drawing.Point(935, 178);
            this.aGaugeFuel.MaxValue = 100F;
            this.aGaugeFuel.MinValue = 0F;
            this.aGaugeFuel.Name = "aGaugeFuel";
            this.aGaugeFuel.NeedleColor1 = TeleBajaUEA.AGauge.NeedleColorEnum.Gray;
            this.aGaugeFuel.NeedleColor2 = System.Drawing.Color.Black;
            this.aGaugeFuel.NeedleRadius = 40;
            this.aGaugeFuel.NeedleType = 0;
            this.aGaugeFuel.NeedleWidth = 2;
            this.aGaugeFuel.Range_Idx = ((byte)(0));
            this.aGaugeFuel.RangeColor = System.Drawing.Color.LightGreen;
            this.aGaugeFuel.RangeEnabled = false;
            this.aGaugeFuel.RangeEndValue = 0F;
            this.aGaugeFuel.RangeInnerRadius = 0;
            this.aGaugeFuel.RangeOuterRadius = 0;
            this.aGaugeFuel.RangesColor = new System.Drawing.Color[] {
        System.Drawing.Color.LightGreen,
        System.Drawing.Color.Red,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.Control};
            this.aGaugeFuel.RangesEnabled = new bool[] {
        false,
        true,
        false,
        false,
        false};
            this.aGaugeFuel.RangesEndValue = new float[] {
        0F,
        426F,
        0F,
        0F,
        0F};
            this.aGaugeFuel.RangesInnerRadius = new int[] {
        0,
        32,
        0,
        0,
        0};
            this.aGaugeFuel.RangesOuterRadius = new int[] {
        0,
        40,
        0,
        0,
        0};
            this.aGaugeFuel.RangesStartValue = new float[] {
        -100F,
        400F,
        0F,
        0F,
        0F};
            this.aGaugeFuel.RangeStartValue = -100F;
            this.aGaugeFuel.ScaleLinesInterColor = System.Drawing.Color.Black;
            this.aGaugeFuel.ScaleLinesInterInnerRadius = 44;
            this.aGaugeFuel.ScaleLinesInterOuterRadius = 50;
            this.aGaugeFuel.ScaleLinesInterWidth = 1;
            this.aGaugeFuel.ScaleLinesMajorColor = System.Drawing.Color.Black;
            this.aGaugeFuel.ScaleLinesMajorInnerRadius = 40;
            this.aGaugeFuel.ScaleLinesMajorOuterRadius = 50;
            this.aGaugeFuel.ScaleLinesMajorStepValue = 50F;
            this.aGaugeFuel.ScaleLinesMajorWidth = 2;
            this.aGaugeFuel.ScaleLinesMinorColor = System.Drawing.Color.Black;
            this.aGaugeFuel.ScaleLinesMinorInnerRadius = 43;
            this.aGaugeFuel.ScaleLinesMinorNumOf = 3;
            this.aGaugeFuel.ScaleLinesMinorOuterRadius = 50;
            this.aGaugeFuel.ScaleLinesMinorWidth = 1;
            this.aGaugeFuel.ScaleNumbersColor = System.Drawing.Color.Black;
            this.aGaugeFuel.ScaleNumbersFormat = null;
            this.aGaugeFuel.ScaleNumbersFormatter = null;
            this.aGaugeFuel.ScaleNumbersRadius = 62;
            this.aGaugeFuel.ScaleNumbersRotation = 90;
            this.aGaugeFuel.ScaleNumbersStartScaleLine = 1;
            this.aGaugeFuel.ScaleNumbersStepScaleLines = 2;
            this.aGaugeFuel.Size = new System.Drawing.Size(95, 107);
            this.aGaugeFuel.TabIndex = 16;
            this.aGaugeFuel.Text = "aGaugeFuel";
            this.aGaugeFuel.Value = 0F;
            // 
            // btEncerrar
            // 
            this.btEncerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btEncerrar.Location = new System.Drawing.Point(413, 481);
            this.btEncerrar.Name = "btEncerrar";
            this.btEncerrar.Size = new System.Drawing.Size(163, 51);
            this.btEncerrar.TabIndex = 18;
            this.btEncerrar.Text = "Encerrar e Salvar";
            this.btEncerrar.UseVisualStyleBackColor = true;
            this.btEncerrar.Click += new System.EventHandler(this.btEncerrar_Click);
            // 
            // GravarCorrida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 561);
            this.Controls.Add(this.btEncerrar);
            this.Controls.Add(this.aGaugeTemperature);
            this.Controls.Add(this.aGaugeFuel);
            this.Controls.Add(this.chartDinamic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorrida";
            this.Text = "Gravar Corrida - TeleBaja  UEA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GravarCorrida_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GravarCorrida_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.chartDinamic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDinamic;
        private TeleBajaUEA.AGauge aGaugeTemperature;
        private TeleBajaUEA.AGauge aGaugeFuel;
        private System.Windows.Forms.Button btEncerrar;
    }
}