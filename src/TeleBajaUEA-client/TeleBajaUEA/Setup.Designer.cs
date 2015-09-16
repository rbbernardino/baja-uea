namespace TeleBajaUEA
{
    partial class Setup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMotorista = new System.Windows.Forms.TabPage();
            this.tabAmbiente = new System.Windows.Forms.TabPage();
            this.tabCarro = new System.Windows.Forms.TabPage();
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
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(526, 336);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(526, 336);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMotorista);
            this.tabControl1.Controls.Add(this.tabAmbiente);
            this.tabControl1.Controls.Add(this.tabCarro);
            this.tabControl1.Location = new System.Drawing.Point(13, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 296);
            this.tabControl1.TabIndex = 0;
            // 
            // tabMotorista
            // 
            this.tabMotorista.Location = new System.Drawing.Point(4, 22);
            this.tabMotorista.Name = "tabMotorista";
            this.tabMotorista.Padding = new System.Windows.Forms.Padding(3);
            this.tabMotorista.Size = new System.Drawing.Size(502, 270);
            this.tabMotorista.TabIndex = 0;
            this.tabMotorista.Text = "Motorista";
            this.tabMotorista.UseVisualStyleBackColor = true;
            // 
            // tabAmbiente
            // 
            this.tabAmbiente.Location = new System.Drawing.Point(4, 22);
            this.tabAmbiente.Name = "tabAmbiente";
            this.tabAmbiente.Padding = new System.Windows.Forms.Padding(3);
            this.tabAmbiente.Size = new System.Drawing.Size(502, 270);
            this.tabAmbiente.TabIndex = 1;
            this.tabAmbiente.Text = "Ambiente";
            this.tabAmbiente.UseVisualStyleBackColor = true;
            // 
            // tabCarro
            // 
            this.tabCarro.Location = new System.Drawing.Point(4, 22);
            this.tabCarro.Name = "tabCarro";
            this.tabCarro.Size = new System.Drawing.Size(502, 270);
            this.tabCarro.TabIndex = 2;
            this.tabCarro.Text = "Carro";
            this.tabCarro.UseVisualStyleBackColor = true;
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 336);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.TabPage tabMotorista;
        private System.Windows.Forms.TabPage tabAmbiente;
        private System.Windows.Forms.TabPage tabCarro;
    }
}