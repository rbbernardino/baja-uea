using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.RaceDataStructs
{
    [Serializable()]
    public struct FileSensorsData
    {
        public uint xValue;
        public float speed;
        public float rpm;
        public bool breakState;
    }
}
