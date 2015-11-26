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
            this.btBancoDeDados = new System.Windows.Forms.Button();
            this.labelTítulo = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboUSB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btSenha
            // 
            this.btSenha.Location = new System.Drawing.Point(114, 226);
            this.btSenha.Name = "btSenha";
            this.btSenha.Size = new System.Drawing.Size(95, 31);
            this.btSenha.TabIndex = 13;
            this.btSenha.Text = "Senha";
            this.btSenha.UseVisualStyleBackColor = true;
            // 
            // btBancoDeDados
            // 
            this.btBancoDeDados.Location = new System.Drawing.Point(114, 173);
            this.btBancoDeDados.Name = "btBancoDeDados";
            this.btBancoDeDados.Size = new System.Drawing.Size(95, 31);
            this.btBancoDeDados.TabIndex = 11;
            this.btBancoDeDados.Text = "Diretório Padrão";
            this.btBancoDeDados.UseVisualStyleBackColor = true;
            // 
            // labelTítulo
            // 
            this.labelTítulo.AutoSize = true;
            this.labelTítulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.labelTítulo.Location = new System.Drawing.Point(199, 24);
            this.labelTítulo.Name = "labelTítulo";
            this.labelTítulo.Size = new System.Drawing.Size(152, 26);
            this.labelTítulo.TabIndex = 10;
            this.labelTítulo.Text = "Configurações";
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btCancelar.Location = new System.Drawing.Point(342, 295);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(135, 51);
            this.btCancelar.TabIndex = 16;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.btOK.Location = new System.Drawing.Point(93, 295);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(135, 51);
            this.btOK.TabIndex = 17;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.label1.Location = new System.Drawing.Point(34, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 24);
            this.label1.TabIndex = 18;
            this.label1.Text = "Porta USB do XBee";
            // 
            // comboUSB
            // 
            this.comboUSB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.comboUSB.FormattingEnabled = true;
            this.comboUSB.Location = new System.Drawing.Point(230, 99);
            this.comboUSB.MaxDropDownItems = 4;
            this.comboUSB.Name = "comboUSB";
            this.comboUSB.Size = new System.Drawing.Size(105, 28);
            this.comboUSB.TabIndex = 19;
            // 
            // Configurações
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 358);
            this.Controls.Add(this.comboUSB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btSenha);
            this.Controls.Add(this.btBancoDeDados);
            this.Controls.Add(this.labelTítulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurações";
            this.Text = "Configurações - TeleBaja UEA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSenha;
        private System.Windows.Forms.Button btBancoDeDados;
        private System.Windows.Forms.Label labelTítulo;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboUSB;
    }
}