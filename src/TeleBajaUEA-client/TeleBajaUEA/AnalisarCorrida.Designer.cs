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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalisarCorrida));
            this.btVoltar = new System.Windows.Forms.Button();
            this.btPlus = new System.Windows.Forms.Button();
            this.btMinus = new System.Windows.Forms.Button();
            this.chartsNew = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btVerSetup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartsNew)).BeginInit();
            this.SuspendLayout();
            // 
            // btVoltar
            // 
            this.btVoltar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btVoltar.Location = new System.Drawing.Point(506, 655);
            this.btVoltar.Name = "btVoltar";
            this.btVoltar.Size = new System.Drawing.Size(135, 51);
            this.btVoltar.TabIndex = 7;
            this.btVoltar.Text = "Fechar";
            this.btVoltar.UseVisualStyleBackColor = true;
            this.btVoltar.Click += new System.EventHandler(this.btVoltar_Click);
            // 
            // btPlus
            // 
            this.btPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btPlus.Location = new System.Drawing.Point(940, 663);
            this.btPlus.Name = "btPlus";
            this.btPlus.Size = new System.Drawing.Size(75, 36);
            this.btPlus.TabIndex = 11;
            this.btPlus.Text = "+ 5 min";
            this.btPlus.UseVisualStyleBackColor = true;
            this.btPlus.Click += new System.EventHandler(this.btPlus_Click);
            // 
            // btMinus
            // 
            this.btMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btMinus.Location = new System.Drawing.Point(836, 663);
            this.btMinus.Name = "btMinus";
            this.btMinus.Size = new System.Drawing.Size(75, 36);
            this.btMinus.TabIndex = 12;
            this.btMinus.Text = "- 5 min";
            this.btMinus.UseVisualStyleBackColor = true;
            this.btMinus.Click += new System.EventHandler(this.btMinus_Click);
            // 
            // chartsNew
            // 
            this.chartsNew.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "Speed";
            chartArea2.AlignWithChartArea = "Speed";
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.Name = "RPM";
            chartArea3.AlignWithChartArea = "Speed";
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.Name = "Brake";
            this.chartsNew.ChartAreas.Add(chartArea1);
            this.chartsNew.ChartAreas.Add(chartArea2);
            this.chartsNew.ChartAreas.Add(chartArea3);
            this.chartsNew.Location = new System.Drawing.Point(12, 12);
            this.chartsNew.Name = "chartsNew";
            series1.ChartArea = "Speed";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            series2.ChartArea = "RPM";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            series3.ChartArea = "Brake";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "Brake";
            this.chartsNew.Series.Add(series1);
            this.chartsNew.Series.Add(series2);
            this.chartsNew.Series.Add(series3);
            this.chartsNew.Size = new System.Drawing.Size(1040, 629);
            this.chartsNew.TabIndex = 13;
            this.chartsNew.Text = "chart1";
            // 
            // btVerSetup
            // 
            this.btVerSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btVerSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btVerSetup.Location = new System.Drawing.Point(86, 655);
            this.btVerSetup.Name = "btVerSetup";
            this.btVerSetup.Size = new System.Drawing.Size(135, 51);
            this.btVerSetup.TabIndex = 14;
            this.btVerSetup.Text = "Ver Setup";
            this.btVerSetup.UseVisualStyleBackColor = true;
            this.btVerSetup.Click += new System.EventHandler(this.btVerSetup_Click);
            // 
            // AnalisarCorrida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 733);
            this.Controls.Add(this.btVerSetup);
            this.Controls.Add(this.chartsNew);
            this.Controls.Add(this.btMinus);
            this.Controls.Add(this.btPlus);
            this.Controls.Add(this.btVoltar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnalisarCorrida";
            this.Text = "Analisar Corrida - TeleBaja UEA";
            ((System.ComponentModel.ISupportInitialize)(this.chartsNew)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btVoltar;
        private System.Windows.Forms.Button btPlus;
        private System.Windows.Forms.Button btMinus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartsNew;
        private System.Windows.Forms.Button btVerSetup;
    }
}