﻿using System;
using System.Timers;

namespace TeleBajaUEA
{
    /// ---------------------------APENAS PARA TESTE-----------------------
    /// 
    /// Função de teste de recebimento de mensagem a cada 1 segundo
    /// 

    public sealed class RandomDataGenerator
    {
        private Timer timerGenerateData;

        private long dataCount;

        private float currentSpeed;
        private float currentTemperature;
        private float currentRPM;
        private float currentFuel;
        private bool currentBreakState;

        private float deltaSpeed;
        private float deltaTemp;
        private float deltaRPM;
        private float deltaFuel;

        private readonly int GENERATE_RATE = 100;
        private readonly long MAX_SPEED = 60;
        private readonly long MAX_TEMP = 300;
        private readonly long MAX_RPM = 3000;

        private Random rndGenerator;
        private int tmpRandomNum;
        private int tmpSignal;

        public void Start()
        {
            // valores iniciais
            dataCount = 0;
            currentSpeed = 30;
            currentTemperature = 80;
            currentRPM = 1000;
            currentFuel = 100;
            currentBreakState = false;
            //--------------------

            rndGenerator = new Random();

            timerGenerateData = new Timer();
            timerGenerateData.Interval = GENERATE_RATE;
            timerGenerateData.Elapsed += new ElapsedEventHandler(TickNewData);
            timerGenerateData.Enabled = true;
        }

        public void Stop()
        {
            timerGenerateData.Stop();
            timerGenerateData.Elapsed -= new ElapsedEventHandler(TickNewData);
            timerGenerateData.Enabled = false;
            timerGenerateData.Dispose();
            timerGenerateData = null;
        }

        private void TickNewData(object _source, ElapsedEventArgs _e)
        {
            GenerateNewData();
        }

        private void GenerateNewData()
        {
            deltaSpeed = RndDeltaSpeed();
            deltaTemp = RndDeltaTemp();
            deltaRPM = RndDeltaRPM();
            deltaFuel = -0.1f;

            currentSpeed       += deltaSpeed;
            currentTemperature += deltaTemp;
            currentRPM         += deltaRPM;
            currentFuel        += deltaFuel;

            if(RndBreakChange())
                currentBreakState = RndBreakState();

            dataCount++;

            if (currentSpeed > MAX_SPEED) currentSpeed = MAX_SPEED;
            if (currentSpeed <= 0) currentSpeed = 0.1f;

            if (currentTemperature > MAX_TEMP) currentTemperature = MAX_TEMP;
            if (currentTemperature <= 0) currentTemperature = 0.1f;

            if (currentRPM > MAX_RPM) currentRPM = MAX_RPM;
            if (currentRPM <= 0) currentRPM = 10;

            SensorsData newDataTemp = new SensorsData(dataCount, currentSpeed, currentTemperature, currentRPM, currentFuel, currentBreakState);

            CarConnection.Send(this, newDataTemp);
        }

        private bool RndBreakState()
        {
            // 20% de chance de freiar
            if (rndGenerator.Next(0, 10) < 2)
                return true;
            else
                return false;
        }

        // 20% de chance de mudar o estado do freio
        private bool RndBreakChange()
        {
            tmpRandomNum = rndGenerator.Next(0, 10);
            if (tmpRandomNum < 2)
                return true;
            else
                return false;
        }

        // Probabilidades:
        //   20% - delta = 400  //   40% - delta = 200 //   40% - delta = 100
        private float RndDeltaRPM()
        {
            tmpSignal = RndSignalDeltaRPM();

            tmpRandomNum = rndGenerator.Next(0, 10);

            if (tmpRandomNum < 2) // 0~1 = 20%
                return tmpSignal * 400;
            else if (tmpRandomNum < 6) // 2~5 = 40%
                return tmpSignal * 200;
            else // 6~9 = 40%
                return tmpSignal * 100;
        }

        // Probabilidades:
        //   10% Positivo  //   10% Negativo   //   80% Sem alteração
        private int RndSignalDeltaRPM()
        {
            tmpRandomNum = rndGenerator.Next(0, 10);
            if (tmpRandomNum < 1)
                return +1;
            else if (tmpRandomNum < 2)
                return -1;
            else
                return 0;
        }

        // Probabilidades:
        //   20% - delta = 8  //   40% - delta = 4 //   40% - delta = 2
        private float RndDeltaSpeed()
        {
            tmpSignal = RndSignalDeltaSpeed();

            tmpRandomNum = rndGenerator.Next(0, 10);

            if (tmpRandomNum < 2) // 0~1 = 20%
                return tmpSignal * 8;
            else if (tmpRandomNum < 6) // 2~5 = 40%
                return tmpSignal * 4;
            else // 6~9 = 40%
                return tmpSignal * 2;
        }

        // Probabilidades:
        //   30% Positivo  //   30% Negativo   //   40% Sem alteração
        private int RndSignalDeltaSpeed()
        {
            tmpRandomNum = rndGenerator.Next(0, 10);
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
            tmpSignal = RndSignalDeltaSpeed();

            tmpRandomNum = rndGenerator.Next(0, 10);

            if (tmpRandomNum < 2) // 0~1 = 20%
                return tmpSignal * 4;
            else if (tmpRandomNum < 6) // 2~5 = 40%
                return tmpSignal * 2;
            else // 6~9 = 40%
                return tmpSignal * 1;
        }

        // Probabilidades:
        //   10% Positivo   //   10% Negativo    //   80% Sem alteração
        private int RndSignalDeltaTemp()
        {
            tmpRandomNum = rndGenerator.Next(0, 10);
            if (tmpRandomNum < 1)
                return +1;
            else if (tmpRandomNum < 2)
                return -1;
            else
                return 0;
        }
    }
}