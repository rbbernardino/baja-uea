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
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSenha
            // 
            this.btSenha.Location = new System.Drawing.Point(257, 112);
            this.btSenha.Name = "btSenha";
            this.btSenha.Size = new System.Drawing.Size(95, 31);
            this.btSenha.TabIndex = 13;
            this.btSenha.Text = "Password";
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
            this.btBancoDeDados.Text = "Database";
            this.btBancoDeDados.UseVisualStyleBackColor = true;
            // 
            // labelTítulo
            // 
            this.labelTítulo.AutoSize = true;
            this.labelTítulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTítulo.Location = new System.Drawing.Point(129, 22);
            this.labelTítulo.Name = "labelTítulo";
            this.labelTítulo.Size = new System.Drawing.Size(76, 24);
            this.labelTítulo.TabIndex = 10;
            this.labelTítulo.Text = "Settings";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.BackgroundImage = global::TeleBajaUEA.Properties.Resources.seta;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(0, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 27);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(297, 200);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(55, 19);
            this.button4.TabIndex = 15;
            this.button4.Text = "Ok";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Configurações
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 231);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btSenha);
            this.Controls.Add(this.btXBee);
            this.Controls.Add(this.btBancoDeDados);
            this.Controls.Add(this.labelTítulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurações";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - TeleBaja UEA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSenha;
        private System.Windows.Forms.Button btXBee;
        private System.Windows.Forms.Button btBancoDeDados;
        private System.Windows.Forms.Label labelTítulo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}