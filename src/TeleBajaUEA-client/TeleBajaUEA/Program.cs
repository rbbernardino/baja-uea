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

            if(args.Length >= 1)
            {
                formMenuPrincipal = new MenuPrincipal(args[0]);
                Application.Run();
            }
            else
            {
                formMenuPrincipal = new MenuPrincipal();
                Application.Run(formMenuPrincipal);
            }


            //SerialTest formSerialTest = new SerialTest();
            //Application.Run(formSerialTest);
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
