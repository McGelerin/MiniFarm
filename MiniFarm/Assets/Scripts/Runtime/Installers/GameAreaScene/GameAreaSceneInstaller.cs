using Runtime.Creation;
using Runtime.Currency;
using Runtime.GameArea;
using Zenject;

namespace Runtime.Installers.GameAreaScene
{
    public class GameAreaSceneInstaller : MonoInstaller<GameAreaSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Install<ViewMediatorInstallerNotTick<CurrencyView,CurrencyMediator>>();
            
            Container.BindInterfacesAndSelfTo<GameAreaInitializer>().AsSingle();
        }
    }
}