using System;
using Cysharp.Threading.Tasks;
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
            try
            {
                await LoadingProgressBar.Instance.ShowProgressBar(2.0f,this.GetCancellationTokenOnDestroy());
                await SceneLoader.LoadScene(1,LoadSceneMode.Single).ContinueWith(LoadingProgressBar.Instance.DisableProgressBar);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveListener(LoadGallery);
        }
    }
}