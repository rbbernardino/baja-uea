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
using TeleBajaUEA.GravacaoDeCorrida;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA.AnalisarCorridaPckg
{
    public partial class AnalisarCorridaSetup : Form
    {
        private RaceParameters parameters = new RaceParameters();

        public AnalisarCorridaSetup(RaceParameters pParameters)
        {
            InitializeComponent();
            SetTags();
            parameters = pParameters;
            SetParameters();
            DisableControls();
        }

        private void DisableControls()
        {
            foreach (TabPage page in tabControl1.TabPages)
            {
                InnerDisable(page.Controls);

                foreach (GroupBox box in page.Controls.OfType<GroupBox>().ToList())
                    InnerDisable(box.Controls);
            }
        }

        private void InnerDisable(Control.ControlCollection controls)
        {
            foreach (TextBox text in controls.OfType<TextBox>().ToList())
            {
                text.BackColor = SystemColors.Window;
                text.BorderStyle = BorderStyle.None;
                text.Location = new Point(text.Location.X, text.Location.Y + 5);
                text.ReadOnly = true;
            }

            foreach (RadioButton radio in controls.OfType<RadioButton>().ToList())
                if (!radio.Checked)
                    radio.Enabled = false;

            foreach (ComboBox combo in controls.OfType<ComboBox>().ToList())
                combo.Enabled = false;
        }

        private void SetRadioClima(char checkedRadio)
        {
            switch (checkedRadio)
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
            switch (checkedRadio)
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
            textCarLarg.Text = parameters.largTotal.ToString();

            textCarPneuDiaExt_front.Text = parameters.rodaDiamExternoFront.ToString();
            textCarPneuDiaExt_rear.Text = parameters.rodaDiamExternoRear.ToString();
            textCarPneuAro_front.Text = parameters.rodaRaioAroFront.ToString();
            textCarPneuAro_rear.Text = parameters.rodaRaioAroRear.ToString();

            txtPneuBandFront.Text = parameters.rodaBandagemFront.ToString();
            txtPneuBandRear.Text = parameters.rodaBandagemRear.ToString();

            textCarPneuPressao.Text = parameters.pneuPressaoFront.ToString();
            textCarPneuPressaoRear.Text = parameters.pneuPressaoRear.ToString();
            textCarPneuMarcaFront.Text = parameters.pneuMarcaFront;
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
                txtAmort2.Visible = true;
            else
                txtAmort2.Visible = false;

            txtAmort1.Text = parameters.preCargaAmort1.ToString();

            textCarRoll.Text = parameters.rollcenter.ToString();
            textFrontToeL.Text = parameters.frontToeL.ToString();
            textFrontToeR.Text = parameters.frontToeR.ToString();
            textRearToeL.Text = parameters.rearToeL.ToString();
            textRearToeR.Text = parameters.rearToeR.ToString();

            textFrontCamberL.Text = parameters.frontCamberL.ToString();
            textFrontCamberR.Text = parameters.frontCamberR.ToString();
            textRearCamberL.Text = parameters.rearCamberL.ToString();
            textRearCamberR.Text = parameters.rearCamberR.ToString();

            textCaster.Text = parameters.caster.ToString();
            textAckermann.Text = parameters.ackermann.ToString();
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

        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}