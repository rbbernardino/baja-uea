using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    static class Program
    {
        private static MenuPrincipal formMenuPrincipal;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramSettings.LoadFromFile();

            formMenuPrincipal = new MenuPrincipal();
            Application.Run(formMenuPrincipal);
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
