using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class GravarCorridaSetup : FormPrincipal
    {
        private RaceParameters parameters;

        public GravarCorridaSetup()
        {
            InitializeComponent();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }

        private async void btIniciar_Click(object sender, EventArgs e)
        {
            Hide();
            await ShowGravarCorrida();
            CloseOnlyThis();
        }

        private async Task ShowGravarCorrida()
        {
            GravarCorridaConexão formGravarCorridaConexao = new GravarCorridaConexão();
            formGravarCorridaConexao.Show();

            await formGravarCorridaConexao.CreateConnections();

            // cria janela de gravar corrida
            GravarCorrida formGravarCorrida = new GravarCorrida(parameters);
            await formGravarCorrida.ConfigureCharts();

            //------------- temporário para testar -----------------//
            formGravarCorrida.formTesteMQSQ = new TESTEJanelaSensores();
            //------------------------------------------------------//

            // fecha janela de loading evitando que programa encerre
            formGravarCorridaConexao.CloseOnlyThis();

            // Informa CarConnection aonde deve enviar as info
            CarConnection.FormGravarCorrida = formGravarCorrida;

            // abre janela de gravar corrida
            formGravarCorrida.Show();

            // inicia recebimento de dados dos sensores
            CarConnection.StartListen();

            // ------------------ Teste: simula medições do carro --------------
            //CarConnection.StartDataGenerator();

            // inicia atualização dos gráficos na tela de gravação de corrida
            formGravarCorrida.StartUpdateCharts();

            //--------------------------- teste ----------------------------//
            formGravarCorrida.formTesteMQSQ.StartCountTime();
            
            // seta posição melhor
            formGravarCorrida.formTesteMQSQ.StartPosition = FormStartPosition.Manual;
            Point parentLoc = formGravarCorrida.Location;
            formGravarCorrida.formTesteMQSQ.Location = new Point(parentLoc.X-100, parentLoc.Y);
            formGravarCorrida.formTesteMQSQ.Show();
            //--------------------------------------------------------------//
        }
    }
}