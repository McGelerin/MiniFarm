using Runtime.Signals;
using Zenject;

namespace Runtime.GameArea
{
    public class GameAreaInitializer : IInitializable
    {
        [Inject] private SignalBus _signalBus;
        
        public void Initialize()
        {
            _signalBus.Fire(new ChangeLoadingScreenActivationSignal(isActive: false, null));
        }
    }
}