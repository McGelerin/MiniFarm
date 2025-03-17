using Runtime.Input;
using Runtime.Input.Enums;
using Runtime.Input.InputStates;
using Runtime.Input.Raycasting;
using Runtime.Input.Signals;
using Zenject;

namespace Runtime.Installers.Input
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickRaycaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputStateMachine>().AsSingle().NonLazy();
            
            BindInputStates();
            
            BindSignals();
        }
        private void BindInputStates()
        {
            Container.Bind<IInputState>()
                     .WithId(InputState.Inactive)
                     .To<InactiveState>()
                     .AsSingle();
            
            Container.Bind<IInputState>()
                .WithId(InputState.BeforeIdle)
                .To<BeforeIdleState>()
                .AsSingle();
            
            Container.Bind<IInputState>()
                     .WithId(InputState.Idle)
                     .To<IdleState>()
                     .AsSingle();
            
            Container.Bind<IInputState>()
                     .WithId(InputState.Click)
                     .To<ClickState>()
                     .AsSingle();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<ChangeInputStateSignal>();
        }
    }
}
