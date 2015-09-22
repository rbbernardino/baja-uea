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
    public partial class Sobre : FormPrincipal
    {
        public Sobre()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
        }
    }
}
