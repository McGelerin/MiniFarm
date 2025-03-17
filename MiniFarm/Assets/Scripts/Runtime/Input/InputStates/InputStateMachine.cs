using System;
using System.Collections.Generic;
using Runtime.Identifiers.Template;
using Runtime.Input.Enums;
using Runtime.Input.Signals;
using UniRx;
using Zenject;

namespace Runtime.Input.InputStates
{
    public class InputStateMachine : SignalListener, ITickable
    {
        private readonly IInputState _inactiveState;
        private readonly IInputState _idleState;
        private readonly IInputState _beforeIdleState;
        private readonly IInputState _clickState;

        [Inject] private IInputModel inputModel;

        [Inject]
        public InputStateMachine(InactiveState inactiveState, IdleState idleState, BeforeIdleState beforeIdleState, 
            ClickState clickState)
        {
            _inactiveState = inactiveState;
            _idleState = idleState;
            _beforeIdleState = beforeIdleState;
            _clickState = clickState;
        }

        private IInputState _currentState;
        private Dictionary<InputState, IInputState> _statesLookup;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _currentState = _inactiveState;
            _currentState.Enter();
            
            _statesLookup = new Dictionary<InputState, IInputState>
                            {
                                {InputState.Inactive, _inactiveState },
                                {InputState.BeforeIdle, _beforeIdleState },
                                {InputState.Idle, _idleState },
                                {InputState.Click, _clickState }
                            };
        }

        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<ChangeInputStateSignal>()
                .Subscribe(OnChangeInputStateSignal)
                .AddTo(_disposables);
        }
        
        private void OnChangeInputStateSignal(ChangeInputStateSignal signal)
        {
            InputState state = signal.State;
            StateControl(state);
        }

        private void StateControl(InputState state)
        {
            if (_statesLookup.TryGetValue(state, out IInputState targetState))
            {
                ChangeState(targetState);
            }
            else
            {
                throw new Exception($"State with ID {state} does not have an implementation!");
            }
        }

        private void ChangeState(IInputState targetState)
        {
            _currentState.Exit();
            targetState.Enter();
            
            _currentState = targetState;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            inputModel.ClearClickable();
        }

        public void Tick()
        {
            _currentState.Tick();
        }
    }
}