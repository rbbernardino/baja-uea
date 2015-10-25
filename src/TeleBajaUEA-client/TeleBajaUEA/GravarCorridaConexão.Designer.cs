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
            this.labelCarConnection = new System.Windows.Forms.Label();
            this.loadingIconCar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconCar)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCarConnection
            // 
            this.labelCarConnection.AutoSize = true;
            this.labelCarConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCarConnection.Location = new System.Drawing.Point(97, 94);
            this.labelCarConnection.Name = "labelCarConnection";
            this.labelCarConnection.Size = new System.Drawing.Size(219, 20);
            this.labelCarConnection.TabIndex = 4;
            this.labelCarConnection.Text = "Conectando com o carro Baja";
            // 
            // loadingIconCar
            // 
            this.loadingIconCar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconCar.Image = global::TeleBajaUEA.Properties.Resources.spinner;
            this.loadingIconCar.Location = new System.Drawing.Point(55, 88);
            this.loadingIconCar.Name = "loadingIconCar";
            this.loadingIconCar.Size = new System.Drawing.Size(30, 30);
            this.loadingIconCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconCar.TabIndex = 3;
            this.loadingIconCar.TabStop = false;
            // 
            // GravarCorridaConexão
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 231);
            this.Controls.Add(this.labelCarConnection);
            this.Controls.Add(this.loadingIconCar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorridaConexão";
            this.Text = "Gravar Corrida - TeleBaja UEA";
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconCar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCarConnection;
        private System.Windows.Forms.PictureBox loadingIconCar;
    }
}