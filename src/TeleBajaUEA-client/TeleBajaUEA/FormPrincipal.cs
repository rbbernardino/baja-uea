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
        public bool AppEnd { get; set; }

        public FormPrincipal() : base()
        {
            AppEnd = true;
        }

        public void CloseOnlyThis()
        {
            AppEnd = false;
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (AppEnd)
                Program.EncerrarPrograma();
        }
    }
}
