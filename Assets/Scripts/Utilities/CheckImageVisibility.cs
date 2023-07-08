using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class CheckImageVisibility : MonoBehaviour
    {
        private bool _isVisible;
        public event Action OnBecameVisible;
    
        [SerializeField] private RectTransform canvasRect;

        [SerializeField] private RectTransform rectToCheck;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public Image GetImage()
        {
            return _image;
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