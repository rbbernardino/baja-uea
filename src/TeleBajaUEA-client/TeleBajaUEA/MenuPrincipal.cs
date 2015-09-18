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

        private void btConfigurações_Click(object sender, EventArgs e)
        {
            Configurações conf = new Configurações();
            conf.ShowDialog();
        }

        private async void btGravarCorrida_Click(object sender, EventArgs e)
        {
            GravarCorridaConexão formGravarCorridaConexao = new GravarCorridaConexão();
            formGravarCorridaConexao.Show();
            Hide();
            await formGravarCorridaConexao.CreateConnections();

            // cria janela de gravar corrida
            GravarCorrida formGravarCorrida = new GravarCorrida();

            // fecha janela de loading evitando que programa encerre
            formGravarCorridaConexao.CloseOnlyThis();

            // Informa CarConnection aonde deve enviar as info
            CarConnection.FormGravarCorrida = formGravarCorrida;

            // abre janela de gravar corrida
            formGravarCorrida.Show();

            // inicia recebimento de dados dos sensores
            CarConnection.StartListen();

            // simula medições do carro
            CarConnection.StartDataGenerator();

            // inicia atualização dos gráficos na tela de gravação de corrida
            formGravarCorrida.StartUpdateGraph();
        }

        private async void btAnalisarCorrida_Click(object sender, EventArgs e)
        {
            AnalisarCorridaConexao formAnalisarCorridaConexao = new AnalisarCorridaConexao();
            formAnalisarCorridaConexao.Show();
            Hide();

            await formAnalisarCorridaConexao.CreateConnection();

            AnalisarCorrida formAnalisarCorrida = new AnalisarCorrida();

            // abre janela de buscar corrida e remove a atual
            BuscarCorrida formBuscarCorrida = new BuscarCorrida();
            formAnalisarCorridaConexao.CloseOnlyThis();
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
