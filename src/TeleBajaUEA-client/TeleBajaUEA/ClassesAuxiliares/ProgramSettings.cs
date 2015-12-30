using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.ClassesAuxiliares
{
    public class ProgramSettings
    {
        public string PortXBee { get; set; } = "NULL";

        // indica se deve manter backup de todas as corridas, mesmo as não salvas
        // Se "false" manterá backup de apenas a última corrida, na pasta do programa
        // Se "true" manterá backup de todas as corridas na pasta "backup"
        public bool KeepBackup { get; set; } = true; // TODO permitir usuário escolher
    }
}
