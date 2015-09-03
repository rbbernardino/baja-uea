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
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal2_Load(object sender, EventArgs e)
        {

        }

        private void btConfigurações_Click(object sender, EventArgs e)
        {
            Configurações conf = new Configurações();
            conf.ShowDialog();
        }
    }
}
