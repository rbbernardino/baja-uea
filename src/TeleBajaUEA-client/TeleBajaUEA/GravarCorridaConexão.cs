using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    public partial class GravarCorridaConexão : FormPrincipal
    {
        private ComponentResourceManager resources;

        public GravarCorridaConexão()
        {
            this.resources = new ComponentResourceManager(typeof(GravarCorridaConexão));
            InitializeComponent();
        }

        public async Task CriarConexoes()
        {
            await ConectarComBD();
            await ConnectToCar();
        }

        private async Task ConectarComBD()
        {
            // cria conexão com o BD
            if(await DBConnection.ConnectToDB())
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

        private async Task ConnectToCar()
        {
            // cria conexão com o BD
            if (await DBConnection.ConnectToDB())
            {
                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                await Task.Delay(1000);

                // feedback de que foi um sucesso
                loadingIconBaja.Image = (Image)(resources.GetObject("doneIcon.Image"));
                labelConexaoBaja.Text = "Conectado!";

                // dá um tempo para o usuário perceber que conectou
                // (feedback prazeroso)
                await Task.Delay(1000);
            }
            else
            {
                // trata erro aqui?
            }
        }
    }
}
