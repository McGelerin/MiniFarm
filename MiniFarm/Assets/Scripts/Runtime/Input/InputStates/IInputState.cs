namespace Runtime.Input.InputStates
{
    public interface IInputState
    {
        public void Enter();
        public void Tick();
        public void Exit();
    }
}