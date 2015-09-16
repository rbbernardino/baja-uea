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
        }

        public async Task CriarConexao()
        {
            await ConectarComBD();
        }

        private async Task ConectarComBD()
        {
            // cria conexão com o BD
            if (await DBConnection.ConnectToDB())
            {
                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                await Task.Delay(1000);

                // feedback de que foi um sucesso
                loadingIconBD.Image = (Image)(resources.GetObject("doneIcon.Image"));
                labelConexaoBD.Text = "Conectado!";
            }
            else
            {
                // trata erro aqui?
            }
        }
    }
}
