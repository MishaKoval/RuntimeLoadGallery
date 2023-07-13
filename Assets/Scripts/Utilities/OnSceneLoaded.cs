using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class OnSceneLoaded : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void SceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
        {
            if (scene.buildIndex == 2)
            {
                Debug.Log("Loaded");
            }
        }

        private void Start()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }
    }
}