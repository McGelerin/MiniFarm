using System;
using Runtime.Input.Enums;
using Runtime.Input.Raycasting;
using Runtime.Input.Signals;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Runtime.Input.InputStates
{
    public class ClickState : IInputState
    {
        private readonly IInputModel _inputModel;
        private readonly SignalBus _signalBus;
        private readonly IClickRaycaster _clickRaycaster;
        
        
        private Vector3 _touchPosition;
        private Touch _touch;
        
        private IClickable _clickable;
        
        private ClickState(SignalBus signalBus, IClickRaycaster clickRaycaster, IInputModel inputModel)
        {
            _signalBus = signalBus;
            _clickRaycaster = clickRaycaster;
            _inputModel = inputModel;
        }
        
        public void Enter()
        {
            Debug.Log("ClickState");

            
            if (UnityEngine.Input.touchCount == 0)
            {
                throw new Exception("No touch input detected when entering Click Input State!");
            }
            
            _touch = UnityEngine.Input.GetTouch(0);
            _touchPosition = _touch.position;
            _clickable = _clickRaycaster.RaycastTouchPosition(_touchPosition);
            
            if (UnityEngine.Input.touchCount == 1)
            {
                _touch = UnityEngine.Input.GetTouch(0);
                _touchPosition = _touch.position;
                
                if (_touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(_touch.fingerId))
                {
                    SwitchToState(InputState.BeforeIdle);
                }
                else if (_clickable != null)
                {
                    if (_inputModel.Clickable == null)
                    {
                        _inputModel.SetClickable(_clickable);
                        _clickable.OnClicked();
                    }
                    else if (_inputModel.Clickable == _clickable)
                    {
                        _clickable.OnClicked();
                    }
                    else
                    {
                        _signalBus.Fire(new AnotherAreaClickSignal());
                        _inputModel.SetClickable(_clickable);
                        _clickable.OnClicked();
                    }
                }
                else
                {
                    _inputModel.SetClickable(null);
                    _signalBus.Fire(new AnotherAreaClickSignal());
                }
            }

            SwitchToState(InputState.BeforeIdle);
        }

        public void Tick()
        {
        }
        
        private void SwitchToState(InputState targetState)
        {
            ChangeInputStateSignal changeInputStateSignal = new (targetState);
            _signalBus.Fire(changeInputStateSignal);
        }

        public void Exit()
        {
        }
    }
}