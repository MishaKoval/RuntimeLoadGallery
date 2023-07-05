using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Download : MonoBehaviour
{
    private static string url = "https://data.ikppbb.com/test-task-unity-data/pics/";

    [SerializeField] private List<Image> _images = new List<Image>();
    
    private List<string> imageUrls = new List<string>();


    private async void Start()
    {
        for (int i = 0; i < 66; i++)
        {
            imageUrls.Add(url + (i + 1)+ ".jpg");
        }
        var textures = await UniTask.WhenAll(imageUrls.Select(DownloadImageAsync));
        for (int i = 0; i < 66; i++)
        {
            _images[i].sprite = textures[i].ConvertToSprite();
        }
    }

    private void Update()
    {
        
    }
    
   
    
    private async UniTask<Texture2D> DownloadImageAsync(string imageUrl)
    {
        using var request = UnityWebRequestTexture.GetTexture(imageUrl);

        await request.SendWebRequest();

        return request.result == UnityWebRequest.Result.Success
            ? DownloadHandlerTexture.GetContent(request)
            : null;
    }
}
