using System;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    public class RectVisibility : MonoBehaviour
    {
        public event Action OnBecameVisible;
        
        [SerializeField] private RectTransform viewPortRect;
        [SerializeField] private RectTransform rectToCheck;
        private bool _isVisible;

        public void SetViewPortRect(RectTransform viewPortRect)
        {
            this.viewPortRect = viewPortRect;
        }

        private void CheckVisible()
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(viewPortRect, rectToCheck.position))
            {
                if (!_isVisible)
                {
                    _isVisible = true;
                    OnBecameVisible?.Invoke();
                }
            }
            else
            {
                _isVisible = false;
            }
        }

        private void LateUpdate()
        {
            CheckVisible();
        }
    }
}