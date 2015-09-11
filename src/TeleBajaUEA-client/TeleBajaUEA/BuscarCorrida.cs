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
    public partial class BuscarCorrida : Form
    {
        private bool appEnd = true;

        public BuscarCorrida()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BuscarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(appEnd)
                Program.EncerrarPrograma();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            // abre janela de analisar corrida
            AnalisarCorrida formGravarCorrida = new AnalisarCorrida();
            formGravarCorrida.Show();

            // fecha janela atual evitando que programa encerre
            appEnd = false;
            Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            // reabre janela de menu principal
            Program.ReabrirMenuPrincipal();

            // fecha janela atual evitando que programa encerre
            appEnd = false;
            Close();
        }
    }
}
