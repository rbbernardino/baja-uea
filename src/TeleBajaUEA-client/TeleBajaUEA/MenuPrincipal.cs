using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private async void btGravarCorrida_Click(object sender, EventArgs e)
        {
            GravarCorridaConexão formLoadingGravarCorrida = new GravarCorridaConexão();
            formLoadingGravarCorrida.Show();
            Hide();
            await formLoadingGravarCorrida.CriarConexoes();

            // cria janela de gravar corrida
            GravarCorrida formGravarCorrida = new GravarCorrida();

            // fecha janela de loading evitando que programa encerre
            formLoadingGravarCorrida.CloseOnlyThis();
            formGravarCorrida.Show();
        }

        private async void btAnalisarCorrida_Click(object sender, EventArgs e)
        {
            AnalisarCorridaConexao formLoadingAnalisarCorrida = new AnalisarCorridaConexao();
            formLoadingAnalisarCorrida.Show();
            Hide();

            await formLoadingAnalisarCorrida.CriarConexao();

            AnalisarCorrida formAnalisarCorrida = new AnalisarCorrida();

            // abre janela de buscar corrida e remove a atual
            BuscarCorrida formBuscarCorrida = new BuscarCorrida();
            formLoadingAnalisarCorrida.CloseOnlyThis();
            formBuscarCorrida.Show();
        }

        private void btSobre_Click(object sender, EventArgs e)
        {
            Sobre sobre = new Sobre();
            sobre.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Setup setup = new Setup();
            setup.ShowDialog();
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }
    }
}
