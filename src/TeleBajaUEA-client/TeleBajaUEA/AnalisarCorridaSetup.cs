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
    public partial class AnalisarCorridaSetup : FormPrincipal
    {
        private RaceParameters parameters = new RaceParameters();
        private char radioClima;
        private char radioPista;

        public AnalisarCorridaSetup(RaceParameters pParameters)
        {
            InitializeComponent();
            parameters = pParameters;
            SetTags();
            SetParameters();
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
            CloseOnlyThis();
        }

        private void SetParameters()
        {
            txtPilNome.Text = parameters.pilNome;
            txtPilPeso.Text = parameters.pilPeso.ToString();
            txtPilAltura.Text = parameters.pilAltura.ToString();

            txtClimTemp.Text = parameters.climaTemp.ToString();

            textCarPeso.Text = parameters.carPeso.ToString();
            textCarComp.Text = parameters.compTotal.ToString();
            textCarAlt.Text = parameters.altTotal.ToString();
            textCarAlt.Text = parameters.largTotal.ToString();

            textCarPneuDiaExt.Text = parameters.rodaDiamExterno.ToString();
            textCarPneuAro.Text = parameters.rodaRaioAro.ToString();

            textPneuBand.Text = parameters.rodaBandagem.ToString();

            textCarPneuPressao.Text = parameters.pneuPressao.ToString();
            textCarPneuMarca.Text = parameters.pneuMarca;
            textCarPneuTipo.Text = parameters.pneuTipo.ToString();
            textCarDistEixo.Text = parameters.distEixo.ToString();
            textCarBitF.Text = parameters.bitolaF.ToString();
            textCarBitR.Text = parameters.bitolaR.ToString();
            textCarVaoF.Text = parameters.vaoLivreF.ToString();
            textCarVaoR.Text = parameters.vaoLivreR.ToString();
            textCarAntiDive.Text = parameters.antiDive.ToString();
            textCarAntiSquat.Text = parameters.antiSquat.ToString();
            textCarMola.Text = parameters.cteMola.ToString();

            if (parameters.preCargaAmortMeio == false)
            {
                textCargAmortecedor.Text = parameters.preCargaAmort1.ToString() + "T";
            }
            else
                textCargAmortecedor.Text = parameters.preCargaAmort1.ToString() + "T½";

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