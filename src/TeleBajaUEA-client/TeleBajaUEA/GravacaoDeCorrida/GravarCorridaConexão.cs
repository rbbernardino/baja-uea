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
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA.GravacaoDeCorrida
{
    public partial class GravarCorridaConexão : FormPrincipal
    {
        public GravarCorridaConexão()
        {
            InitializeComponent();
        }

        public async Task<bool> CreateConnections()
        {
            bool result = true;
            result &= await ConnectToCar();
            result &= await RaceFile.CreateTempFile();

            if (!result)
            {
                CarConnection.CloseConnection();
                RaceFile.DeleteTempFile();
            }
            return result;
        }

        private async Task<bool> ConnectToCar()
        {
            // cria conexão com o carro
            try
            {
                await CarConnection.ConnectToCar();

                // mesmo que seja instantâneo, deve esperar 1/2 segundo
                // pois feedback de [carregando -->-->-->-- carregado] é prazerozo!
                await Task.Delay(500);

                // feedback de que foi um sucesso
                loadingIconCar.Image = Properties.Resources.done;
                labelCarConnection.Text = "Conectado!";

                // dá um tempo para o usuário perceber que conectou
                // (feedback prazeroso)
                await Task.Delay(500);
                return true;
            }
            catch (ErrorMessage.ReceiveDataTimeoutException exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ConnectToCarFailed, exception.Message);
                return false;
            }
            catch (Exception e)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ConnectToCarFailed, e.Message);
                return false;
            }
        }
    }
}
