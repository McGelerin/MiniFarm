using Runtime.Signals;
using Zenject;

namespace Runtime.Installers.CrossScene
{
    public class CrossSceneSignalsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<LoadSceneSignal>();
            Container.DeclareSignal<ChangeLoadingScreenActivationSignal>();
        }
    }
}