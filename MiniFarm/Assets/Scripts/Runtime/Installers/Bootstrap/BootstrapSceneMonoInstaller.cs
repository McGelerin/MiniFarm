using Runtime.BootStrap;
using Zenject;

namespace Runtime.Installers.Bootstrap
{
    public class BootstrapSceneMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapSceneInitializer>().AsSingle();
        }
    }
}