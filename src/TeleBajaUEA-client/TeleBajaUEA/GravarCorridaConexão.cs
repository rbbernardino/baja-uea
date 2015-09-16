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
    public partial class GravarCorridaConexão : FormExtendido
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
            await Task.Run(() =>
            {
                // cria conexão com o BD
                // Program.ConectarBD();

                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                Thread.Sleep(1000);

                // feedback de que foi um sucesso
                loadingIconBD.Image = (Image)(resources.GetObject("doneIcon.Image"));
                labelConexaoBD.Text = "Conectado!";
            });
        }

        private async Task ConnectToCar()
        {
            // cria conexão com o carro
            await Task.Run(() =>
            {
                // cria conexão com o Carro
                // Program.ConectarCarro();

                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                Thread.Sleep(1000);

                // indica que foi um sucesso
                loadingIconBaja.Image = (Image)(resources.GetObject("doneIcon.Image"));
                labelConexaoBaja.Text = "Conectado!";

                // dá um tempo para o usuário perceber que conectou
                // (feedback prazeroso)
                Thread.Sleep(1000);
            });
        }
    }
}
