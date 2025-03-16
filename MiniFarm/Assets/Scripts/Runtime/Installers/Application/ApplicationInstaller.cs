using Runtime.Installers.CrossScene;
using Zenject;

namespace Runtime.Installers.Application
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<CrossSceneSignalsInstaller>();
            Container.Install<CrossScenePanelsInstaller>();
        }
    }
}