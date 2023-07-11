using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class CheckImageVisibility : MonoBehaviour
    {
        public event Action OnBecameVisible;
        
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private RectTransform rectToCheck;
        private Image _image;
        private bool _isVisible;
        
        public Image GetImage()
        {
            return _image;
        }
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        private void CheckVisible()
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(canvasRect, rectToCheck.position))
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

        private void Update()
        {
            CheckVisible();
        }
    }
}