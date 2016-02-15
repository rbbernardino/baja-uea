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

namespace TeleBajaUEA.GravacaoDeCorrida
{
    public partial class GravarCorridaSetup : FormPrincipal
    {
        private RaceParameters parameters = new RaceParameters();
        private char radioClima;
        private char radioPista;

        public GravarCorridaSetup(RaceParameters pParameters)
        {
            InitializeComponent();
            SetTags();
            parameters = pParameters;
            SetParameters();
        }

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

        private void SetRadioClima(char checkedRadio)
        {
            radioClima = checkedRadio;
            switch (parameters.clima)
            {
                case 'C':
                    radioClimChuvoso.Checked = true;
                    break;
                case 'E':
                    radioClimEnsolarado.Checked = true;
                    break;
                case 'N':
                    radioClimNublado.Checked = true;
                    break;
            }
        }

        private void SetRadioPista(char checkedRadio)
        {
            radioPista = checkedRadio;
            switch (parameters.pista)
            {
                case 'M':
                    radioPistaMolhada.Checked = true;
                    break;
                case 'S':
                    radioPistaSeca.Checked = true;
                    break;
                case 'P':
                    radioPistaParcMolhada.Checked = true;
                    break;
            }
        }

        private void SetCombo(ComboBox combo, int value)
        {
            int itemInt;
            foreach(object item in combo.Items)
                if(int.TryParse(item.ToString(), out itemInt))
                    if (itemInt == value)
                        combo.SelectedItem = item;
        }

        private void SetCombo(ComboBox combo, string value)
        {
            foreach (object item in combo.Items)
                if (((string)item).Equals(value))
                    combo.SelectedItem = item;
        }

        private void SetParameters()
        {
            txtPilNome.Text = parameters.pilNome;
            txtPilPeso.Text = parameters.pilPeso.ToString();
            txtPilAltura.Text = parameters.pilAltura.ToString();

            txtClimTemp.Text = parameters.climaTemp.ToString();

            SetRadioClima(parameters.clima);

            SetRadioPista(parameters.pista);

            textCarPeso.Text = parameters.carPeso.ToString();
            textCarComp.Text = parameters.compTotal.ToString();
            textCarAlt.Text = parameters.altTotal.ToString();
            textCarAlt.Text = parameters.largTotal.ToString();

            textCarPneuDiaExt_front.Text = parameters.rodaDiamExternoFront.ToString();
            textCarPneuDiaExt_rear.Text = parameters.rodaDiamExternoRear.ToString();
            textCarPneuAro_front.Text = parameters.rodaRaioAroFront.ToString();
            textCarPneuAro_rear.Text = parameters.rodaRaioAroRear.ToString();

            SetCombo(comboPneuBand, parameters.rodaBandagemFront);
            SetCombo(comboPneuBandRear, parameters.rodaBandagemRear);

            textCarPneuPressao.Text = parameters.pneuPressaoFront.ToString();
            textCarPneuPressaoRear.Text = parameters.pneuPressaoRear.ToString();
            textCarPneuMarca.Text = parameters.pneuMarcaFront;
            textCarPneuMarcaRear.Text = parameters.pneuMarcaRear;
            textCarPneuTipo.Text = parameters.pneuTipoFront.ToString();
            textCarPneuTipoRear.Text = parameters.pneuTipoRear.ToString();
            textCarDistEixo.Text = parameters.distEixo.ToString();
            textCarBitF.Text = parameters.bitolaF.ToString();
            textCarBitR.Text = parameters.bitolaR.ToString();
            textCarVaoF.Text = parameters.vaoLivreF.ToString();
            textCarVaoR.Text = parameters.vaoLivreR.ToString();
            textCarAntiDive.Text = parameters.antiDive.ToString();
            textCarAntiSquat.Text = parameters.antiSquat.ToString();
            textCarMola.Text = parameters.cteMola.ToString();

            if (parameters.preCargaAmortMeio == true)
                SetCombo(comboCarAmort2, "½");
            else
                SetCombo(comboCarAmort2, "");
            SetCombo(comboCarAmort, parameters.preCargaAmort1);

            parameters.rollcenter = int.Parse(textCarRoll.Text);
            parameters.frontToeL = float.Parse(textFrontToeL.Text);
            parameters.frontToeR = float.Parse(textFrontToeR.Text);
            parameters.rearToeL = float.Parse(textRearToeL.Text);
            parameters.rearToeR = float.Parse(textRearToeR.Text);
            parameters.caster = float.Parse(textCaster.Text);
            parameters.ackermann = float.Parse(textAckermann.Text);
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
            //-------------------------------------------------- TESTE-----------//
            BindParameters();
            Hide();
            await RaceFile.CreateTempFile();
            GravarCorrida formGravarCorrida = new GravarCorrida(parameters);
            await formGravarCorrida.ConfigureCharts();
            formGravarCorrida.Show();
            formGravarCorrida.StartUpdateCharts();
            return;
            //----------------------------------------------------------

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

            parameters.rodaDiamExternoFront = int.Parse(textCarPneuDiaExt_front.Text);
            parameters.rodaDiamExternoRear = int.Parse(textCarPneuDiaExt_rear.Text);
            parameters.rodaRaioAroFront = int.Parse(textCarPneuAro_front.Text);
            parameters.rodaRaioAroRear = int.Parse(textCarPneuAro_rear.Text);
            parameters.rodaBandagemFront = int.Parse(comboPneuBand.SelectedItem.ToString());
            parameters.rodaBandagemRear = int.Parse(comboPneuBandRear.SelectedItem.ToString());
            parameters.pneuPressaoFront = float.Parse(textCarPneuPressao.Text);
            parameters.pneuPressaoRear = float.Parse(textCarPneuPressaoRear.Text);
            parameters.pneuMarcaFront = textCarPneuMarca.Text;
            parameters.pneuMarcaFront = textCarPneuMarcaRear.Text;
            parameters.pneuTipoFront = int.Parse(textCarPneuTipo.Text);
            parameters.pneuTipoRear = int.Parse(textCarPneuTipoRear.Text);
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