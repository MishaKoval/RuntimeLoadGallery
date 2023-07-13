using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    [RequireComponent(typeof(RawImage))]
    public class GalleryElement : MonoBehaviour,IPointerClickHandler
    {
        public static event Action<Texture> OnLoadSingleView;
        
        private RawImage image;
        
        public RawImage GetImage()
        {
            return image;
        }
        
        private void Awake()
        {
            image = GetComponent<RawImage>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            LoadSingleView();
        }
        
        private async void LoadSingleView()
        {
            await SceneLoader.LoadScene(2,  LoadSceneMode.Single).ContinueWith(()=>
            {
                SelectedImageData.selectedSprite = image.texture;
                OnLoadSingleView?.Invoke(image.texture);
            });
        }
    }
}