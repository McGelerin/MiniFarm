using System;
using System.Collections.Generic;
using Runtime.Identifiers.Template;
using Runtime.Input.Enums;
using Runtime.Input.Signals;
using Runtime.Signals;
using UniRx;
using Zenject;

namespace Runtime.Input.InputStates
{
    public class InputStateMachine : SignalListener, ITickable
    {
        [Inject(Id = InputState.Inactive) ] private readonly IInputState _inactiveState;
        [Inject(Id = InputState.Idle) ] private readonly IInputState _idleState;
        [Inject(Id = InputState.BeforeIdle) ] private readonly IInputState _beforeIdleState;
        [Inject(Id = InputState.Click) ] private readonly IInputState _clickState;
        
        [Inject] private IInputModel inputModel;
        

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

            _signalBus.GetStream<ChangeLoadingScreenActivationSignal>()
                .Where(x => !x.IsActive)
                .Subscribe(CloseLoadingScreen)
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
            
            _currentState = targetState;
            
            _currentState.Enter();
        }

        private void CloseLoadingScreen(ChangeLoadingScreenActivationSignal signal)
        {
            StateControl(InputState.Idle);
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