using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    static class Program
    {
        public static Version AssemblyVersion
        {
            get
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
        }

        private static MenuPrincipal formMenuPrincipal;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramSettings.LoadFromFile();

            try
            {
                string[] activationData;
                if(GetActivationData(out activationData))
                    args = activationData;

                if (args != null && args.Length >= 1)
                {
                    formMenuPrincipal = new MenuPrincipal(args[0]);
                    Application.Run();
                }
                else
                {
                    formMenuPrincipal = new MenuPrincipal();
                    Application.Run(formMenuPrincipal);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("ERRO!\n\n" +
                    e.Message + "\n\n" +
                    e.StackTrace + "\n\n");
            }

            //SerialTest formSerialTest = new SerialTest();
            //Application.Run(formSerialTest);
        }

        // fonte: https://robindotnet.wordpress.com/2010/03/21/how-to-pass-arguments-to-an-offline-clickonce-application/
        private static bool GetActivationData(out string[] activationData)
        {
            // se executado pelo VisualStudio ou diretamente o .exe, isso não é válido
            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null)
            {
                activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                return true;
            }
            else
                activationData = null;
                return false;
        }

        public static void ShowMenuPrincipal()
        {
            formMenuPrincipal.Show();
        }

        public static void Exit()
        {
            formMenuPrincipal.Close();
        }
    }
}
