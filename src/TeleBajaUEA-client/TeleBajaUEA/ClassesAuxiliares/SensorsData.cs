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
        public float RPM { get { return rpm; } }
        public bool BreakState { get { return breakOn; } }// TODO BreakState_ToString(); } }
        public long DataCount { get { return dataCount; } }

        private readonly float speed;
        private readonly float engineTemperature;
        private readonly float rpm;
        private readonly bool breakOn;
        private readonly long dataCount;

        public SensorsData(long pDataCount, float pSpeed, float pEngineTemp, float pRPM, bool pBreakON)
        {
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            rpm = pRPM;
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
