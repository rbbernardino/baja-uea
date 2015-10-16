﻿using System;
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
        public GravarCorridaConexão()
        {
            InitializeComponent();
        }

        public async Task CreateConnections()
        {
            await ConnectToCar();
            await RaceFile.CreateTempFile();
        }

        private async Task ConnectToCar()
        {
            // cria conexão com o BD
            if (await CarConnection.ConnectToCar())
            {
                // mesmo que seja instantâneo, deve esperar 1 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                await Task.Delay(1000);

                // feedback de que foi um sucesso
                loadingIconCar.Image = Properties.Resources.done;
                labelCarConnection.Text = "Conectado!";

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
