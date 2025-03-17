using UnityEngine;

namespace Runtime.Input.Raycasting
{
    public interface IClickRaycaster
    {
        public IClickable RaycastTouchPosition(Vector3 clickPosition);
    }
}