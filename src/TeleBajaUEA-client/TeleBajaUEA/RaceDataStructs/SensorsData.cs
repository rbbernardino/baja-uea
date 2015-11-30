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
        public uint Millis { get { return millis; } }

        private readonly float speed;
        private readonly float engineTemperature;
        private readonly float rpm;
        private readonly float fuel;
        // TODO: transformar 'H' e 'L' em enums?? talvez no SeiralMsg??
        private readonly char breakOn; // (H)igh - freiando, (L)ow - não freiando
        private uint millis;

        // Esse contrutor é usado nas antigas simulações. Como o tempo Millis
        // do arduino não é considerado aqui, os simuladores não vão mais funcionar
        //
        // possível solução: long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        public SensorsData(float pSpeed, float pEngineTemp, float pRPM, float pFuel, bool pBreakON)
        {
            millis = 0;
            speed = pSpeed;
            engineTemperature = pEngineTemp;
            rpm = pRPM;
            fuel = pFuel;

            if (pBreakON)
                breakOn = 'H';
            else
                breakOn = 'L';
        }

        public SensorsData(uint pMillis, float pSpeed, float pEngineTemp, float pRPM, float pFuel, char pBreakON)
        {
            millis = pMillis;
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
