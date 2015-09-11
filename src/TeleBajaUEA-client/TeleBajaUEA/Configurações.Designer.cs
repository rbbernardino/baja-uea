namespace TeleBajaUEA
{
    partial class Configurações
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configurações));
            this.btSenha = new System.Windows.Forms.Button();
            this.btXBee = new System.Windows.Forms.Button();
            this.btBancoDeDados = new System.Windows.Forms.Button();
            this.labelTítulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btSenha
            // 
            this.btSenha.Location = new System.Drawing.Point(257, 112);
            this.btSenha.Name = "btSenha";
            this.btSenha.Size = new System.Drawing.Size(95, 31);
            this.btSenha.TabIndex = 13;
            this.btSenha.Text = "Senha";
            this.btSenha.UseVisualStyleBackColor = true;
            // 
            // btXBee
            // 
            this.btXBee.Location = new System.Drawing.Point(133, 112);
            this.btXBee.Name = "btXBee";
            this.btXBee.Size = new System.Drawing.Size(95, 31);
            this.btXBee.TabIndex = 12;
            this.btXBee.Text = "XBee";
            this.btXBee.UseVisualStyleBackColor = true;
            // 
            // btBancoDeDados
            // 
            this.btBancoDeDados.Location = new System.Drawing.Point(12, 112);
            this.btBancoDeDados.Name = "btBancoDeDados";
            this.btBancoDeDados.Size = new System.Drawing.Size(95, 31);
            this.btBancoDeDados.TabIndex = 11;
            this.btBancoDeDados.Text = "Banco de Dados";
            this.btBancoDeDados.UseVisualStyleBackColor = true;
            // 
            // labelTítulo
            // 
            this.labelTítulo.AutoSize = true;
            this.labelTítulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTítulo.Location = new System.Drawing.Point(101, 22);
            this.labelTítulo.Name = "labelTítulo";
            this.labelTítulo.Size = new System.Drawing.Size(132, 24);
            this.labelTítulo.TabIndex = 10;
            this.labelTítulo.Text = "Configurações";
            // 
            // Configurações
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 231);
            this.Controls.Add(this.btSenha);
            this.Controls.Add(this.btXBee);
            this.Controls.Add(this.btBancoDeDados);
            this.Controls.Add(this.labelTítulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurações";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - TeleBaja UEA";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Configurações_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSenha;
        private System.Windows.Forms.Button btXBee;
        private System.Windows.Forms.Button btBancoDeDados;
        private System.Windows.Forms.Label labelTítulo;
    }
}