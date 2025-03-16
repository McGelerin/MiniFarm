using System;
using Runtime.Audio.Model;
using Runtime.Audio.Signal.Audio;
using Runtime.Identifiers.Template;
using UniRx;
using UnityEngine;

namespace Runtime.Audio
{
    public class AudioController : SignalListener
    {
        private readonly IAudioModel _audioModel;
        private readonly AudioMediator _audioMediator;

        public AudioController(IAudioModel audioModel,AudioMediator audioMediator)
        {
            _audioModel = audioModel;
            _audioMediator = audioMediator;
        }
        
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<AudioPlaySignal>()
                .Subscribe(OnAudioPlaySignal)
                .AddTo(_disposables);
            
            _signalBus.GetStream<AudioButtonTypeSignal>()
                .Subscribe(OnAudioTypeSignal)
                .AddTo(_disposables);
        }

        private void OnAudioPlaySignal(AudioPlaySignal audioPlaySignal)
        {
            AudioClip audioClip = _audioModel.AudioData.AudioClips[audioPlaySignal.Sounds];

            switch (audioPlaySignal.AudioPlayers)
            {
                case AudioPlayers.Sound:
                    _audioMediator.PlaySound(audioClip);
                    break;
                case AudioPlayers.Music:
                    _audioMediator.PlayMusic(audioClip);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnAudioTypeSignal(AudioButtonTypeSignal audioButtonTypeSignal)
        {
            switch (audioButtonTypeSignal.AudioType)
            {
                case AudioType.Sound:
                    if (audioButtonTypeSignal.IsMute)
                        _audioModel.MuteSound();
                    else
                        _audioModel.UnMuteSound();
                    break;
                case AudioType.Music:
                    if (audioButtonTypeSignal.IsMute)
                        _audioModel.MuteMusic();
                    else
                        _audioModel.UnMuteMusic();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}