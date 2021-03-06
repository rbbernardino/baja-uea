﻿using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    // TODO 1- criar variável global com o nome do aplicativo (TeleBajaUEA)
    //
    // TODO 2- repensar tratamento de erros no caso de exceções do sistema
    //         exibir também a call stack nesse caso
    static class Program
    {
        public static Version AssemblyVersion
        {
            get
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    return ApplicationDeployment.CurrentDeployment.CurrentVersion;
                else
                    return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        private static MenuPrincipal formMenuPrincipal;
        public static ProgramSettings Settings { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (SettingsFile.SettingsFileExists)
                Settings = SettingsFile.LoadFromFile();
            else
            {
                Settings = new ProgramSettings();
                SettingsFile.CreateAppFilesFolder();
                SettingsFile.SaveToFile(Settings);
                Directory.CreateDirectory(Settings.BackupPath);
            }

            try
            {
                args = GetActivationData(args);

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
                string errorMessage =
                    "Erro Inesperado! \n\n" +
                    e.Message + "\n\n" +
                    "----------------------------\n\n" +
                    e.StackTrace + "\n\n";
                MessageBox.Show(errorMessage, "TeleBajaUEA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Não há uma real necessidade de se passar o args do main para essa função
        // fiz isso apenas para tentar simplificar o código no main, já que:
        //
        // 1. se estiver rodando direto pelo .exe ou visual studio, o args do main será usado
        // 2. se estiver rodando pelo application instalado, será usado o ActivationData
        // fonte: https://robindotnet.wordpress.com/2010/03/21/how-to-pass-arguments-to-an-offline-clickonce-application/
        private static string[] GetActivationData(string[] pArgs)
        {
            // TODO verificar se de fato são caminhos para arquivos e se eles existem...
            // se executado pelo VisualStudio ou diretamente o .exe, isso não é válido
            if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null &&
                    AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
            {
                string[] activationData = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;

                string[] args = new string[activationData.Length];
                for (int i = 0; i < activationData.Length; i++)
                {
                    Uri uri = new Uri(activationData[i]);
                    args[i] = uri.LocalPath.ToString();
                }
                return args;
            }
            else
                return pArgs;
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
