using Runtime.Audio.Data;

namespace Runtime.Audio.Signal.Audio
{
    public readonly struct AudioPlaySignal
    {
        private readonly AudioPlayers _audioPlayers;
        private readonly Sounds _sounds;

        public AudioPlaySignal(AudioPlayers audioPlayers, Sounds sounds)
        {
            _audioPlayers = audioPlayers;
            _sounds = sounds;
        }

        public AudioPlayers AudioPlayers => _audioPlayers;

        public Sounds Sounds => _sounds;
    }
}