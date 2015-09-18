using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA
{
    public struct SensorsData
    {
        public long TimeStamp { get { return timestamp; } }
        public float Speed { get { return speed; } }
        public float EngineTemperature { get { return engineTemperature; } }
        public bool BreakState { get { return breakOn; } }//BreakState_ToString(); } }

        private readonly long timestamp;
        private readonly float speed;
        private readonly float engineTemperature;
        private readonly bool breakOn;

        public SensorsData(long pTimestamp, float pSpeed, float pEngineTemp, bool pBreakON)
        {
            timestamp = pTimestamp;
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            breakOn = pBreakON;
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
