namespace TeleBajaUEA_client
{
    partial class MenuPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Gravar Corrida";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(213, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 31);
            this.button4.TabIndex = 3;
            this.button4.Text = "Sobre";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(277, 188);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 31);
            this.button5.TabIndex = 4;
            this.button5.Text = "Sair";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "TeleBaja - UEA";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 31);
            this.button2.TabIndex = 6;
            this.button2.Text = "Configurações";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(47, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 31);
            this.button3.TabIndex = 7;
            this.button3.Text = "Analisar Corrida";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 231);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuPrincipal";
            this.Text = "TeleBaja-UEA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}