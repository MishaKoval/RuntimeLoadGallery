using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public static class SceneLoader    
    {
        public static async UniTask LoadScene(int index,LoadSceneMode loadSceneMode,Action onLoaded = null)
        {
            await SceneManager.LoadSceneAsync(index,loadSceneMode);
            onLoaded?.Invoke();
        }
    }
}