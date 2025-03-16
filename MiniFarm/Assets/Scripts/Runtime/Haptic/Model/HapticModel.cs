using Zenject;

namespace Runtime.Haptic.Model
{
    public class HapticModel : IHapticModel, IInitializable
    {
        private const string HAPTIC_PATH = "HAPTIC_PATH";
        private bool _isHapticIdle;
        public bool IsHapticIdle => _isHapticIdle;
        
        public void Initialize()
        {
            _isHapticIdle = ES3.Load<bool>(nameof(_isHapticIdle), HAPTIC_PATH, false);
        }
        
        public void SetHaptic(bool isActive)
        {
            _isHapticIdle = isActive;
            ES3.Save<bool>(nameof(_isHapticIdle), _isHapticIdle, HAPTIC_PATH);
        }
    }
}