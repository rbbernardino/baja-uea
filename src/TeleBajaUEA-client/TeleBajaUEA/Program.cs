using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TeleBajaUEA
{
    static class Program
    {
        private static MenuPrincipal FormPrincipal;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormPrincipal = new MenuPrincipal();
            Application.Run(FormPrincipal);
        }

        public static void ReabrirMenuPrincipal()
        {
            FormPrincipal.Show();
        }

        public static void EncerrarPrograma()
        {
            FormPrincipal.Close();
        }
    }
}
