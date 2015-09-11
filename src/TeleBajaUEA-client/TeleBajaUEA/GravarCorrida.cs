﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    public partial class GravarCorrida : Form
    {
        private bool appEnd = true;

        public GravarCorrida()
        {
            InitializeComponent();
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(appEnd)
                Program.EncerrarPrograma();
        }
    }
}
