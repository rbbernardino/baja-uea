using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA
{
    // Essa classe é um Singleton para manter a conexão com o Banco de Dados
    // Ela também encapsula a tradução entre Objeto do C# ---> Linhas no BD
    // Vamos usar PostgreSQL
    public sealed class DBConnection
    {
        // private static NpgsqlConnection DBConnection;

        // public static NpgsqlConnection GetInstance() { return DBConnection; }
        
        public async static Task<bool> ConnectToDB() {
            await Task.Delay(500);
            return true;
        }
    }
}
