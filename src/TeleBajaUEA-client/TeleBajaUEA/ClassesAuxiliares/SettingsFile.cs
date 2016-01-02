using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TeleBajaUEA.ClassesAuxiliares
{
    public static class SettingsFile
    {
        public static bool SettingsFileExists { get { return File.Exists(SETTINGS_PATH); } }

        private readonly static string SETTINGS_FILE_NAME = "config.xml";
        private static string SETTINGS_PATH = APP_FILES_PATH + SETTINGS_FILE_NAME;
        public static string APP_FILES_PATH
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"TeleBajaUEA\");
            }
        }

        public static void SaveToFile(ProgramSettings pSettings)
        {
            XmlSerializer serializer = new XmlSerializer(pSettings.GetType());
            StreamWriter writer = new StreamWriter(SETTINGS_PATH);
            serializer.Serialize(writer, pSettings);
            writer.Close();
        }

        // TODO exibir mensagem de erro na criação/leitura do arq de configurações
        //
        // carrega settings do arquivo padrão
        // lembrando que Program.Settings precisa ter um inicializar padrão
        public static ProgramSettings LoadFromFile()
        {
            ProgramSettings settings = new ProgramSettings();
            XmlSerializer serializer = new XmlSerializer(settings.GetType());
            StreamReader reader = new StreamReader(SETTINGS_PATH);
            settings = (ProgramSettings)serializer.Deserialize(reader);
            reader.Close();
            return settings;
        }

        public static void CreateAppFilesFolder()
        {
            Directory.CreateDirectory(APP_FILES_PATH);
        }
    }
}
