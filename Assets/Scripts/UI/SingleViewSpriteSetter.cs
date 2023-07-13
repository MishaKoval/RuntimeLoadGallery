using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SingleViewSpriteSetter : MonoBehaviour
    {
        private RawImage image;
        
        private void Awake()
        {
            image = GetComponent<RawImage>();
            GalleryElement.OnLoadSingleView += SetImage;
        }

        private void SetImage(Texture texture)
        {
            image.texture = texture;
        }

        private void OnDestroy()
        {
            GalleryElement.OnLoadSingleView -= SetImage;
        }
    }
}