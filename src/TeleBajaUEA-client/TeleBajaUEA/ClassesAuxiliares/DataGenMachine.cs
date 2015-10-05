using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA   
{
    public enum CarState
    {
        Stopped,         // ACC OFF, BRK ON/OFF - (VEL)ocidade (ZERO)
        Accelerating, // ACC ON,  BRK OFF    - (VEL)ocidade (AUM)entando
        Free,         // ACC OFF, BRK OFF    - (VEL)ocidade (DIM)inuindo
        Braking,      // ACC OFF, BRK ON     - (VEL)ocidade (DIM)inuindo
        MaxSpeed      // ACC ON,  BRK OFF    - (VEL)ocidade (MAX)axima
    }

    public enum Action
    {
        ACC_ON,        // ACC (OFF -> ON)
        ACC_OFF,       // ACC (ON  -> OFF)
        BRK_ON,        // BRK (OFF -> ON)
        BRK_OFF,       // BRK (ON  -> OFF)
        REACH_MAX_SPEED,  // SPD (... -> MAX_SPEED)
        REACH_ZERO_SPEED    // SPD (... -> ZERO)
    }

    // maquina de estados obtida aqui: http://stackoverflow.com/a/5924053
    // separei em dois arquivos, sendo um só para as transições
    public partial class DataGenMachine
    {
        private Dictionary<Transition, CarState> transitions;
        public CarState CurrentState { get; private set; }

        public DataGenMachine()
        {
            CurrentState = CarState.Stopped;
            transitions = new Dictionary<Transition, CarState>
            {
                {new Transition(CarState.Stopped, Action.ACC_ON), CarState.Accelerating},
                {new Transition(CarState.Stopped, Action.BRK_ON), CarState.Stopped},
                {new Transition(CarState.Stopped, Action.BRK_OFF), CarState.Stopped},

                {new Transition(CarState.Accelerating, Action.REACH_MAX_SPEED), CarState.MaxSpeed},
                {new Transition(CarState.Accelerating, Action.ACC_OFF), CarState.Free},

                {new Transition(CarState.MaxSpeed, Action.ACC_OFF), CarState.Free},

                {new Transition(CarState.Free, Action.BRK_ON), CarState.Braking},
                {new Transition(CarState.Free, Action.ACC_ON), CarState.Accelerating},
                {new Transition(CarState.Free, Action.REACH_ZERO_SPEED), CarState.Stopped},

                {new Transition(CarState.Braking, Action.BRK_OFF), CarState.Free},
                {new Transition(CarState.Braking, Action.REACH_ZERO_SPEED), CarState.Braking}
            };
        }

        private CarState GetNext(Action action)
        {
            Transition transition = new Transition(CurrentState, action);
            CarState nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception("Transicao inexistente: " + CurrentState + " -> " + action);
            return nextState;
        }

        public CarState MoveNext(Action action)
        {
            CurrentState = GetNext(action);
            UpdateGenerator(CurrentState);
            return CurrentState;
        }
    }
}
