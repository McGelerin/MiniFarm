using Runtime.Audio.Data;

namespace Runtime.Audio.Model
{
    public interface IAudioModel
    {
        public bool IsSoundMuted { get; }
        public bool IsMusicMuted { get; }
        public AudioData AudioData { get; }
        
        public void StopMusic();
        public void MuteSound();
        public void UnMuteSound();
        public void MuteMusic();
        public void UnMuteMusic();
    }
}