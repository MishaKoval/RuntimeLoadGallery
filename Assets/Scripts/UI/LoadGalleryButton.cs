using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class LoadGalleryButton : MonoBehaviour
    {
        private Button btn;

        private void Awake()
        {
            btn = GetComponent<Button>();
        }

        private void Start()
        {
            btn.onClick.AddListener(LoadGallery);
        }

        private async void LoadGallery()
        {
            await SceneLoader.LoadScene(1,LoadSceneMode.Single);
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveListener(LoadGallery);
        }
    }
}