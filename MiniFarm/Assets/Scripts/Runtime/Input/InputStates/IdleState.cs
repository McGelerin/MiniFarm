using System.Collections.Generic;
using Runtime.Input.Enums;
using Runtime.Input.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static UnityEngine.Input;

namespace Runtime.Input.InputStates
{
    public class IdleState : IInputState
    {
        private readonly SignalBus _signalBus;

        private IdleState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Enter()
        {
        }

        public void Tick()
        {
            if (UserTouchedGameplayArea())
            {
                TransitionToClickState();
            }
        }
        
        private bool UserTouchedGameplayArea()
        {
            if (touchCount > 0 )
            {
                if (!IsPointerOverUIObject())
                    return true;
            }

            return false;
        }
        
        private bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position =  new Vector2(mousePosition.x, mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
        
        private void TransitionToClickState()
        {
            ChangeInputStateSignal changeInputStateSignal = new (InputState.Click);
            _signalBus.Fire(changeInputStateSignal);
        }
        
        public void Exit()
        {
        }
    }
}