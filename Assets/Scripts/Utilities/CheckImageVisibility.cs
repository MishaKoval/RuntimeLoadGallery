using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Image))]
    public class CheckImageVisibility : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<Sprite> OnLoadSingleView;
        
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

        public void OnPointerClick(PointerEventData eventData)
        {
            LoadSingleView();
        }
        
        private async void LoadSingleView()
        {
            await SceneLoader.LoadScene(2,  LoadSceneMode.Single,()=>
            {
                OnLoadSingleView?.Invoke(_image.sprite);
            });
        }
    }
}