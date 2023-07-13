using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RawImage))]
    public class SingleViewSpriteSetter : MonoBehaviour
    {
        private RawImage image;
        
        private void Awake()
        {
            image = GetComponent<RawImage>();
            if (SelectedPhotoData.selectedTexture != null)
            {
                image.texture = SelectedPhotoData.selectedTexture;
            }
        }
    }
}