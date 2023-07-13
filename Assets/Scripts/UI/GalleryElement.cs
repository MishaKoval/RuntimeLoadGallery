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
        private RawImage image;
        
        public RawImage GetImage()
        {
            return image;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            LoadSingleView();
        }
        
        private void Awake()
        {
            image = GetComponent<RawImage>();
        }
        
        private async void LoadSingleView()
        {
            SelectedPhotoData.selectedTexture = image.texture;
            await LoadingProgressBar.Instance.ShowProgressBar(0.5f);
            await SceneLoader.LoadScene(2,  LoadSceneMode.Single).ContinueWith(LoadingProgressBar.Instance.DisableProgressBar);
        }
    }
}