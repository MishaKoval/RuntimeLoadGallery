using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public static class SceneLoader
    {
        public static async UniTask LoadPreviousScene()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1,LoadSceneMode.Single);
            }
            else
            {
                Application.Quit();
            }
        }

        public static async UniTask LoadScene(int index,LoadSceneMode loadSceneMode)
        {
            await SceneManager.LoadSceneAsync(index,loadSceneMode);
        }
    }
}