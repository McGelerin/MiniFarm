using Runtime.Input.Raycasting;

namespace Runtime.Input
{
    public class InputModel : IInputModel
    {
        private IClickable _clickable;
        public IClickable Clickable => _clickable;
        
        public void SetClickable(IClickable clickable)
        {
            _clickable = clickable;
        }

        public void ClearClickable()
        {
            _clickable = null;
        }

        public bool IsTutorialRingClickable { get; set; }
    }
}