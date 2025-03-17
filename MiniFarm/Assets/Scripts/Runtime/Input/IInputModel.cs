using Runtime.Input.Raycasting;

namespace Runtime.Input
{
    public interface IInputModel
    {
        IClickable Clickable { get; }

        public void SetClickable(IClickable clickable);

        public void ClearClickable();
    }
}