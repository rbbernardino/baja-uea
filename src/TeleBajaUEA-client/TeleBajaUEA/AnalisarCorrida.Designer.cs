namespace TeleBajaUEA
{
    partial class AnalisarCorrida
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalisarCorrida));
            this.btVoltar = new System.Windows.Forms.Button();
            this.chartSpeed = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRPM = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBrake = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btPlus = new System.Windows.Forms.Button();
            this.btMinus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBrake)).BeginInit();
            this.SuspendLayout();
            // 
            // btVoltar
            // 
            this.btVoltar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btVoltar.Location = new System.Drawing.Point(444, 678);
            this.btVoltar.Name = "btVoltar";
            this.btVoltar.Size = new System.Drawing.Size(135, 51);
            this.btVoltar.TabIndex = 7;
            this.btVoltar.Text = "Voltar";
            this.btVoltar.UseVisualStyleBackColor = true;
            this.btVoltar.Click += new System.EventHandler(this.btVoltar_Click);
            // 
            // chartSpeed
            // 
            this.chartSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chartSpeed.BackColor = System.Drawing.Color.Transparent;
            this.chartSpeed.BorderlineWidth = 3;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.Name = "ChartArea";
            this.chartSpeed.ChartAreas.Add(chartArea1);
            this.chartSpeed.Location = new System.Drawing.Point(85, 0);
            this.chartSpeed.Name = "chartSpeed";
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            this.chartSpeed.Series.Add(series1);
            this.chartSpeed.Size = new System.Drawing.Size(978, 221);
            this.chartSpeed.TabIndex = 8;
            this.chartSpeed.Text = "chartSpeed";
            // 
            // chartRPM
            // 
            this.chartRPM.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chartRPM.BackColor = System.Drawing.Color.Transparent;
            this.chartRPM.BorderlineWidth = 3;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.Name = "ChartArea";
            this.chartRPM.ChartAreas.Add(chartArea2);
            this.chartRPM.Location = new System.Drawing.Point(85, 215);
            this.chartRPM.Name = "chartRPM";
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "RPM";
            this.chartRPM.Series.Add(series2);
            this.chartRPM.Size = new System.Drawing.Size(978, 219);
            this.chartRPM.TabIndex = 9;
            this.chartRPM.Text = "chartRPM";
            // 
            // chartBrake
            // 
            this.chartBrake.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chartBrake.BackColor = System.Drawing.Color.Transparent;
            this.chartBrake.BorderlineWidth = 3;
            chartArea3.Name = "ChartArea";
            this.chartBrake.ChartAreas.Add(chartArea3);
            this.chartBrake.Location = new System.Drawing.Point(85, 428);
            this.chartBrake.Name = "chartBrake";
            series3.ChartArea = "ChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Name = "Brake";
            this.chartBrake.Series.Add(series3);
            this.chartBrake.Size = new System.Drawing.Size(978, 235);
            this.chartBrake.TabIndex = 10;
            this.chartBrake.Text = "chartBrake";
            // 
            // btPlus
            // 
            this.btPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btPlus.Location = new System.Drawing.Point(748, 685);
            this.btPlus.Name = "btPlus";
            this.btPlus.Size = new System.Drawing.Size(75, 36);
            this.btPlus.TabIndex = 11;
            this.btPlus.Text = "+ 5 min";
            this.btPlus.UseVisualStyleBackColor = true;
            // 
            // btMinus
            // 
            this.btMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btMinus.Location = new System.Drawing.Point(197, 678);
            this.btMinus.Name = "btMinus";
            this.btMinus.Size = new System.Drawing.Size(75, 36);
            this.btMinus.TabIndex = 12;
            this.btMinus.Text = "- 5 min";
            this.btMinus.UseVisualStyleBackColor = true;
            // 
            // AnalisarCorrida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 741);
            this.Controls.Add(this.btMinus);
            this.Controls.Add(this.btPlus);
            this.Controls.Add(this.chartBrake);
            this.Controls.Add(this.chartRPM);
            this.Controls.Add(this.chartSpeed);
            this.Controls.Add(this.btVoltar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnalisarCorrida";
            this.Text = "Analisar Corrida - TeleBaja UEA";
            ((System.ComponentModel.ISupportInitialize)(this.chartSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBrake)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btVoltar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpeed;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRPM;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBrake;
        private System.Windows.Forms.Button btPlus;
        private System.Windows.Forms.Button btMinus;
    }
}