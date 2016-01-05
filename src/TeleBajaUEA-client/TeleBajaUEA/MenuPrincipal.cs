using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public MenuPrincipal(string fileName)
        {
            InitializeComponent();
            RaceData raceData = RaceFile.LoadFromFile(fileName);

            AnalisarCorrida formAnalisarCorrida = new AnalisarCorrida(raceData);

            formAnalisarCorrida.Show();
        }

        private void btConfigurações_Click(object sender, EventArgs e)
        {
            Configurações conf = new Configurações();
            conf.ShowDialog();
        }

        private void btGravarCorrida_Click(object sender, EventArgs e)
        {
            //GravarCorridaSetup formSetup2 = new GravarCorridaSetup();
            //Hide();
            //formSetup2.Show();
            //return;
            // TODO TESTE

            if (CarConnection.AvaiablePortExists)
            {
                if(Program.Settings.PortXBee == "NULL")
                    ErrorMessage.Show(ErrorType.Warnning, ErrorReason.NoPortSet);
                else
                {
                    if (CarConnection.IsPortAvaiable(Program.Settings.PortXBee))
                    {
                        GravarCorridaSetup formSetup = new GravarCorridaSetup();
                        Hide();
                        formSetup.Show();
                    }
                    else
                        ErrorMessage.Show(ErrorType.Error, ErrorReason.PortUnreachable);
                }
            }
            else
                ErrorMessage.Show(ErrorType.Error, ErrorReason.NoAvaiablePort);
        }

        private void btAnalisarCorrida_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Arquivos TeleBajaUEA (*.tbu,*.btbu)|*.tbu;*.btbu|Todos os Arquivos (*.*)|*.*";
            openDialog.FilterIndex = 1;
            openDialog.RestoreDirectory = true;

            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                RaceData raceData = RaceFile.LoadFromFile(openDialog.FileName);
                    
                AnalisarCorrida formAnalisarCorrida = new AnalisarCorrida(raceData);

                Hide();
                formAnalisarCorrida.Show();
            }
        }

        private void btSobre_Click(object sender, EventArgs e)
        {
            Sobre sobre = new Sobre();
            sobre.ShowDialog();
        }

        private void btSair_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }

        }

        private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
