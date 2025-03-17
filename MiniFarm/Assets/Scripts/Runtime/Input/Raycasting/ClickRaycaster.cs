using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Runtime.Input.Raycasting
{
    public class ClickRaycaster : IClickRaycaster 
    {
        private RaycastHit[] _raycastHits = new RaycastHit[5];
        private readonly int _layerMask = 1 << 6;
        private const float RAYCAST_DISTANCE = 300f;

        private Camera _mainCamera;

        [Inject]
        public ClickRaycaster()
        {
            _mainCamera = Camera.main;
        }
        
        public IClickable RaycastTouchPosition(Vector3 clickPosition)
        {
            Ray worldPosition = _mainCamera.ScreenPointToRay(clickPosition);
            
            int hitCount = Physics.RaycastNonAlloc(worldPosition, _raycastHits, RAYCAST_DISTANCE, _layerMask);

            Debug.DrawLine(worldPosition.origin, worldPosition.direction * RAYCAST_DISTANCE, Color.green);
            
            if (hitCount == 0) return null;
            
            // Hit sonuçlarını mesafeye göre sıralayın
            Array.Sort(_raycastHits, 0, hitCount, new RaycastHitDistanceComparer());
            
            for (var index = 0 ; index < hitCount; index++)
            {
                var raycastHit = _raycastHits[index];
                
                if (raycastHit.transform.parent.parent.TryGetComponent(out IClickable order))
                {
                    return order;
                }
            }

            return null;
        }
        
        // RaycastHit mesafesine göre sıralama yapan Comparer
        private class RaycastHitDistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }
    }
}