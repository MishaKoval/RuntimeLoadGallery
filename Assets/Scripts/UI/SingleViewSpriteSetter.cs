using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class SingleViewSpriteSetter : MonoBehaviour
    {
        private Image image;
        
        private void Awake()
        {
            image = GetComponent<Image>();
            CheckImageVisibility.OnLoadSingleView += SetImage;
        }

        private void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        private void OnDestroy()
        {
            CheckImageVisibility.OnLoadSingleView -= SetImage;
        }
    }
}