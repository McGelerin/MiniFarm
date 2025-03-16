using Runtime.Signals;
using Runtime.Signals.Currency;
using Zenject;

namespace Runtime.Installers.CrossScene
{
    public class CrossSceneSignalsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<LoadSceneSignal>();
            Container.DeclareSignal<ChangeLoadingScreenActivationSignal>();
            
            Container.DeclareSignal<ChangeCurrencyValueSignal>();
            Container.DeclareSignal<RefreshCurrencyVisualSignal>();
        }
    }
}