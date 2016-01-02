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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btCancelar.Location = new System.Drawing.Point(498, 269);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(83, 26);
            this.btCancelar.TabIndex = 16;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btOK
            // 
            this.btOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btOK.Location = new System.Drawing.Point(407, 269);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(83, 26);
            this.btOK.TabIndex = 17;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(22, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Porta:";
            // 
            // comboUSB
            // 
            this.comboUSB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.comboUSB.FormattingEnabled = true;
            this.comboUSB.Location = new System.Drawing.Point(74, 29);
            this.comboUSB.MaxDropDownItems = 4;
            this.comboUSB.Name = "comboUSB";
            this.comboUSB.Size = new System.Drawing.Size(97, 28);
            this.comboUSB.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(36, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Manter Backup";
            // 
            // checkKeepBackup
            // 
            this.checkKeepBackup.AutoSize = true;
            this.checkKeepBackup.Checked = true;
            this.checkKeepBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkKeepBackup.Location = new System.Drawing.Point(15, 36);
            this.checkKeepBackup.Name = "checkKeepBackup";
            this.checkKeepBackup.Size = new System.Drawing.Size(15, 14);
            this.checkKeepBackup.TabIndex = 21;
            this.checkKeepBackup.UseVisualStyleBackColor = true;
            this.checkKeepBackup.CheckedChanged += new System.EventHandler(this.checkKeepBackup_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Local:";
            // 
            // textBackupPath
            // 
            this.textBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.textBackupPath.Location = new System.Drawing.Point(64, 60);
            this.textBackupPath.Name = "textBackupPath";
            this.textBackupPath.Size = new System.Drawing.Size(441, 23);
            this.textBackupPath.TabIndex = 23;
            this.textBackupPath.Enter += new System.EventHandler(this.textBackupPath_Enter);
            // 
            // btBackupPath
            // 
            this.btBackupPath.Font = new System.Drawing.Font("Cambria", 10.25F);
            this.btBackupPath.Location = new System.Drawing.Point(511, 61);
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
            this.labelPortError.Location = new System.Drawing.Point(71, 60);
            this.labelPortError.Name = "labelPortError";
            this.labelPortError.Size = new System.Drawing.Size(225, 13);
            this.labelPortError.TabIndex = 25;
            this.labelPortError.Text = "A configuração de porta anterior será mantida:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelPortError);
            this.groupBox1.Controls.Add(this.comboUSB);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox1.Location = new System.Drawing.Point(24, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 100);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conexão com o XBee (USB)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkKeepBackup);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btBackupPath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBackupPath);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox2.Location = new System.Drawing.Point(24, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(549, 118);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Backup de Corrida (durante gravação)";
            // 
            // Configurações
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 315);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurações";
            this.Text = "Configurações - TeleBaja UEA";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configurações_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}