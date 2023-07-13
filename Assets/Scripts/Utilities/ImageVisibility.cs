using System;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RawImage))]
    public class ImageVisibility : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<Texture> OnLoadSingleView;
        
        public event Action OnBecameVisible;
        
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private RectTransform rectToCheck;
        private RawImage _image;
        private bool _isVisible;
        
        public RawImage GetImage()
        {
            return _image;
        }
        
        private void Awake()
        {
            _image = GetComponent<RawImage>();
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
            await SceneLoader.LoadScene(2,  LoadSceneMode.Single).ContinueWith(()=>
            {
                SelectedImageData.selectedSprite = _image.texture;
                OnLoadSingleView?.Invoke(_image.texture);
            });
        }
    }
}