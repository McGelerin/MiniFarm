using UnityEngine;

namespace Runtime.Audio
{
    public class AudioMediator
    {
        private readonly AudioView _view;

        public AudioMediator(AudioView view)
        {
            _view = view;
        }
        
        public void PlaySound(AudioClip audioClip)
        {
            _view.SoundAudioSource.PlayOneShot(audioClip);
        }
        
        public void PlayMusic(AudioClip audioClip)
        {
            _view.MusicAudioSource.clip = audioClip;
            _view.MusicAudioSource.loop = true;
            
            _view.MusicAudioSource.Play();
        }
    }
}