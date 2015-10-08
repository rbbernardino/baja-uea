namespace TeleBajaUEA
{
    partial class GravarCorridaSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GravarCorridaSetup));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btIniciar = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDriver = new System.Windows.Forms.TabPage();
            this.tabWeather = new System.Windows.Forms.TabPage();
            this.tabCar = new System.Windows.Forms.TabPage();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.btCancelar);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.btIniciar);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(830, 479);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(830, 504);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btCancelar.Location = new System.Drawing.Point(421, 394);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(135, 51);
            this.btCancelar.TabIndex = 5;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btIniciar
            // 
            this.btIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btIniciar.Location = new System.Drawing.Point(152, 394);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(135, 51);
            this.btIniciar.TabIndex = 5;
            this.btIniciar.Text = "Iniciar Corrida";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDriver);
            this.tabControl1.Controls.Add(this.tabWeather);
            this.tabControl1.Controls.Add(this.tabCar);
            this.tabControl1.Location = new System.Drawing.Point(23, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 360);
            this.tabControl1.TabIndex = 0;
            // 
            // tabDriver
            // 
            this.tabDriver.Location = new System.Drawing.Point(4, 22);
            this.tabDriver.Name = "tabDriver";
            this.tabDriver.Size = new System.Drawing.Size(738, 334);
            this.tabDriver.TabIndex = 2;
            this.tabDriver.Text = "Motorista";
            this.tabDriver.UseVisualStyleBackColor = true;
            // 
            // tabWeather
            // 
            this.tabWeather.Location = new System.Drawing.Point(4, 22);
            this.tabWeather.Name = "tabWeather";
            this.tabWeather.Padding = new System.Windows.Forms.Padding(3);
            this.tabWeather.Size = new System.Drawing.Size(738, 334);
            this.tabWeather.TabIndex = 1;
            this.tabWeather.Text = "Clima e Pista";
            this.tabWeather.UseVisualStyleBackColor = true;
            // 
            // tabCar
            // 
            this.tabCar.Location = new System.Drawing.Point(4, 22);
            this.tabCar.Name = "tabCar";
            this.tabCar.Padding = new System.Windows.Forms.Padding(3);
            this.tabCar.Size = new System.Drawing.Size(738, 334);
            this.tabCar.TabIndex = 0;
            this.tabCar.Text = "Carro";
            this.tabCar.UseVisualStyleBackColor = true;
            // 
            // GravarCorridaSetup
            // 
            this.AcceptButton = this.btIniciar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(830, 504);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorridaSetup";
            this.Text = "Setup - TeleBaja UEA";
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCar;
        private System.Windows.Forms.TabPage tabWeather;
        private System.Windows.Forms.TabPage tabDriver;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.Button btCancelar;
    }
}