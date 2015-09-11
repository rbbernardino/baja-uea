namespace TeleBajaUEA
{
    partial class GravarCorridaConexão
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GravarCorridaConexão));
            this.loadingIconBD = new System.Windows.Forms.PictureBox();
            this.labelConexaoBD = new System.Windows.Forms.Label();
            this.labelConexaoBaja = new System.Windows.Forms.Label();
            this.loadingIconBaja = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBaja)).BeginInit();
            this.SuspendLayout();
            // 
            // loadingIconBD
            // 
            this.loadingIconBD.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconBD.Image = ((System.Drawing.Image)(resources.GetObject("loadingIconBD.Image")));
            this.loadingIconBD.Location = new System.Drawing.Point(58, 59);
            this.loadingIconBD.Name = "loadingIconBD";
            this.loadingIconBD.Size = new System.Drawing.Size(30, 30);
            this.loadingIconBD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconBD.TabIndex = 0;
            this.loadingIconBD.TabStop = false;
            // 
            // labelConexaoBD
            // 
            this.labelConexaoBD.AutoSize = true;
            this.labelConexaoBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConexaoBD.Location = new System.Drawing.Point(100, 65);
            this.labelConexaoBD.Name = "labelConexaoBD";
            this.labelConexaoBD.Size = new System.Drawing.Size(278, 20);
            this.labelConexaoBD.TabIndex = 2;
            this.labelConexaoBD.Text = "Conectando com o Banco de Dados...";
            // 
            // labelConexaoBaja
            // 
            this.labelConexaoBaja.AutoSize = true;
            this.labelConexaoBaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConexaoBaja.Location = new System.Drawing.Point(100, 122);
            this.labelConexaoBaja.Name = "labelConexaoBaja";
            this.labelConexaoBaja.Size = new System.Drawing.Size(234, 20);
            this.labelConexaoBaja.TabIndex = 4;
            this.labelConexaoBaja.Text = "Conectando com o Carro Baja...";
            // 
            // loadingIconBaja
            // 
            this.loadingIconBaja.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconBaja.Image = ((System.Drawing.Image)(resources.GetObject("loadingIconBaja.Image")));
            this.loadingIconBaja.Location = new System.Drawing.Point(58, 116);
            this.loadingIconBaja.Name = "loadingIconBaja";
            this.loadingIconBaja.Size = new System.Drawing.Size(30, 30);
            this.loadingIconBaja.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconBaja.TabIndex = 3;
            this.loadingIconBaja.TabStop = false;
            // 
            // GravarCorridaConexão
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 231);
            this.Controls.Add(this.labelConexaoBaja);
            this.Controls.Add(this.loadingIconBaja);
            this.Controls.Add(this.labelConexaoBD);
            this.Controls.Add(this.loadingIconBD);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorridaConexão";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gravar Corrida - TeleBaja UEA";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GravarCorridaConexão_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox loadingIconBD;
        private System.Windows.Forms.Label labelConexaoBD;
        private System.Windows.Forms.Label labelConexaoBaja;
        private System.Windows.Forms.PictureBox loadingIconBaja;
    }
}