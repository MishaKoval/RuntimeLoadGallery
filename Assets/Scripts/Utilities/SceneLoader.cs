using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public static class SceneLoader    
    {
        public static async UniTask LoadScene(int index,LoadSceneMode loadSceneMode)
        {
            await SceneManager.LoadSceneAsync(index,loadSceneMode);
        }
    }
}