﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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

            formMenuPrincipal = new MenuPrincipal();
            Application.Run(formMenuPrincipal);
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
