using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Download : MonoBehaviour
{
    public class ForceAcceptAll : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
    
    
    private static string url = "http://data.ikppbb.com/test-task-unity-data/pics/";

    [SerializeField] private List<Image> startImages;

    [SerializeField] private List<Image> _images = new List<Image>();
    
    private List<string> imageUrls = new List<string>();


    private async void Start()
    {
        for (int i = 0; i < 66; i++)
        {
            imageUrls.Add(url + (i + 1)+ ".jpg");
        }

        List<string> startUrls = new List<string>();
        startUrls = imageUrls.GetRange(0, startImages.Count);

        var startTextures = await UniTask.WhenAll(startUrls.Select(DownloadImageAsync));
        
        for (int i = 0; i < startTextures.Length; i++)
        {
            startImages[i].sprite = startTextures[i].ConvertToSprite();
        }
        
        /*List<UniTask<Texture2D>> tasks = new List<UniTask<Texture2D>>();
        for (int i = 0; i < 66; i++)
        {
            var task = DownloadImageAsync(imageUrls[i]);
            tasks.Add(task);
        }

        for (int i = 0; i < 66; i++)
        {
            Texture2D a = await tasks[i];
            if (a != null)
            {
                _images[i].sprite = a.ConvertToSprite();
            }
        }*/
    }

    private async UniTask<Texture2D> DownloadImageAsync(string imageUrl)
    {
        var cert = new ForceAcceptAll();
        using var request = UnityWebRequestTexture.GetTexture(imageUrl);
        request.certificateHandler = cert;
        try
        {
            await request.SendWebRequest();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
       
        return request.result == UnityWebRequest.Result.Success
            ? DownloadHandlerTexture.GetContent(request)
            : null;
    }
}
