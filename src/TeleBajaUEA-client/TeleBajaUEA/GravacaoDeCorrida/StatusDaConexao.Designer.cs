namespace TeleBajaUEA.GravacaoDeCorrida
{
    partial class StatusDaConexao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusDaConexao));
            this.label1 = new System.Windows.Forms.Label();
            this.labelIncome = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelByteRate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalPontos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPacotesPerd = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelBytesInBuffer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pontos por Segundo:";
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelIncome.Location = new System.Drawing.Point(205, 19);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(80, 20);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "0000 Pt/s";
            this.labelIncome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(16, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bytes por Segundo:";
            // 
            // labelByteRate
            // 
            this.labelByteRate.AutoSize = true;
            this.labelByteRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelByteRate.Location = new System.Drawing.Point(209, 89);
            this.labelByteRate.Name = "labelByteRate";
            this.labelByteRate.Size = new System.Drawing.Size(76, 20);
            this.labelByteRate.TabIndex = 3;
            this.labelByteRate.Text = "0000 B/s";
            this.labelByteRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(13, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pontos no gráfico:";
            // 
            // labelTotalPontos
            // 
            this.labelTotalPontos.AutoSize = true;
            this.labelTotalPontos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelTotalPontos.Location = new System.Drawing.Point(210, 50);
            this.labelTotalPontos.Name = "labelTotalPontos";
            this.labelTotalPontos.Size = new System.Drawing.Size(75, 20);
            this.labelTotalPontos.TabIndex = 5;
            this.labelTotalPontos.Text = "0000 Pts";
            this.labelTotalPontos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.Location = new System.Drawing.Point(13, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pacotes Perdidos:";
            // 
            // labelPacotesPerd
            // 
            this.labelPacotesPerd.AutoSize = true;
            this.labelPacotesPerd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelPacotesPerd.Location = new System.Drawing.Point(206, 151);
            this.labelPacotesPerd.Name = "labelPacotesPerd";
            this.labelPacotesPerd.Size = new System.Drawing.Size(79, 20);
            this.labelPacotesPerd.TabIndex = 7;
            this.labelPacotesPerd.Text = "0000 Pac";
            this.labelPacotesPerd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label5.Location = new System.Drawing.Point(16, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Bytes no \"in buffer\":";
            // 
            // labelBytesInBuffer
            // 
            this.labelBytesInBuffer.AutoSize = true;
            this.labelBytesInBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.labelBytesInBuffer.Location = new System.Drawing.Point(206, 118);
            this.labelBytesInBuffer.Name = "labelBytesInBuffer";
            this.labelBytesInBuffer.Size = new System.Drawing.Size(93, 20);
            this.labelBytesInBuffer.TabIndex = 9;
            this.labelBytesInBuffer.Text = "0000 Bytes";
            this.labelBytesInBuffer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusDaConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 261);
            this.Controls.Add(this.labelBytesInBuffer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelPacotesPerd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelTotalPontos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelByteRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelIncome);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatusDaConexao";
            this.Text = "Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StatusDaConexao_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatusDaConexao_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelIncome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelByteRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTotalPontos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPacotesPerd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelBytesInBuffer;
    }
}