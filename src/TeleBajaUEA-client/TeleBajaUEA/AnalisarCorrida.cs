using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    public partial class AnalisarCorrida : FormPrincipal
    {
        public AnalisarCorrida()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MenuPrincipal aux = new MenuPrincipal();
            aux.Show();
        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            BuscarCorrida formBuscarCorrida = new BuscarCorrida();
            CloseOnlyThis();
            formBuscarCorrida.Show();
        }
    }
}
