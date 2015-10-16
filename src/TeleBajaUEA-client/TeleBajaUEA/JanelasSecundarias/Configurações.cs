using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    public partial class Configurações : FormPrincipal
    {
        public Configurações()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MenuPrincipal aux = new MenuPrincipal();
            aux.Show();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            // TODO salvar configurações
            // SaveSettings();
            CloseOnlyThis();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
        }
    }
}
