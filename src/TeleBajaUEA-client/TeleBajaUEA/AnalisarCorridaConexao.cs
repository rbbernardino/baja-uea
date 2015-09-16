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
    public partial class AnalisarCorridaConexao : FormPrincipal
    {
        private ComponentResourceManager resources;

        public AnalisarCorridaConexao()
        {
            this.resources = new ComponentResourceManager(typeof(AnalisarCorridaConexao));
            InitializeComponent();
            ConectarComBanco();
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.EncerrarPrograma();
        }

        private async void ConectarComBanco()
        {
            // cria conexão com o BD
            await Task.Delay(1000);

            // indica que foi um sucesso
            loadingIconBD.Image = (Image)(resources.GetObject("doneIcon.Image"));
            labelConexaoBD.Text = "Conectado!";

            // dá um tempo para o usuário perceber que conectou
            await Task.Delay(1000);

            // abre janela de gravar corrida e remove a atual
            BuscarCorrida formBuscarCorrida = new BuscarCorrida();
            formBuscarCorrida.Show();
            CloseOnlyThis();
        }
    }
}
