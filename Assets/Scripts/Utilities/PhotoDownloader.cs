using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Networking;

namespace Utilities
{
    public class PhotoDownloader : MonoBehaviour
    {
        [SerializeField] private Texture loadingSprite;
        [SerializeField] private GameObject galleryElementPrefab;
        [SerializeField] private Transform contentTransform;
        
        [SerializeField] private RectTransform viewPortRect;

        private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        
        private readonly List<RectVisibility> _rectVisibilities = new();
        private readonly List<GalleryElement> _galleryElements = new();
        private readonly List<string> _imageUrls = new();
        private readonly List<Action> _actions = new();

        private void Start()
        {
            Application.targetFrameRate = 120;
            for (int i = 0; i < 66; i++)
            {
                _imageUrls.Add(URL + (i + 1) + ".jpg");
            }

            for (int i = 0; i < 66; i++)
            {
                var index = i;
                var lambda =  new Action(() => DownloadImageWithIndex(index));
                _actions.Add(lambda);
                var galleryPrefab = Instantiate(galleryElementPrefab, contentTransform);
                var rectVisibility = galleryPrefab.GetComponent<RectVisibility>();
                rectVisibility.SetViewPortRect(viewPortRect);
                _rectVisibilities.Add(rectVisibility);
                var galleryElement = galleryPrefab.GetComponent<GalleryElement>();
                _galleryElements.Add(galleryElement);
                rectVisibility.OnBecameVisible += lambda;
            }
            
            AndroidToastMessage.ShowAndroidToastMessage("Downloading...");
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _rectVisibilities.Count; i++)
            {
                _rectVisibilities[i].OnBecameVisible -= _actions[i];
            }
        }

        private async void DownloadImageWithIndex(int index)
        {
            if (_galleryElements[index].GetImage().texture == null)
            {
                _galleryElements[index].GetImage().texture = loadingSprite;
                try
                {
                    
                    Texture2D texture2D;
                    if (File.Exists(Path.Combine(Application.persistentDataPath,index + ".jpg")))
                    {
                        texture2D = await DownloadImageAsync(Path.Combine("file://" + Application.persistentDataPath,index + ".jpg"), this.GetCancellationTokenOnDestroy());
                    }
                    else
                    {
                        texture2D = 
                            await DownloadImageAsync(_imageUrls[index], this.GetCancellationTokenOnDestroy());
                        byte[] bytes = texture2D.EncodeToJPG();
                        var savePath = Path.Combine(Application.persistentDataPath,index + ".jpg");
                        if (!Directory.Exists(Application.persistentDataPath))
                        {
                            Directory.CreateDirectory(Application.persistentDataPath);
                        }
                        await File.WriteAllBytesAsync(savePath, bytes);
                    }
                    
                    if (texture2D != null)
                    {
                        _galleryElements[index].GetImage().texture = texture2D;
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
    }
}