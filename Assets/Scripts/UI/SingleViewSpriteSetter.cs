using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class SingleViewSpriteSetter : MonoBehaviour
    {
        private RawImage image;
        
        private void Awake()
        {
            image = GetComponent<RawImage>();
            ImageVisibility.OnLoadSingleView += SetImage;
        }

        private void SetImage(Texture texture)
        {
            image.texture = texture;
        }

        private void OnDestroy()
        {
            ImageVisibility.OnLoadSingleView -= SetImage;
        }
    }
}