using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    // Essa classe encapsula o salvamento e recuperação dos dados de uma corrida
    // de/para um arquivo
    public sealed class RaceFile
    {
        // TODO campo para manter referência ao arquivo temporário
        // private File...

        public async static Task<bool> SaveToFile(string FilePath, RaceData data) {
            Stream stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();

            await Task.Run(() => { bFormatter.Serialize(stream, data); });
            stream.Close();
            
            // TODO serializa dados, abre arquivo e salva
            // TODO encriptografar e colocar senha (mesma senha de logar no programa)
            return true;
        }

        public async static Task<RaceData> LoadFromFile(string FilePath)
        {
            // TODO abre arquivo e deserializa
            await Task.Delay(500);
            return new RaceData();
        }

        // durante a gravação, a cada 5 minutos, um arquivo temporário é atualizado
        // ele contém os dados da corrida até o momento, logo, se ocorrer um falha
        // como o PC travando e reiniciando, esses dados poderão ser recuperados
        public async static Task<bool> CreateTempFile()
        {
            // TODO cria arquivo temporário
            await Task.Delay(500);
            return true;
        }

        public async static Task<bool> UpdateTempFile(RaceData data)
        {
            // TODO Atualiza arquivo temporário com novo conjunto de dados
            await Task.Delay(500);
            return true;
        }
    }
}
