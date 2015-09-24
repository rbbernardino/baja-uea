using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA
{
    public struct SensorsData
    {
        public float Speed { get { return speed; } }
        public float EngineTemperature { get { return engineTemperature; } }
        public bool BreakState { get { return breakOn; } }//BreakState_ToString(); } }
        public long DataCount { get { return dataCount; } }

        private readonly float speed;
        private readonly float engineTemperature;
        private readonly bool breakOn;
        private readonly long dataCount;

        public SensorsData(long pDataCount, float pSpeed, float pEngineTemp, bool pBreakON)
        {
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            breakOn = pBreakON;
            dataCount = pDataCount;
        }
        /*
        public string BreakState_ToString()
        {
            if (breakOn)
                return "ON";
            else
                return "OFF";
        }
        */
    }
}
