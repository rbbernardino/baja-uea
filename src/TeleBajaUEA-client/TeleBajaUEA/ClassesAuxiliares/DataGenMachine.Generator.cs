using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class DataGenMachine
    {
        private Timer timerUpdateData;

        //private long dataCount;

        private float currentSpeed;
        private float currentTemperature;
        private float currentRPM;
        private float currentFuel;
        private bool currentBreakState;

        private float deltaSpeed;
        private float deltaTemp;
        private float deltaRPM;
        private float deltaFuel;

        private readonly float UPDATE_RATE = 50;

        private readonly long MAX_SPEED = 60;
        private readonly long MAX_TEMP = 300;
        private readonly long MAX_RPM = 2800;

        private readonly long MIN_RPM = 1000;

        public async void Start()
        {
            // valores iniciais
            //dataCount = 0;
            currentSpeed = 0;
            currentTemperature = 80;
            currentRPM = MIN_RPM;
            currentFuel = 100;
            currentBreakState = false;
            //--------------------

            timerUpdateData = new Timer();
            timerUpdateData.Interval = UPDATE_RATE;
            timerUpdateData.Elapsed += new ElapsedEventHandler(TickUpdateData);
            timerUpdateData.Enabled = true;  // TODO ou não precisa disso, apenas o Start()?
            //timerUpdateData.Start();

            await SimulateRace();
        }

        public void Stop()
        {
            timerUpdateData.Stop();
            timerUpdateData.Elapsed -= new ElapsedEventHandler(TickUpdateData);
            timerUpdateData.Enabled = false; // TODO apenas o Stop() não serve?
            timerUpdateData.Dispose();
            timerUpdateData = null;
        }

        private void TickUpdateData(object _source, ElapsedEventArgs _e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            currentSpeed += deltaSpeed;
            currentTemperature += deltaTemp;
            currentRPM += deltaRPM;
            currentFuel += -0.1f;

            //dataCount++;

            if(currentSpeed >= MAX_SPEED && CurrentState != CarState.MaxSpeed)
            {
                currentSpeed = MAX_SPEED;
                MoveNext(Action.REACH_MAX_SPEED);
            }

            if (currentRPM >= MAX_RPM) currentRPM = MAX_RPM;
            if (currentRPM <= MIN_RPM) currentRPM = MIN_RPM;

            if (currentFuel <= 0) currentFuel = 0;

            if(currentSpeed <= 0 && CurrentState == CarState.Free)
            {
                currentSpeed = 0;
                MoveNext(Action.REACH_ZERO_SPEED);
            }

            SensorsData newDataTemp =
                new SensorsData(currentSpeed, currentTemperature,
                                    currentRPM, currentFuel, currentBreakState);

            CarConnection.Send(this, newDataTemp);
        }

        private void UpdateGenerator(CarState state)
        {
            switch (state)
            {
                case CarState.Stopped:
                    deltaSpeed = deltaRPM = 0;
                    break;

                // aceleracao de 12km/h a cada 1.5 segundos
                // RPM aumenta 300 a cada 1.5s
                // 1.5 s                --- 10km/h 
                // UPDATE_RATE / 1000ms --- x
                case CarState.Accelerating:
                    deltaSpeed = +((12f * UPDATE_RATE/1000f) / 1.5f);
                    deltaRPM = +((1100f * UPDATE_RATE/1000f) / 1.5f);
                    break;
                
                case CarState.MaxSpeed:
                    deltaSpeed = 0;
                    deltaRPM = 0;
                    break;
                
                // desaceleracao LIVRE de 3km/h a cada 1.5 segundos
                // RPM diminue 50 a cada 1.5s
                case CarState.Free:
                    currentBreakState = false;
                    if (currentSpeed > 0)
                    {
                        deltaSpeed = -((3f * UPDATE_RATE / 1000f) / 1.5f);
                        deltaRPM = -((300f * UPDATE_RATE / 1000f) / 1.5f);
                    }
                    else
                        deltaSpeed = deltaRPM = 0;
                    break;

                // desaceleracao FREIANDO de 17km/h a cada 1.5 segundos
                // RPM diminue 450 a cada 1.5s
                case CarState.Braking:
                    currentBreakState = true;
                    if (currentSpeed > 0)
                    {
                        deltaSpeed = -((15f * UPDATE_RATE / 1000f) / 1.5f);
                        deltaRPM = -((1300f * UPDATE_RATE / 1000f) / 1.5f);
                    }
                    else
                        deltaSpeed = deltaRPM = 0;
                    break;
            }
        }

        private async Task SimulateRace()
        {
            // inicia parado (no construtor) e espera largada (2s)
            await Task.Delay(2000);

            // comeca a acelerar e permanece até automaticamente trocar para
            // estado de velocidade maxima
            MoveNext(Action.ACC_ON);

            // inicia aquecimento do motor
            deltaTemp = 1;

            // permanece acelerando por 8 segundos
            await Task.Delay(8000);

            // mantém temperatura do motor
            deltaTemp = 0;

            // freia rapidamente
            MoveNext(Action.ACC_OFF);
            await Task.Delay(300);
            MoveNext(Action.BRK_ON);
            await Task.Delay(500);
            MoveNext(Action.BRK_OFF);

            // anda "solto" por 0.5s e acelera por 1.5s
            await Task.Delay(500);
            MoveNext(Action.ACC_ON);
            await Task.Delay(1500);

            // anda "solto" por 3s
            MoveNext(Action.ACC_OFF);
            await Task.Delay(3000);

            // volta a acelerar, mantém por 4s e freia por 4s (até parar?)
            MoveNext(Action.ACC_ON);
            await Task.Delay(4000);
            MoveNext(Action.ACC_OFF);
            MoveNext(Action.BRK_ON);
            await Task.Delay(4000);

            // solta freio e espera 5s até acelerar novamente
            MoveNext(Action.BRK_OFF);
            await Task.Delay(5000);
            MoveNext(Action.ACC_ON);

            // TODO continuar...
        }
    }
}
