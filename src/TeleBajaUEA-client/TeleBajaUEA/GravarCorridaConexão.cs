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
    public partial class GravarCorridaConexão : Form
    {
        private bool appEnd = true;
        private ComponentResourceManager resources;

        public GravarCorridaConexão()
        {
            this.resources = new ComponentResourceManager(typeof(GravarCorridaConexão));
            InitializeComponent();
            ConectarComBD();
        }

        private async void ConectarComBD()
        {
            // cria conexão com o BD
            await Task.Delay(1000);

            // indica que foi um sucesso
            loadingIconBD.Image = (Image)(resources.GetObject("doneIcon.Image"));
            labelConexaoBD.Text = "Conectado!";

            ConnectToCar();
        }

        private async void ConnectToCar()
        {

            // cria conexão com o carro
            await Task.Delay(1000);

            // indica que foi um sucesso
            loadingIconBaja.Image = (Image)(resources.GetObject("doneIcon.Image"));
            labelConexaoBaja.Text = "Conectado!";

            // dá um tempo para o usuário perceber que conectou
            await Task.Delay(1000);

            // abre janela de gravar corrida e remove a atual
            GravarCorrida formGravarCorrida = new GravarCorrida();
            formGravarCorrida.Show();

            // fecha janela evitando que programa encerre
            appEnd = false;
            Close();
        }

        private void GravarCorridaConexão_FormClosed(object sender, FormClosedEventArgs e)
        {
            // se fechar janela no botão do canto superior direito da tela, encerra o programa
            if(appEnd)
                Program.EncerrarPrograma();
        }
    }
}
