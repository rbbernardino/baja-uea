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
    public partial class AnalisarCorridaSetup : Form
    {
        private RaceParameters parameters = new RaceParameters();

        public AnalisarCorridaSetup(RaceParameters pParameters)
        {
            InitializeComponent();
            parameters = pParameters;
            SetParameters();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetParameters()
        {
            txtPilNome.Text = parameters.pilNome;
            txtPilPeso.Text = parameters.pilPeso.ToString();
            txtPilAltura.Text = parameters.pilAltura.ToString();

            txtClimTemp.Text = parameters.climaTemp.ToString();

            switch (parameters.clima)
            {
                case 'C':
                    textClima.Text = "Chuvoso";
                    break;
                case 'E':
                    textClima.Text = "Ensolarado";
                    break;
                case 'N':
                    textClima.Text = "Nublado";
                    break;
            }

            switch (parameters.pista)
            {
                case 'M':
                    textPista.Text = "Molhada";
                    break;
                case 'S':
                    textPista.Text = "Seca";
                    break;
                case 'P':
                    textPista.Text = "Parcialmente molhada";
                    break;
            }

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