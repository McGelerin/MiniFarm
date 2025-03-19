using UnityEngine;

namespace Runtime.Input.InputStates
{
    public class InactiveState : IInputState
    {
        public void Enter()
        {
        }

        public void Tick()
        {
            return;
        }

        public void Exit()
        {
        }
    }
}