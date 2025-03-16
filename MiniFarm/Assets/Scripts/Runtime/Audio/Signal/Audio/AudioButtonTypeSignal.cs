namespace Runtime.Audio.Signal.Audio
{
    public readonly struct AudioButtonTypeSignal
    {
        private readonly AudioType _audioType;
        private readonly bool _isMute;

        public AudioButtonTypeSignal(AudioType audioType, bool isMute)
        {
            _audioType = audioType;
            _isMute = isMute;
        }

        public AudioType AudioType => _audioType;

        public bool IsMute => _isMute;
    }
}