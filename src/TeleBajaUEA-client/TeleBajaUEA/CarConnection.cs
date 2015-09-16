using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA
{
    // Essa classe é um Singleton para manter a conexão com o Carro
    // Ela também encapsula a tradução entre Formato bytes XBee ---> Objeto do C#
    class CarConnection
    {
        // private static USBConnection USBConnection;

        // public static USBConnection GetInstance(){ return USBConnection }

        public async static Task<bool> ConnectToCar()
        {
            await Task.Delay(500);
            return true;
        }
    }
}
