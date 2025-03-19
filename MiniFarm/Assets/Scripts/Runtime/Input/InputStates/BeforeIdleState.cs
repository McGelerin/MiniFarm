using Runtime.Input.Enums;
using Runtime.Input.Signals;
using UnityEngine;
using Zenject;
using static UnityEngine.Input;

namespace Runtime.Input.InputStates
{
    public class BeforeIdleState : IInputState
    {
        [Inject]private SignalBus _signalBus;
        
        public void Enter()
        {
        }

        public void Tick()
        {
            if (UserNotTouchedGameplayArea())
            {
                SwitchToState();
            }
        }
        
        private bool UserNotTouchedGameplayArea()
        {
            if (touchCount == 0 )
            {
                return true;
            }

            return false;
        }
        
        private void SwitchToState()
        {
            ChangeInputStateSignal changeInputStateSignal = new (InputState.Idle);
            _signalBus.Fire(changeInputStateSignal);
        }
        
        public void Exit()
        {
        }
    }
}