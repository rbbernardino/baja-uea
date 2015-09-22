namespace TeleBajaUEA
{
    partial class AnalisarCorridaConexao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalisarCorridaConexao));
            this.labelConexaoBD = new System.Windows.Forms.Label();
            this.loadingIconBD = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBD)).BeginInit();
            this.SuspendLayout();
            // 
            // labelConexaoBD
            // 
            this.labelConexaoBD.AutoSize = true;
            this.labelConexaoBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConexaoBD.Location = new System.Drawing.Point(167, 136);
            this.labelConexaoBD.Name = "labelConexaoBD";
            this.labelConexaoBD.Size = new System.Drawing.Size(198, 20);
            this.labelConexaoBD.TabIndex = 6;
            this.labelConexaoBD.Text = "Connecting to Database ...";
            // 
            // loadingIconBD
            // 
            this.loadingIconBD.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconBD.Image = ((System.Drawing.Image)(resources.GetObject("loadingIconBD.Image")));
            this.loadingIconBD.Location = new System.Drawing.Point(125, 130);
            this.loadingIconBD.Name = "loadingIconBD";
            this.loadingIconBD.Size = new System.Drawing.Size(30, 30);
            this.loadingIconBD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconBD.TabIndex = 5;
            this.loadingIconBD.TabStop = false;
            // 
            // AnalisarCorridaConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 346);
            this.Controls.Add(this.labelConexaoBD);
            this.Controls.Add(this.loadingIconBD);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnalisarCorridaConexao";
            this.Text = "Analisar Corrida - TeleBaja UEA";
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconBD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelConexaoBD;
        private System.Windows.Forms.PictureBox loadingIconBD;
    }
}