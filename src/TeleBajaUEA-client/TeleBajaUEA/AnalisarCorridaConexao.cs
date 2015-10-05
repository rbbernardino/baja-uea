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
        public AnalisarCorridaConexao()
        {
            InitializeComponent();
        }

        public async Task CreateConnection()
        {
            await ConnectToDB();
        }   

        private async Task ConnectToDB()
        {
            // cria conexão com o BD
            if (await DBConnection.ConnectToDB())
            {
                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                await Task.Delay(1000);

                // feedback de que foi um sucesso
                loadingIconBD.Image = Properties.Resources.done;
                labelConexaoBD.Text = "Conectado!";
                await Task.Delay(1000); // tempo para notar que carregou
            }
            else
            {
                // trata erro aqui?
            }
        }
    }
}
