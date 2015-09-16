using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA
{
   public class FormPrincipal : System.Windows.Forms.Form
    {
        /// <summary>
        /// flag para indicar se, ao fechar a janela (Alt + F4 ou botão X)
        /// deve ou não encerrar o programa
        /// </summary>
        public bool ExitOnClose { get; set; }

        public FormPrincipal() : base()
        {
            ExitOnClose = true;
        }

        public void CloseOnlyThis()
        {
            ExitOnClose = false;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (ExitOnClose)
                Program.Exit();
        }
    }
}
