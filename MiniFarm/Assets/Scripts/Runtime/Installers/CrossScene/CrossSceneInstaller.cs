using Runtime.CrossScene.SceneManagement;
using Zenject;

namespace Runtime.Installers.CrossScene
{
    public class CrossSceneInstaller : MonoInstaller<CrossSceneInstaller>
    {
        public override void InstallBindings()
        {
            BindSceneLoading();
        }
        
        private void BindSceneLoading()
        {
            Container.BindInterfacesAndSelfTo<SceneLoadingService>().AsSingle();
        }
    }
}