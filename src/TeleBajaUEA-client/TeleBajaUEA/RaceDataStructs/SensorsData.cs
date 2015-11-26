using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA.RaceDataStructs
{
    public struct SensorsData
    {
        public float Speed { get { return speed; } }
        public float EngineTemperature { get { return engineTemperature; } }
        public float RPM { get { return rpm; } }
        public float Fuel { get { return fuel; } }
        public bool BreakState { get { return BreakState_ToBool(breakOn); } }
        //public long DataCount { get { return dataCount; } }

        private readonly float speed;
        private readonly float engineTemperature;
        private readonly float rpm;
        private readonly float fuel;
        // TODO: transformar 'H' e 'L' em enums?? talvez no SeiralMsg??
        private readonly char breakOn; // (H)igh - freiando, (L)ow - não freiando
        //private readonly long dataCount;

        public SensorsData(float pSpeed, float pEngineTemp, float pRPM, float pFuel, bool pBreakON)
        {
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            rpm = pRPM;
            fuel = pFuel;

            if (pBreakON)
                breakOn = 'H';
            else
                breakOn = 'L';
        }

        public SensorsData(float pSpeed, float pEngineTemp, float pRPM, float pFuel, char pBreakON)
        {
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            rpm = pRPM;
            fuel = pFuel;
            breakOn = pBreakON;
            // TODO remover definitivamente dataCount = pDataCount; ??
        }

        public bool BreakState_ToBool(char pBreakState)
        {
            if (pBreakState == 'H')
                return true;
            else // TODO Throw(estado de freio não reconhecido)
                return false;
        }
    }
}
