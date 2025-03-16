namespace Runtime.Haptic.Model
{
    public interface IHapticModel
    {
        public bool IsHapticIdle { get; }
        public void SetHaptic(bool isActive);
    }
}