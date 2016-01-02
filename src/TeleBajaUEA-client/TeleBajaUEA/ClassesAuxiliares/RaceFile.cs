using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // Essa classe encapsula o salvamento e recuperação dos dados de uma corrida
    // de/para um arquivo
    public sealed class RaceFile
    {
        public static string DEFAULT_BACKUP_PATH
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    @"TeleBajaUEA\backup\");
            }
        }

        private static string TEMP_FILE_PREFIX = "__backup";
        private static string TEMP_FILE_EXTENSION = ".btbu";
        private static string TEMP_FILE_PATH { get { return SettingsFile.APP_FILES_PATH + tempFileName; } }

        private static string BackupPath { get { return Program.Settings.BackupPath; } }
        private static string tempFileName;

        // TODO campo para manter referência ao arquivo temporário
        private static Stream backupFile;

        public async static Task<bool> SaveToFile(string FilePath, RaceData data) {
            Stream stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();

            await Task.Run(() => { bFormatter.Serialize(stream, data); });
            stream.Close();
            
            // TODO serializa dados, abre arquivo e salva
            // TODO encriptografar e colocar senha (mesma senha de logar no programa)
            return true;
        }

        public static RaceData LoadFromFile(string FilePath)
        {
            // TODO abre arquivo e deserializa
            Stream stream = File.Open(FilePath, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            RaceData data = (RaceData)bFormatter.Deserialize(stream);
            stream.Close();

            return data;
        }

        // durante a gravação, a cada
        // GravarCorrida.ChartSettings.UPDATE_BACKUP_RATE milisegundos,
        // um arquivo temporário é atualizado
        // ele contém os dados da corrida até o momento, logo, se ocorrer um falha
        // como o PC travando e reiniciando, esses dados poderão ser recuperados
        public async static Task<bool> CreateTempFile()
        {
            return await Task.Run(() =>
            {
                CleanBackupFiles();
                var culture = new CultureInfo("pt-BR");
                tempFileName =
                    TEMP_FILE_PREFIX +
                    DateTime.Now.ToString("yyyyMMddHHmmss") +
                    TEMP_FILE_EXTENSION;
                backupFile = File.Open(TEMP_FILE_PATH, FileMode.Create);
                backupFile.Close();
                return true;
            });
        }

        // Apaga todos os arquivos de backup na pasta do programa
        // a ideia é apagar o backup da corrida anterior 
        //----------------------------------------------
        // Fonte: http://stackoverflow.com/a/8132800
        // "Note that I first try to set attributes to "normal",
        //      because File.Delete() fails if file is read-only...
        // Note the use of GetFiles(): see this link
        // (http://msdn.microsoft.com/it-it/library/8he88b63.aspx) for details."
        private static void CleanBackupFiles()
        {
            DirectoryInfo di = new DirectoryInfo(SettingsFile.APP_FILES_PATH);
            FileInfo[] files = di.GetFiles("*" + TEMP_FILE_EXTENSION)
                                 .Where(p => p.Extension == TEMP_FILE_EXTENSION).ToArray();
            foreach (FileInfo file in files)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
        }

        public static void UpdateTempFile(RaceData data)
        {
            backupFile = File.Open(TEMP_FILE_PATH, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(backupFile, data);
            backupFile.Close();
        }

        public static void SaveToBackupDir()
        {
            Directory.CreateDirectory(BackupPath);
            File.Move(TEMP_FILE_PATH, BackupPath + tempFileName);
        }
    }
}
