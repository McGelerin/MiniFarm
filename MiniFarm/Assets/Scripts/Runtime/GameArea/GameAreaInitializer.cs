using Runtime.Audio;
using Runtime.Audio.Data;
using Runtime.Audio.Signal.Audio;
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
            _signalBus.Fire(new AudioPlaySignal(AudioPlayers.Music, Sounds.Gameplay1));
        }
    }
}