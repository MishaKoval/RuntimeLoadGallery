using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace UI
{
    public class BackButton : MonoBehaviour
    {
        private bool isEnabled = true;

        private async void Update()
        {
            if (isEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isEnabled = false;
                    await SceneLoader.LoadPreviousScene().ContinueWith(()=> isEnabled = true);
                }
            }
        }
    }
}