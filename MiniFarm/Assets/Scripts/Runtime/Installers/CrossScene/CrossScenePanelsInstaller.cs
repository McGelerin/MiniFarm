using Runtime.CrossScene.LoadingScreen;
using Zenject;

namespace Runtime.Installers.CrossScene
{
    public class CrossScenePanelsInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingScreenView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LoadingScreenMediator>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenController>().AsSingle();
        }
    }
}