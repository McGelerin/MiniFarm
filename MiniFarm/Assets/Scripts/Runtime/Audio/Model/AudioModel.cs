using Runtime.Audio.Data;
using UnityEngine.Audio;
using Zenject;

namespace Runtime.Audio.Model
{
    public class AudioModel : IInitializable, IAudioModel
    {
        private readonly AudioData _audioData;
        private AudioMixer _audioMixer;
        public AudioData AudioData => _audioData;
        
        public AudioModel(AudioData audioData)
        {
            _audioData = audioData;
        }
        
        private const float DEFAULT_VOLUME = .8f;
        
        private const string SOUND_PARAM = "SOUND_PARAM";
        private const string MUSIC_PARAM = "MUSIC_PARAM";
        private const string SOUND_PATH = "SOUND_PATH";
        
        private bool _isSoundMuted;
        public bool IsSoundMuted => _isSoundMuted;
        
        private bool _isMusicMuted;
        public bool IsMusicMuted => _isMusicMuted;


        public void Initialize()
        {
            _isSoundMuted = ES3.Load<bool>(nameof(_isSoundMuted), SOUND_PATH, false);
            _isMusicMuted = ES3.Load<bool>(nameof(_isMusicMuted), SOUND_PATH, false);

            _audioMixer = _audioData.Mixer; 
            
            if(_isMusicMuted)
                MuteMusic();
            if(_isSoundMuted)
                MuteSound();
        }
        
        public void StopMusic()
        {
            throw new System.NotImplementedException();
        }
        
        public void MuteSound()
        {
            _audioMixer.SetFloat(SOUND_PARAM, -80);
            _isSoundMuted = true;
            ES3.Save<bool>(nameof(_isSoundMuted), _isSoundMuted, SOUND_PATH);
        }

        public void UnMuteSound()
        {
            _audioMixer.SetFloat(SOUND_PARAM, 0);
            _isSoundMuted = false;
            ES3.Save<bool>(nameof(_isSoundMuted), _isSoundMuted, SOUND_PATH);
        }

        public void MuteMusic()
        {
            _audioMixer.SetFloat(MUSIC_PARAM, -80);
            _isMusicMuted = true;
            ES3.Save<bool>(nameof(_isMusicMuted), _isMusicMuted, SOUND_PATH);
        }

        public void UnMuteMusic()
        {
            _audioMixer.SetFloat(MUSIC_PARAM, -5);
            _isMusicMuted = false;
            ES3.Save<bool>(nameof(_isMusicMuted), _isMusicMuted, SOUND_PATH);
        }
    }
}