using Lofelt.NiceVibrations;

namespace Runtime.Haptic.Signal
{
    public struct VibrateSignal
    {
        private readonly HapticPatterns.PresetType _hapticType;

        public VibrateSignal(HapticPatterns.PresetType hapticType)
        {
            _hapticType = hapticType;
        }

        public HapticPatterns.PresetType HapticType => _hapticType;
    }
}