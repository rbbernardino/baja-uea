namespace TeleBajaUEA
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
            this.label1_Título = new System.Windows.Forms.Label();
            this.btAnalisarCorrida = new System.Windows.Forms.Button();
            this.btConfigurações = new System.Windows.Forms.Button();
            this.btSair = new System.Windows.Forms.Button();
            this.btSobre = new System.Windows.Forms.Button();
            this.btGravarCorrida = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1_Título
            // 
            this.label1_Título.AutoSize = true;
            this.label1_Título.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1_Título.Location = new System.Drawing.Point(112, 21);
            this.label1_Título.Name = "label1_Título";
            this.label1_Título.Size = new System.Drawing.Size(139, 24);
            this.label1_Título.TabIndex = 0;
            this.label1_Título.Text = "TeleBaja - UEA";
            this.label1_Título.Click += new System.EventHandler(this.label1_Click);
            // 
            // btAnalisarCorrida
            // 
            this.btAnalisarCorrida.Location = new System.Drawing.Point(47, 128);
            this.btAnalisarCorrida.Name = "btAnalisarCorrida";
            this.btAnalisarCorrida.Size = new System.Drawing.Size(95, 31);
            this.btAnalisarCorrida.TabIndex = 12;
            this.btAnalisarCorrida.Text = "Analisar Corrida";
            this.btAnalisarCorrida.UseVisualStyleBackColor = true;
            // 
            // btConfigurações
            // 
            this.btConfigurações.Location = new System.Drawing.Point(213, 79);
            this.btConfigurações.Name = "btConfigurações";
            this.btConfigurações.Size = new System.Drawing.Size(95, 31);
            this.btConfigurações.TabIndex = 11;
            this.btConfigurações.Text = "Configurações";
            this.btConfigurações.UseVisualStyleBackColor = true;
            this.btConfigurações.Click += new System.EventHandler(this.btConfigurações_Click);
            // 
            // btSair
            // 
            this.btSair.Location = new System.Drawing.Point(277, 188);
            this.btSair.Name = "btSair";
            this.btSair.Size = new System.Drawing.Size(75, 31);
            this.btSair.TabIndex = 10;
            this.btSair.Text = "Sair";
            this.btSair.UseVisualStyleBackColor = true;
            // 
            // btSobre
            // 
            this.btSobre.Location = new System.Drawing.Point(213, 128);
            this.btSobre.Name = "btSobre";
            this.btSobre.Size = new System.Drawing.Size(95, 31);
            this.btSobre.TabIndex = 9;
            this.btSobre.Text = "Sobre";
            this.btSobre.UseVisualStyleBackColor = true;
            // 
            // btGravarCorrida
            // 
            this.btGravarCorrida.Location = new System.Drawing.Point(47, 79);
            this.btGravarCorrida.Name = "btGravarCorrida";
            this.btGravarCorrida.Size = new System.Drawing.Size(95, 31);
            this.btGravarCorrida.TabIndex = 8;
            this.btGravarCorrida.Text = "Gravar Corrida";
            this.btGravarCorrida.UseVisualStyleBackColor = true;
            // 
            // MenuPrincipal2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 231);
            this.Controls.Add(this.btAnalisarCorrida);
            this.Controls.Add(this.btConfigurações);
            this.Controls.Add(this.btSair);
            this.Controls.Add(this.btSobre);
            this.Controls.Add(this.btGravarCorrida);
            this.Controls.Add(this.label1_Título);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuPrincipal2";
            this.Text = "TeleBaja-UEA";
            this.Load += new System.EventHandler(this.MenuPrincipal2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1_Título;
        private System.Windows.Forms.Button btAnalisarCorrida;
        private System.Windows.Forms.Button btConfigurações;
        private System.Windows.Forms.Button btSair;
        private System.Windows.Forms.Button btSobre;
        private System.Windows.Forms.Button btGravarCorrida;
    }
}