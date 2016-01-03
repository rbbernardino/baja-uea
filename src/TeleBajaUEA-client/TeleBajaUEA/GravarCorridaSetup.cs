using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class GravarCorridaSetup : FormPrincipal
    {
        private RaceParameters parameters = new RaceParameters();
        private char radioClima;
        private char radioPista;

        public GravarCorridaSetup()
        {
            InitializeComponent();
            SetTags();
            FillDefaultValues();
        }

        private void FillDefaultValues()
        {
            radioClimEnsolarado.Checked = true;
            radioPistaMolhada.Checked = true;
            comboPneuBand.SelectedIndex = 0;
            comboCarAmort.SelectedIndex = 0;
            comboCarAmort2.SelectedIndex = 0;
        }

        private void SetTags()
        {
            radioClimChuvoso.Tag = 'C';
            radioClimEnsolarado.Tag = 'E';
            radioClimNublado.Tag = 'N';

            radioPistaMolhada.Tag = 'M';
            radioPistaSeca.Tag = 'S';
            radioPistaParcMolhada.Tag = 'P';
        }

        private void radioClima_CheckChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
                radioClima = (char) button.Tag;
        }

        private void radioPista_CheckChanged(object sender, EventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.Checked)
                radioPista = (char)button.Tag;
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }

        private async void btIniciar_Click(object sender, EventArgs e)
        {
            BindParameters();
            Hide();
            await ShowGravarCorrida();
        }

        private async Task ShowGravarCorrida()
        {
            GravarCorridaConexão formGravarCorridaConexao = new GravarCorridaConexão();
            formGravarCorridaConexao.Show();

            bool connected = await formGravarCorridaConexao.CreateConnections();

            if (!connected)
            {
                formGravarCorridaConexao.CloseOnlyThis();
                Show();
            }
            else
            {
                // cria janela de gravar corrida
                GravarCorrida formGravarCorrida = new GravarCorrida(parameters);
                await formGravarCorrida.ConfigureCharts();

                // fecha janela de loading e setup
                formGravarCorridaConexao.CloseOnlyThis();
                CloseOnlyThis();

                // abre janela de gravar corrida
                formGravarCorrida.Show();

                // TODO o código abaixo está feio, melhorar? Botar captura em outro lugar?
                try
                {
                    // inicia recebimento de dados dos sensores
                    CarConnection.StartListen();
                }
                catch(Exception e)
                {
                    ErrorMessage.Show(ErrorType.Error, ErrorReason.SendToCarFail, e.Message);
                    formGravarCorridaConexao.CloseOnlyThis();
                    Show();
                    return;
                }

                // inicia atualização dos gráficos na tela de gravação de corrida
                formGravarCorrida.StartUpdateCharts();
            }
        }

        private void BindParameters()
        {
            parameters.pilNome = txtPilNome.Text;
            parameters.pilPeso = float.Parse(txtPilPeso.Text);
            parameters.pilAltura = float.Parse(txtPilAltura.Text);

            parameters.climaTemp = int.Parse(txtClimTemp.Text);
            parameters.pista = radioPista;
            parameters.clima = radioClima;

            parameters.carPeso = float.Parse(textCarPeso.Text);
            parameters.compTotal = int.Parse(textCarComp.Text);
            parameters.altTotal = int.Parse(textCarAlt.Text);
            parameters.largTotal = int.Parse(textCarAlt.Text);

            parameters.rodaDiamExterno = int.Parse(textCarPneuDiaExt.Text);
            parameters.rodaRaioAro = int.Parse(textCarPneuAro.Text);
            parameters.rodaBandagem = int.Parse(comboPneuBand.SelectedItem.ToString());
            parameters.pneuPressao = float.Parse(textCarPneuPressao.Text);
            parameters.pneuMarca = textCarPneuMarca.Text;
            parameters.pneuTipo = int.Parse(textCarPneuTipo.Text);
            parameters.distEixo = int.Parse(textCarDistEixo.Text);
            parameters.bitolaF = int.Parse(textCarBitF.Text);
            parameters.bitolaR = int.Parse(textCarBitR.Text);
            parameters.vaoLivreF = int.Parse(textCarVaoF.Text);
            parameters.vaoLivreR = int.Parse(textCarVaoR.Text);
            parameters.antiDive = float.Parse(textCarAntiDive.Text);
            parameters.antiSquat = float.Parse(textCarAntiSquat.Text);
            parameters.cteMola = int.Parse(textCarMola.Text);
            parameters.preCargaAmort1 = int.Parse(comboCarAmort.SelectedItem.ToString());
            if (comboCarAmort2.SelectedItem.ToString() == "") // TODO tratar quando usuário não selecionar nada aqui
                parameters.preCargaAmortMeio = false;
            else
                parameters.preCargaAmortMeio = true;
            parameters.rollcenter = int.Parse(textCarRoll.Text);
            parameters.frontToeL = float.Parse(textFrontToeL.Text);
            parameters.frontToeR = float.Parse(textFrontToeR.Text);
            parameters.rearToeL = float.Parse(textRearToeL.Text);
            parameters.rearToeR = float.Parse(textRearToeR.Text);
            parameters.caster = float.Parse(textCaster.Text);
            parameters.ackermann = float.Parse(textAckermann.Text);
        }
    }
}