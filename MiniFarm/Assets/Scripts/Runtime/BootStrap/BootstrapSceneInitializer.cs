using Runtime.Identifiers;
using Runtime.Signals;
using Zenject;

namespace Runtime.BootStrap
{
    public class BootstrapSceneInitializer : IInitializable
    {
        [Inject] private SignalBus _signalBus;
        
        public void Initialize()
        {
            _signalBus.Fire(new LoadSceneSignal(SceneID.GameScene));
        }
    }
}