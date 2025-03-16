using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Runtime.Audio.Data
{
    [CreateAssetMenu(fileName = "Sounds Data", menuName = "Data/Sounds Data", order = 0)]
    public class AudioData : SerializedScriptableObject
    {
        [SerializeField] private AudioMixer _audioMixer;
        public AudioMixer Mixer => _audioMixer;
        
        [SerializeField]
        private Dictionary<Sounds, AudioClip> _audioClips = new Dictionary<Sounds, AudioClip>();
        public Dictionary<Sounds, AudioClip> AudioClips => _audioClips;
    }

    public enum Sounds
    {
        None,
        Gameplay1
    }
}