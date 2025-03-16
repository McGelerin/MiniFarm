using Lofelt.NiceVibrations;
using Runtime.Haptic.Model;
using Runtime.Haptic.Signal;
using Runtime.Identifiers.Template;
using UniRx;

namespace Runtime.Haptic
{
    public class HapticController : SignalListener
    {
        private readonly IHapticModel _hapticModel;

        public HapticController(IHapticModel hapticModel)
        {
            _hapticModel = hapticModel;
        }
        
        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<VibrateSignal>()
                .Subscribe(OnVibrateSignal)
                .AddTo(_disposables);
        }
        
        private void OnVibrateSignal(VibrateSignal signal)
        {
            if (_hapticModel.IsHapticIdle) return;
            
            HapticPatterns.PlayPreset(signal.HapticType);
        }
    }
}