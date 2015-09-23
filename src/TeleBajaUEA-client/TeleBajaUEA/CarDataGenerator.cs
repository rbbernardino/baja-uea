using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TeleBajaUEA
{
    /// ---------------------------APENAS PARA TESTE-----------------------
    /// 
    /// Função de teste de recebimento de mensagem a cada 1 segundo
    /// 

    public sealed class CarDataGenerator
    {
        private Timer timerGenerateData;

        private int currentTime;

        private float currentSpeed;
        private float currentTemperature;
        private bool currentBreakState;

        private float deltaSpeed;
        private float deltaTemp;

        private readonly int GENERATE_RATE = 100;
        private readonly long MAX_SPEED = 60;

        private Random rdnGenerator;
        private int tmpRandomNum;
        private int tmpSignal;

        public void StartGenerateData()
        {
            // valores iniciais
            currentTime = 0;
            currentSpeed = 30;
            currentTemperature = 80;
            currentBreakState = false;
            //--------------------

            rdnGenerator = new Random();

            timerGenerateData = new Timer();
            timerGenerateData.Interval = GENERATE_RATE;
            timerGenerateData.Elapsed += new ElapsedEventHandler(TickNewData);
            timerGenerateData.Enabled = true;
        }

        private void TickNewData(object _source, ElapsedEventArgs _e)
        {
            GenerateNewData();
        }

        private void GenerateNewData()
        {
            deltaSpeed = RndDeltaSpeed();
            deltaTemp = RndDeltaTemp();

            currentSpeed       += deltaSpeed;
            currentTemperature += deltaTemp;
            currentBreakState = RndBreakState(); 
            currentTime++;

            if (currentSpeed > MAX_SPEED) currentSpeed = MAX_SPEED;
            if (currentSpeed < 0) currentSpeed = 0;
            if (currentTemperature < 0) currentTemperature = 0;

            SensorsData newDataTemp = new SensorsData(currentTime, currentSpeed, currentTemperature, currentBreakState);

            CarConnection.Send(this, newDataTemp);
        }

        private bool RndBreakState()
        {
            // 30% de chance de freiar
            if (rdnGenerator.Next(0, 10) < 4)
                return true;
            else
                return false;
        }

        // Probabilidades:
        //   20% - delta = 8  //   40% - delta = 4 //   40% - delta = 2
        private float RndDeltaSpeed()
        {
            tmpSignal = RdnSignalDeltaSpeed();

            tmpRandomNum = rdnGenerator.Next(0, 10);

            if (tmpRandomNum < 2) // 0~1 = 20%
                return tmpSignal * 8;
            else if (tmpRandomNum < 6) // 2~5 = 40%
                return tmpSignal * 4;
            else // 6~9 = 40%
                return tmpSignal * 2;
        }

        // Probabilidades:
        //   30% Positivo  //   30% Negativo   //   40% Sem alteração
        private int RdnSignalDeltaSpeed()
        {
            tmpRandomNum = rdnGenerator.Next(0, 10);
            if (tmpRandomNum < 3)
                return +1;
            else if (tmpRandomNum < 6)
                return -1;
            else
                return 0;
        }

        // Probabilidades:
        //   20% - delta = 8   //   40% - delta = 4   //   40% - delta = 2
        private float RndDeltaTemp()
        {
            tmpSignal = RdnSignalDeltaSpeed();

            tmpRandomNum = rdnGenerator.Next(0, 10);

            if (tmpRandomNum < 2) // 0~1 = 20%
                return tmpSignal * 4;
            else if (tmpRandomNum < 6) // 2~5 = 40%
                return tmpSignal * 2;
            else // 6~9 = 40%
                return tmpSignal * 1;
        }

        // Probabilidades:
        //   10% Positivo   //   10% Negativo    //   80% Sem alteração
        private int RdnSignalDeltaTemp()
        {
            tmpRandomNum = rdnGenerator.Next(0, 10);
            if (tmpRandomNum < 1)
                return +1;
            else if (tmpRandomNum < 2)
                return -1;
            else
                return 0;
        }
    }
}
