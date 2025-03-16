using UnityEngine;

namespace Runtime.Audio
{
    public class AudioView : MonoBehaviour
    {
        [SerializeField] private AudioSource soundAudioSource;
        [SerializeField] private AudioSource musicAudioSource;

        public AudioSource SoundAudioSource => soundAudioSource;
        public AudioSource MusicAudioSource => musicAudioSource;

    }
}