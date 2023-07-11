using UnityEngine;

namespace UI
{
    public class CameraForLoadingCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        
        private void OnEnable()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}