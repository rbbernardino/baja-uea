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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem1 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem2 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.LegendItem legendItem3 = new System.Windows.Forms.DataVisualization.Charting.LegendItem();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalisarCorrida));
            this.btVoltar = new System.Windows.Forms.Button();
            this.chartsNew = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btVerSetup = new System.Windows.Forms.Button();
            this.btZoomOut = new System.Windows.Forms.Button();
            this.btZoomIn = new System.Windows.Forms.Button();
            this.btMinus = new System.Windows.Forms.Button();
            this.btPlus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartsNew)).BeginInit();
            this.SuspendLayout();
            // 
            // btVoltar
            // 
            this.btVoltar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btVoltar.Location = new System.Drawing.Point(891, 655);
            this.btVoltar.Name = "btVoltar";
            this.btVoltar.Size = new System.Drawing.Size(135, 51);
            this.btVoltar.TabIndex = 7;
            this.btVoltar.Text = "Fechar";
            this.btVoltar.UseVisualStyleBackColor = true;
            this.btVoltar.Click += new System.EventHandler(this.btVoltar_Click);
            // 
            // chartsNew
            // 
            this.chartsNew.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.ScrollBar.Size = 16D;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "Speed";
            chartArea2.AlignWithChartArea = "Speed";
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.ScrollBar.Size = 16D;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "RPM";
            chartArea3.AlignWithChartArea = "Speed";
            chartArea3.AxisX.ScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea3.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea3.AxisX.ScrollBar.Size = 16D;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.Name = "Brake";
            this.chartsNew.ChartAreas.Add(chartArea1);
            this.chartsNew.ChartAreas.Add(chartArea2);
            this.chartsNew.ChartAreas.Add(chartArea3);
            this.chartsNew.Cursor = System.Windows.Forms.Cursors.Cross;
            legend1.BackColor = System.Drawing.Color.White;
            legend1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
            legend1.BorderColor = System.Drawing.Color.Black;
            legendItem1.BorderWidth = 3;
            legendItem1.Color = System.Drawing.Color.Maroon;
            legendItem1.ImageStyle = System.Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line;
            legendItem1.Name = "max";
            legendItem2.BorderWidth = 3;
            legendItem2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            legendItem2.ImageStyle = System.Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line;
            legendItem2.Name = "med";
            legendItem3.BorderWidth = 3;
            legendItem3.Color = System.Drawing.Color.Purple;
            legendItem3.ImageStyle = System.Windows.Forms.DataVisualization.Charting.LegendImageStyle.Line;
            legendItem3.Name = "min";
            legend1.CustomItems.Add(legendItem1);
            legend1.CustomItems.Add(legendItem2);
            legend1.CustomItems.Add(legendItem3);
            legend1.DockedToChartArea = "Speed";
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Legend1";
            legend1.ShadowOffset = 3;
            this.chartsNew.Legends.Add(legend1);
            this.chartsNew.Location = new System.Drawing.Point(12, 12);
            this.chartsNew.Name = "chartsNew";
            series1.ChartArea = "Speed";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Speed";
            series2.ChartArea = "RPM";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "RPM";
            series3.ChartArea = "Brake";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "Brake";
            this.chartsNew.Series.Add(series1);
            this.chartsNew.Series.Add(series2);
            this.chartsNew.Series.Add(series3);
            this.chartsNew.Size = new System.Drawing.Size(1040, 629);
            this.chartsNew.TabIndex = 13;
            this.chartsNew.Text = "5";
            this.chartsNew.SelectionRangeChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.chartsNew_SelectionRangeChanged);
            this.chartsNew.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chartsNew_AxisViewChanged);
            this.chartsNew.CustomizeLegend += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CustomizeLegendEventArgs>(this.chartsNew_CustomizeLegend);
            // 
            // btVerSetup
            // 
            this.btVerSetup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btVerSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btVerSetup.Location = new System.Drawing.Point(733, 655);
            this.btVerSetup.Name = "btVerSetup";
            this.btVerSetup.Size = new System.Drawing.Size(135, 51);
            this.btVerSetup.TabIndex = 14;
            this.btVerSetup.Text = "Ver Setup";
            this.btVerSetup.UseVisualStyleBackColor = true;
            this.btVerSetup.Click += new System.EventHandler(this.btVerSetup_Click);
            // 
            // btZoomOut
            // 
            this.btZoomOut.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomOut.Image = global::TeleBajaUEA.Properties.Resources.zoom_out;
            this.btZoomOut.Location = new System.Drawing.Point(510, 681);
            this.btZoomOut.Name = "btZoomOut";
            this.btZoomOut.Size = new System.Drawing.Size(40, 40);
            this.btZoomOut.TabIndex = 18;
            this.btZoomOut.UseVisualStyleBackColor = true;
            this.btZoomOut.Click += new System.EventHandler(this.btZoomOut_Click);
            // 
            // btZoomIn
            // 
            this.btZoomIn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btZoomIn.Image = global::TeleBajaUEA.Properties.Resources.zoom_in;
            this.btZoomIn.Location = new System.Drawing.Point(510, 623);
            this.btZoomIn.Name = "btZoomIn";
            this.btZoomIn.Size = new System.Drawing.Size(40, 40);
            this.btZoomIn.TabIndex = 17;
            this.btZoomIn.UseVisualStyleBackColor = true;
            this.btZoomIn.Click += new System.EventHandler(this.btZoomIn_Click);
            // 
            // btMinus
            // 
            this.btMinus.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btMinus.Image = global::TeleBajaUEA.Properties.Resources.arrow_left_minus;
            this.btMinus.Location = new System.Drawing.Point(453, 647);
            this.btMinus.Name = "btMinus";
            this.btMinus.Size = new System.Drawing.Size(40, 48);
            this.btMinus.TabIndex = 12;
            this.btMinus.UseVisualStyleBackColor = true;
            this.btMinus.Click += new System.EventHandler(this.btMinus_Click);
            // 
            // btPlus
            // 
            this.btPlus.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btPlus.Image = global::TeleBajaUEA.Properties.Resources.arrow_right_plus;
            this.btPlus.Location = new System.Drawing.Point(571, 647);
            this.btPlus.Name = "btPlus";
            this.btPlus.Size = new System.Drawing.Size(40, 48);
            this.btPlus.TabIndex = 11;
            this.btPlus.UseVisualStyleBackColor = true;
            this.btPlus.Click += new System.EventHandler(this.btPlus_Click);
            // 
            // AnalisarCorrida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 733);
            this.Controls.Add(this.btZoomOut);
            this.Controls.Add(this.btZoomIn);
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
        private System.Windows.Forms.Button btZoomIn;
        private System.Windows.Forms.Button btZoomOut;
    }
}