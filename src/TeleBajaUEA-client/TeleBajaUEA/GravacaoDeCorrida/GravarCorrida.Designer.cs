namespace TeleBajaUEA.GravacaoDeCorrida
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
            this.btEncerrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textVelocidade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textRPM = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textFreio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.imgConnStatus = new System.Windows.Forms.PictureBox();
            this.labelSemSinal = new System.Windows.Forms.Label();
            this.labelForca = new System.Windows.Forms.Label();
            this.checkBoxSpeed = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxBrake = new System.Windows.Forms.CheckBox();
            this.checkBoxRPM = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartDinamic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgConnStatus)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartDinamic
            // 
            this.chartDinamic.BackColor = System.Drawing.Color.Transparent;
            this.chartDinamic.BorderlineWidth = 3;
            this.chartDinamic.BorderSkin.BorderWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.chartDinamic.ChartAreas.Add(chartArea1);
            this.chartDinamic.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chartDinamic.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.BorderWidth = 2;
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            legend1.IsTextAutoFit = false;
            legend1.ItemColumnSpacing = 90;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            legend1.ShadowOffset = 2;
            this.chartDinamic.Legends.Add(legend1);
            this.chartDinamic.Location = new System.Drawing.Point(0, 0);
            this.chartDinamic.Name = "chartDinamic";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.LegendText = "Velocidade";
            series1.Name = "Speed";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series3.Legend = "Legend1";
            series3.LegendText = "Freio";
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
        "Temperatura",
        "",
        "",
        "C"};
            this.aGaugeTemperature.CapText = "Temperatura";
            this.aGaugeTemperature.Center = new System.Drawing.Point(15, 70);
            this.aGaugeTemperature.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aGaugeTemperature.Location = new System.Drawing.Point(214, 481);
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
            this.aGaugeTemperature.Size = new System.Drawing.Size(94, 108);
            this.aGaugeTemperature.TabIndex = 17;
            this.aGaugeTemperature.Text = "aGaugeTemperature";
            this.aGaugeTemperature.Value = 60F;
            // 
            // btEncerrar
            // 
            this.btEncerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btEncerrar.Location = new System.Drawing.Point(916, 564);
            this.btEncerrar.Name = "btEncerrar";
            this.btEncerrar.Size = new System.Drawing.Size(136, 35);
            this.btEncerrar.TabIndex = 18;
            this.btEncerrar.Text = "Encerrar e Salvar";
            this.btEncerrar.UseVisualStyleBackColor = true;
            this.btEncerrar.Click += new System.EventHandler(this.btEncerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(328, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Velocidade";
            // 
            // textVelocidade
            // 
            this.textVelocidade.BackColor = System.Drawing.Color.Black;
            this.textVelocidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            this.textVelocidade.ForeColor = System.Drawing.Color.Lime;
            this.textVelocidade.Location = new System.Drawing.Point(331, 501);
            this.textVelocidade.Name = "textVelocidade";
            this.textVelocidade.Size = new System.Drawing.Size(100, 43);
            this.textVelocidade.TabIndex = 20;
            this.textVelocidade.Text = " 00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(390, 519);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "km/h";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(547, 519);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "rpm";
            // 
            // textRPM
            // 
            this.textRPM.BackColor = System.Drawing.Color.Black;
            this.textRPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            this.textRPM.ForeColor = System.Drawing.Color.Lime;
            this.textRPM.Location = new System.Drawing.Point(455, 501);
            this.textRPM.Name = "textRPM";
            this.textRPM.Size = new System.Drawing.Size(127, 43);
            this.textRPM.TabIndex = 23;
            this.textRPM.Text = " 0000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(452, 481);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "RPM";
            // 
            // textFreio
            // 
            this.textFreio.BackColor = System.Drawing.Color.Black;
            this.textFreio.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            this.textFreio.ForeColor = System.Drawing.Color.Red;
            this.textFreio.Location = new System.Drawing.Point(609, 501);
            this.textFreio.Name = "textFreio";
            this.textFreio.Size = new System.Drawing.Size(90, 43);
            this.textFreio.TabIndex = 26;
            this.textFreio.Text = "OFF";
            this.textFreio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label5.Location = new System.Drawing.Point(606, 481);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Freio";
            // 
            // imgConnStatus
            // 
            this.imgConnStatus.Image = global::TeleBajaUEA.Properties.Resources.conn_off;
            this.imgConnStatus.Location = new System.Drawing.Point(87, 481);
            this.imgConnStatus.Name = "imgConnStatus";
            this.imgConnStatus.Size = new System.Drawing.Size(85, 85);
            this.imgConnStatus.TabIndex = 27;
            this.imgConnStatus.TabStop = false;
            // 
            // labelSemSinal
            // 
            this.labelSemSinal.AutoSize = true;
            this.labelSemSinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelSemSinal.ForeColor = System.Drawing.Color.DarkRed;
            this.labelSemSinal.Location = new System.Drawing.Point(83, 569);
            this.labelSemSinal.Name = "labelSemSinal";
            this.labelSemSinal.Size = new System.Drawing.Size(90, 20);
            this.labelSemSinal.TabIndex = 28;
            this.labelSemSinal.Text = "Sem Sinal!";
            this.labelSemSinal.Visible = false;
            // 
            // labelForca
            // 
            this.labelForca.AutoSize = true;
            this.labelForca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelForca.ForeColor = System.Drawing.Color.Black;
            this.labelForca.Location = new System.Drawing.Point(84, 569);
            this.labelForca.Name = "labelForca";
            this.labelForca.Size = new System.Drawing.Size(95, 16);
            this.labelForca.TabIndex = 29;
            this.labelForca.Text = "Força do Sinal";
            this.labelForca.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelForca.Visible = false;
            // 
            // checkBoxSpeed
            // 
            this.checkBoxSpeed.AutoSize = true;
            this.checkBoxSpeed.Checked = true;
            this.checkBoxSpeed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSpeed.Location = new System.Drawing.Point(17, 31);
            this.checkBoxSpeed.Name = "checkBoxSpeed";
            this.checkBoxSpeed.Size = new System.Drawing.Size(97, 21);
            this.checkBoxSpeed.TabIndex = 30;
            this.checkBoxSpeed.Text = "Velocidade";
            this.checkBoxSpeed.UseVisualStyleBackColor = true;
            this.checkBoxSpeed.CheckedChanged += new System.EventHandler(this.checkBoxEnabledSeries_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(896, 523);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 35);
            this.button1.TabIndex = 31;
            this.button1.Text = "Encerrar sem Salvar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxBrake);
            this.groupBox1.Controls.Add(this.checkBoxRPM);
            this.groupBox1.Controls.Add(this.checkBoxSpeed);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(721, 481);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 118);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gráficos Visíveis";
            // 
            // checkBoxBrake
            // 
            this.checkBoxBrake.AutoSize = true;
            this.checkBoxBrake.Checked = true;
            this.checkBoxBrake.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBrake.Location = new System.Drawing.Point(17, 83);
            this.checkBoxBrake.Name = "checkBoxBrake";
            this.checkBoxBrake.Size = new System.Drawing.Size(59, 21);
            this.checkBoxBrake.TabIndex = 32;
            this.checkBoxBrake.Text = "Freio";
            this.checkBoxBrake.UseVisualStyleBackColor = true;
            this.checkBoxBrake.CheckedChanged += new System.EventHandler(this.checkBoxEnabledSeries_CheckedChanged);
            // 
            // checkBoxRPM
            // 
            this.checkBoxRPM.AutoSize = true;
            this.checkBoxRPM.Checked = true;
            this.checkBoxRPM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRPM.Location = new System.Drawing.Point(17, 56);
            this.checkBoxRPM.Name = "checkBoxRPM";
            this.checkBoxRPM.Size = new System.Drawing.Size(57, 21);
            this.checkBoxRPM.TabIndex = 31;
            this.checkBoxRPM.Text = "RPM";
            this.checkBoxRPM.UseVisualStyleBackColor = true;
            this.checkBoxRPM.CheckedChanged += new System.EventHandler(this.checkBoxEnabledSeries_CheckedChanged);
            // 
            // GravarCorrida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 611);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelForca);
            this.Controls.Add(this.labelSemSinal);
            this.Controls.Add(this.imgConnStatus);
            this.Controls.Add(this.textFreio);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textRPM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textVelocidade);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btEncerrar);
            this.Controls.Add(this.aGaugeTemperature);
            this.Controls.Add(this.chartDinamic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorrida";
            this.Text = "Gravar Corrida - TeleBaja  UEA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GravarCorrida_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GravarCorrida_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.chartDinamic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgConnStatus)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDinamic;
        private TeleBajaUEA.AGauge aGaugeTemperature;
        private System.Windows.Forms.Button btEncerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textVelocidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textRPM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textFreio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox imgConnStatus;
        private System.Windows.Forms.Label labelSemSinal;
        private System.Windows.Forms.Label labelForca;
        private System.Windows.Forms.CheckBox checkBoxSpeed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxRPM;
        private System.Windows.Forms.CheckBox checkBoxBrake;
    }
}