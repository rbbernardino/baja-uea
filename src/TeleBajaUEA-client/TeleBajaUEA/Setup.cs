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
    public partial class Setup : FormPrincipal
    {
        public Setup()
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
            GravarCorrida formGravarCorrida = new GravarCorrida();
            await formGravarCorrida.ConfigureCharts();

            // TODO temporario para teste
            //------------- temporário para testar -----------------//
            formGravarCorrida.formTesteMQSQ = new TESTEJanelaSensores();
            formGravarCorrida.formTesteMQSQ.Show();
            //------------------------------------------------------//


            // fecha janela de loading evitando que programa encerre
            formGravarCorridaConexao.CloseOnlyThis();

            // Informa CarConnection aonde deve enviar as info
            CarConnection.FormGravarCorrida = formGravarCorrida;

            // abre janela de gravar corrida
            formGravarCorrida.Show();

            // inicia recebimento de dados dos sensores
            CarConnection.StartListen();

            // TODO temporario para teste
            // ------------------ Teste: simula medições do carro --------------
            CarConnection.StartDataGenerator();

            // inicia atualização dos gráficos na tela de gravação de corrida
            formGravarCorrida.StartUpdateCharts();
            formGravarCorrida.formTesteMQSQ.StartCountTime();
        }
    }
}