using Runtime.Input.Enums;

namespace Runtime.Input.Signals
{
    public readonly struct ChangeInputStateSignal
    {
        private readonly InputState _inputState;

        public InputState State => _inputState;
        
        public ChangeInputStateSignal(InputState inputState)
        {
            _inputState = inputState;
        }

    }
}