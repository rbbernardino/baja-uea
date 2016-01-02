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
            this.labelTítulo = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboUSB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkKeepBackup = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBackupPath = new System.Windows.Forms.TextBox();
            this.btBackupPath = new System.Windows.Forms.Button();
            this.labelPortError = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(34, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Porta USB do XBee:";
            // 
            // comboUSB
            // 
            this.comboUSB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.comboUSB.FormattingEnabled = true;
            this.comboUSB.Location = new System.Drawing.Point(203, 96);
            this.comboUSB.MaxDropDownItems = 4;
            this.comboUSB.Name = "comboUSB";
            this.comboUSB.Size = new System.Drawing.Size(97, 28);
            this.comboUSB.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(34, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Manter Backup";
            // 
            // checkKeepBackup
            // 
            this.checkKeepBackup.AutoSize = true;
            this.checkKeepBackup.Checked = true;
            this.checkKeepBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkKeepBackup.Location = new System.Drawing.Point(203, 150);
            this.checkKeepBackup.Name = "checkKeepBackup";
            this.checkKeepBackup.Size = new System.Drawing.Size(15, 14);
            this.checkKeepBackup.TabIndex = 21;
            this.checkKeepBackup.UseVisualStyleBackColor = true;
            this.checkKeepBackup.CheckedChanged += new System.EventHandler(this.checkKeepBackup_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(34, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Pasta de backup:";
            // 
            // textBackupPath
            // 
            this.textBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.textBackupPath.Location = new System.Drawing.Point(177, 194);
            this.textBackupPath.Name = "textBackupPath";
            this.textBackupPath.Size = new System.Drawing.Size(358, 23);
            this.textBackupPath.TabIndex = 23;
            this.textBackupPath.Enter += new System.EventHandler(this.textBackupPath_Enter);
            // 
            // btBackupPath
            // 
            this.btBackupPath.Font = new System.Drawing.Font("Cambria", 10.25F);
            this.btBackupPath.Location = new System.Drawing.Point(541, 195);
            this.btBackupPath.Name = "btBackupPath";
            this.btBackupPath.Size = new System.Drawing.Size(29, 23);
            this.btBackupPath.TabIndex = 24;
            this.btBackupPath.Text = "...";
            this.btBackupPath.UseVisualStyleBackColor = true;
            this.btBackupPath.Click += new System.EventHandler(this.btDefaultPath_Click);
            // 
            // labelPortError
            // 
            this.labelPortError.AutoSize = true;
            this.labelPortError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelPortError.ForeColor = System.Drawing.Color.DarkRed;
            this.labelPortError.Location = new System.Drawing.Point(201, 127);
            this.labelPortError.Name = "labelPortError";
            this.labelPortError.Size = new System.Drawing.Size(225, 13);
            this.labelPortError.TabIndex = 25;
            this.labelPortError.Text = "A configuração de porta anterior será mantida:";
            // 
            // Configurações
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 358);
            this.Controls.Add(this.labelPortError);
            this.Controls.Add(this.btBackupPath);
            this.Controls.Add(this.textBackupPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkKeepBackup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboUSB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.labelTítulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurações";
            this.Text = "Configurações - TeleBaja UEA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configurações_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTítulo;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboUSB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkKeepBackup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBackupPath;
        private System.Windows.Forms.Button btBackupPath;
        private System.Windows.Forms.Label labelPortError;
    }
}