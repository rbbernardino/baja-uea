using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBajaUEA
{
    public partial class DataGenMachine
    {
        class Transition
        {
            readonly CarState CurrentState;
            readonly Action Action;

            public Transition(CarState pCurrentState, Action pAction)
            {
                CurrentState = pCurrentState;
                Action = pAction;
            }

            public override int GetHashCode()
            {
                // TODO verificar se 17 e 31 podiam ser quaisquer outros valores...
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Action.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                Transition other = obj as Transition;
                return other != null &&
                    this.CurrentState == other.CurrentState &&
                    this.Action == other.Action;
            }
        }
    }
}
