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
            this.labelDBConnection = new System.Windows.Forms.Label();
            this.labelCarConnection = new System.Windows.Forms.Label();
            this.loadingIconCar = new System.Windows.Forms.PictureBox();
            this.loadingIconDB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconDB)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDBConnection
            // 
            this.labelDBConnection.AutoSize = true;
            this.labelDBConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBConnection.Location = new System.Drawing.Point(100, 65);
            this.labelDBConnection.Name = "labelDBConnection";
            this.labelDBConnection.Size = new System.Drawing.Size(194, 20);
            this.labelDBConnection.TabIndex = 2;
            this.labelDBConnection.Text = "Connecting to Database...";
            // 
            // labelCarConnection
            // 
            this.labelCarConnection.AutoSize = true;
            this.labelCarConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCarConnection.Location = new System.Drawing.Point(100, 122);
            this.labelCarConnection.Name = "labelCarConnection";
            this.labelCarConnection.Size = new System.Drawing.Size(223, 20);
            this.labelCarConnection.TabIndex = 4;
            this.labelCarConnection.Text = "Connecting with the car Baja...";
            // 
            // loadingIconCar
            // 
            this.loadingIconCar.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconCar.Image = global::TeleBajaUEA.Properties.Resources.spinner;
            this.loadingIconCar.Location = new System.Drawing.Point(58, 116);
            this.loadingIconCar.Name = "loadingIconCar";
            this.loadingIconCar.Size = new System.Drawing.Size(30, 30);
            this.loadingIconCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconCar.TabIndex = 3;
            this.loadingIconCar.TabStop = false;
            // 
            // loadingIconDB
            // 
            this.loadingIconDB.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.loadingIconDB.Image = global::TeleBajaUEA.Properties.Resources.spinner;
            this.loadingIconDB.Location = new System.Drawing.Point(58, 59);
            this.loadingIconDB.Name = "loadingIconDB";
            this.loadingIconDB.Size = new System.Drawing.Size(30, 30);
            this.loadingIconDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loadingIconDB.TabIndex = 0;
            this.loadingIconDB.TabStop = false;
            // 
            // GravarCorridaConexão
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 231);
            this.Controls.Add(this.labelCarConnection);
            this.Controls.Add(this.loadingIconCar);
            this.Controls.Add(this.labelDBConnection);
            this.Controls.Add(this.loadingIconDB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GravarCorridaConexão";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gravar Corrida - TeleBaja UEA";
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIconDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox loadingIconDB;
        private System.Windows.Forms.Label labelDBConnection;
        private System.Windows.Forms.Label labelCarConnection;
        private System.Windows.Forms.PictureBox loadingIconCar;
    }
}