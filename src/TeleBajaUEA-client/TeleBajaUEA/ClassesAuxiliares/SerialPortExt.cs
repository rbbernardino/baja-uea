using System;
using System.IO.Ports;

namespace TeleBajaUEA.ClassesAuxiliares
{
    class SerialPortExt : SerialPort
    {
        private byte[] intBuffer = new byte[2];

        public int ReadInt16()
        {
            intBuffer[0] = (byte)ReadByte(); // ler o lowByte, a direita
            intBuffer[1] = (byte)ReadByte(); // ler o highByte, a esquerda
            return BitConverter.ToInt16(intBuffer, 0);
        }

        // o ReadChar padrão retorna um int, logo essa função encapsula o cast
        public char ReadCharCasted()
        {
            return (char)base.ReadChar();
        }

        public int ReadInt8()
        {
            return base.ReadChar();
        }

        public void WriteChar(char c)
        {
            Write(c.ToString());
        }
    }
}
