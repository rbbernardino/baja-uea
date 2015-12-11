using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.ClassesAuxiliares
{
    public static class ProgramSettings
    {
        public static string PortXBee { get; private set; }

        // indica se deve manter backup de todas as corridas, mesmo as não salvas
        // Se "false" manterá backup de apenas a última corrida, na pasta do programa
        // Se "true" manterá backup de todas as corridas na pasta "backup"
        public static bool KeepBackup { get; private set; } = true; // TODO permitir usuário escolher

        // TODO salvar em .xml
        public static void SaveXBeePort(string pPortXBee)
        {
            PortXBee = pPortXBee;
        }

        // TODO carrega settings do .xml ou cria novo se ainda não existir
        // TODO exibir mensagem de erro na criação/leitura do arq de configurações
        // carrega settings do arquivo padrão
        public static void LoadFromFile()
        {
            PortXBee = "COM15";
        }
    }
}
