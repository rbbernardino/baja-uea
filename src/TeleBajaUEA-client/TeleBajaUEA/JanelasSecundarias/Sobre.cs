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
    public partial class Sobre : Form
    {
        public Sobre()
        {
            InitializeComponent();
            var version = Program.AssemblyVersion;
            labelVersion.Text = String.Format("Versão {0}", version);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
