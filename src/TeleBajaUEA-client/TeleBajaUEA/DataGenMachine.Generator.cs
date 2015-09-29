using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TeleBajaUEA
{
    public partial class DataGenMachine
    {
        private Timer timerUpdateData;

        private long dataCount;

        private float currentSpeed;
        private float currentTemperature;
        private float currentRPM;
        private bool currentBreakState;

        private float deltaSpeed;
        private float deltaTemp;
        private float deltaRPM;

        private readonly float UPDATE_RATE = 100;

        private readonly long MAX_SPEED = 60;
        private readonly long MAX_TEMP = 180;
        private readonly long MAX_RPM = 3000;

        public async void Start()
        {
            // valores iniciais
            dataCount = 0;
            currentSpeed = 30;
            currentTemperature = 80;
            currentRPM = 1000;
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

            dataCount++;

            if(currentSpeed >= MAX_SPEED && CurrentState != CarState.MaxSpeed)
            {
                currentSpeed = MAX_SPEED;
                MoveNext(Action.REACH_MAX_SPEED);
            }

            SensorsData newDataTemp = new SensorsData(dataCount, currentSpeed, currentTemperature, currentRPM, currentBreakState);

            CarConnection.Send(this, newDataTemp);
        }

        private void UpdateGenerator(CarState state)
        {
            switch (state)
            {
                // aceleracao de 10km/h a cada 1.5 segundos
                // 1.5 s                --- 10km/h 
                // UPDATE_RATE / 1000ms --- x
                case CarState.Accelerating:
                    deltaSpeed = +((10f * UPDATE_RATE/1000f) / 1.5f);
                    break;
                
                case CarState.MaxSpeed:
                    deltaSpeed = 0;
                    break;
                
                // desaceleracao LIVRE de 5km/h a cada 1.5 segundos
                case CarState.Free:
                    currentBreakState = false;
                    deltaSpeed = -((5f * UPDATE_RATE/1000f) / 1.5f);
                    break;

                // desaceleracao FREIANDO de 15km/h a cada 1.5 segundos
                case CarState.Braking:
                    deltaSpeed = -((15f * UPDATE_RATE / 1000f) / 1.5f);
                    currentBreakState = true;
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

            // permanece acelerando por 8 segundos
            await Task.Delay(8000);

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

            // volta a acelerar
            MoveNext(Action.ACC_ON);

            // TODO continuar...
        }
    }
}
