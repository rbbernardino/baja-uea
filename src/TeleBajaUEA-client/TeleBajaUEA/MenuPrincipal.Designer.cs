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
            this.label1_Título.Location = new System.Drawing.Point(108, 21);
            this.label1_Título.Name = "label1_Título";
            this.label1_Título.Size = new System.Drawing.Size(139, 24);
            this.label1_Título.TabIndex = 0;
            this.label1_Título.Text = "TeleBaja - UEA";
            // 
            // btAnalisarCorrida
            // 
            this.btAnalisarCorrida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btAnalisarCorrida.Location = new System.Drawing.Point(112, 115);
            this.btAnalisarCorrida.Name = "btAnalisarCorrida";
            this.btAnalisarCorrida.Size = new System.Drawing.Size(123, 35);
            this.btAnalisarCorrida.TabIndex = 2;
            this.btAnalisarCorrida.Text = "Analisar Corrida";
            this.btAnalisarCorrida.UseVisualStyleBackColor = true;
            this.btAnalisarCorrida.Click += new System.EventHandler(this.btAnalisarCorrida_Click);
            // 
            // btConfigurações
            // 
            this.btConfigurações.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btConfigurações.Location = new System.Drawing.Point(112, 162);
            this.btConfigurações.Name = "btConfigurações";
            this.btConfigurações.Size = new System.Drawing.Size(123, 35);
            this.btConfigurações.TabIndex = 3;
            this.btConfigurações.Text = "Configurações";
            this.btConfigurações.UseVisualStyleBackColor = true;
            this.btConfigurações.Click += new System.EventHandler(this.btConfigurações_Click);
            // 
            // btSair
            // 
            this.btSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btSair.Location = new System.Drawing.Point(202, 217);
            this.btSair.Name = "btSair";
            this.btSair.Size = new System.Drawing.Size(95, 35);
            this.btSair.TabIndex = 5;
            this.btSair.Text = "Sair";
            this.btSair.UseVisualStyleBackColor = true;
            this.btSair.Click += new System.EventHandler(this.btSair_Click);
            // 
            // btSobre
            // 
            this.btSobre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btSobre.Location = new System.Drawing.Point(50, 217);
            this.btSobre.Name = "btSobre";
            this.btSobre.Size = new System.Drawing.Size(95, 35);
            this.btSobre.TabIndex = 4;
            this.btSobre.Text = "Sobre";
            this.btSobre.UseVisualStyleBackColor = true;
            this.btSobre.Click += new System.EventHandler(this.btSobre_Click);
            // 
            // btGravarCorrida
            // 
            this.btGravarCorrida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btGravarCorrida.Location = new System.Drawing.Point(112, 74);
            this.btGravarCorrida.Name = "btGravarCorrida";
            this.btGravarCorrida.Size = new System.Drawing.Size(123, 35);
            this.btGravarCorrida.TabIndex = 1;
            this.btGravarCorrida.Text = "Gravar Corrida";
            this.btGravarCorrida.UseVisualStyleBackColor = true;
            this.btGravarCorrida.Click += new System.EventHandler(this.btGravarCorrida_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 271);
            this.Controls.Add(this.btAnalisarCorrida);
            this.Controls.Add(this.btConfigurações);
            this.Controls.Add(this.btSair);
            this.Controls.Add(this.btSobre);
            this.Controls.Add(this.btGravarCorrida);
            this.Controls.Add(this.label1_Título);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeleBaja-UEA";
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