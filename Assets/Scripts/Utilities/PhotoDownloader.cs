using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utilities
{
    public class PhotoDownloader : MonoBehaviour
    {
        private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";

        [SerializeField] private List<Image> startImages;

        [SerializeField] private List<CheckImageVisibility> checkImageVisibility = new List<CheckImageVisibility>();

        [SerializeField] private Sprite loadingSprite;
        
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

            
            AndroidToastMessage.ShowAndroidToastMessage("Загрузка началась!");
            for (int i = 0; i < startImages.Count; i++)
            {
                startImages[i].sprite = loadingSprite;
            }

            var startUrls = _imageUrls.GetRange(0, startImages.Count);

            var startTextures = await UniTask.WhenAll(startUrls.Select(DownloadImageAsync));

            for (int i = 0; i < startTextures.Length; i++)
            {
                startImages[i].sprite =SpriteExtension.Scale(startTextures[i],100,100).ConvertToSprite();
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
            if (checkImageVisibility[index].GetImage().sprite == null)
            {
                checkImageVisibility[index].GetImage().sprite = loadingSprite;
                //todo add settings for compress
                checkImageVisibility[index].GetImage().sprite = SpriteExtension.Scale(await DownloadImageAsync(_imageUrls[index + 8]),100,100).ConvertToSprite();
            }
        }


        private async UniTask<Texture2D> DownloadImageAsync(string imageUrl)
        {
            var cert = new Certificate();
            using var request = UnityWebRequestTexture.GetTexture(imageUrl);
            request.certificateHandler = cert;
            try
            {
                await request.SendWebRequest();
            }
            catch (Exception)
            {
                return null;
            }

            cert.Dispose();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerTexture.GetContent(request);
            }
            
            return null;
        }

        private void OnValidate()
        {
            Transform content = GameObject.Find("Content").transform;
            checkImageVisibility = content.GetComponentsInChildren<CheckImageVisibility>().ToList();
        }
    }
}