using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Utilities
{
    public class PhotoDownloader : MonoBehaviour
    {
        [SerializeField] private List<RectVisibility> imagesVisibility = new();
        [SerializeField] private List<GalleryElement> galleryElements;
        [SerializeField] private Texture loadingSprite;
        
        private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private readonly List<string> _imageUrls = new();
        private readonly List<Action> _actions = new();

        private void OnEnable()
        {
            for (int i = 0; i < imagesVisibility.Count; i++)
            {
                var index = i;
                var lambda =  new Action(() => DownloadImageWithIndex(index));
                _actions.Add(lambda);
                imagesVisibility[index].OnBecameVisible += lambda;
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 120;
            for (int i = 0; i < 66; i++)
            {
                _imageUrls.Add(URL + (i + 1) + ".jpg");
            }
            
            AndroidToastMessage.ShowAndroidToastMessage("Download started!");
        }

        private void OnDisable()
        {
            for (int i = 0; i < imagesVisibility.Count; i++)
            {
                imagesVisibility[i].OnBecameVisible -= _actions[i];
            }
        }

        private async void DownloadImageWithIndex(int index)
        {
            if (galleryElements[index].GetImage().texture == null)
            {
                galleryElements[index].GetImage().texture = loadingSprite;
                try
                {
                    Texture2D texture2D =
                        await DownloadImageAsync(_imageUrls[index], this.GetCancellationTokenOnDestroy());
                    if (texture2D != null)
                    {
                        galleryElements[index].GetImage().texture = texture2D;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }


        private async UniTask<Texture2D> DownloadImageAsync(string imageUrl,CancellationToken cancellationToken)
        {
            using var request = UnityWebRequestTexture.GetTexture(imageUrl);
            try
            {
                await request.SendWebRequest().WithCancellation(cancellationToken);
            }
            catch (Exception)
            {
                return null;
            }
            if (request.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(request);
            }
            return null;
        }

        private void OnValidate()
        {
            Transform content = GameObject.Find("Content").transform;
            imagesVisibility = content.GetComponentsInChildren<RectVisibility>().ToList();
            galleryElements = content.GetComponentsInChildren<GalleryElement>().ToList();
        }
    }
}