using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utilities
{
    public class PhotoDownloader : MonoBehaviour
    {
        [SerializeField] private List<RawImage> startImages;
        [SerializeField] private List<ImageVisibility> checkImageVisibility = new List<ImageVisibility>();
        [SerializeField] private Texture loadingSprite;
        
        private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
        private readonly List<string> _imageUrls = new List<string>();
        private readonly List<Action> _actions = new List<Action>();

        private void OnEnable()
        {
            for (int i = 0; i < checkImageVisibility.Count; i++)
            {
                var index = i;
                var lambda =  new Action(() => DownloadImageWithIndex(index));
                _actions.Add(lambda);
                checkImageVisibility[index].OnBecameVisible += lambda;
            }
        }

        private async void Start()
        {
            Application.targetFrameRate = 120;
            for (int i = 0; i < 66; i++)
            {
                _imageUrls.Add(URL + (i + 1) + ".jpg");
            }
            
            AndroidToastMessage.ShowAndroidToastMessage("Download started!");
            for (int i = 0; i < startImages.Count; i++)
            {
                startImages[i].texture = loadingSprite;
            }

            var startUrls = _imageUrls.GetRange(0, startImages.Count);

            List<UniTask<Texture2D>> startImagesLoadTasks = new List<UniTask<Texture2D>>();

            for (int i = 0; i < startUrls.Count; i++)
            {
                startImagesLoadTasks.Add(DownloadImageAsync(startUrls[i],this.GetCancellationTokenOnDestroy()));
            }

            var startTextures = await UniTask.WhenAll(startImagesLoadTasks);

            for (int i = 0; i < startTextures.Length; i++)
            {
                startImages[i].texture =startTextures[i];
            }
            
            for (int i = 0; i < checkImageVisibility.Count; i++)
            {
                checkImageVisibility[i].GetImage().maskable = false;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < checkImageVisibility.Count; i++)
            {
                checkImageVisibility[i].OnBecameVisible -= _actions[i];
            }
        }

        private async void DownloadImageWithIndex(int index)
        {
            if (checkImageVisibility[index].GetImage().texture == null)
            {
                checkImageVisibility[index].GetImage().texture = loadingSprite;
                //todo add settings for compress
                try
                {
                    Texture2D texture2D =
                        await DownloadImageAsync(_imageUrls[index + 8], this.GetCancellationTokenOnDestroy());
                    if (texture2D != null)
                    {
                        checkImageVisibility[index].GetImage().texture = texture2D;
                        //checkImageVisibility[index].GetImage().sprite = SpriteExtension.Scale(texture2D,100,100).ConvertToSprite();
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
            checkImageVisibility = content.GetComponentsInChildren<ImageVisibility>().ToList();
        }
    }
}