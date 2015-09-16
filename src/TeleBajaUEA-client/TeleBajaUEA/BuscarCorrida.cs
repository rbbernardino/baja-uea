﻿using System;
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
    public partial class BuscarCorrida : FormPrincipal
    {
        public BuscarCorrida()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            // abre janela de analisar corrida
            AnalisarCorrida formGravarCorrida = new AnalisarCorrida();
            formGravarCorrida.Show();

            // fecha janela atual evitando que programa encerre
            CloseOnlyThis();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            // reabre janela de menu principal
            Program.ReabrirMenuPrincipal();

            // fecha janela atual evitando que programa encerre
            CloseOnlyThis();
        }
    }
}