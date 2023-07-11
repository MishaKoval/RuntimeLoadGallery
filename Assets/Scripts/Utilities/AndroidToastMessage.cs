using UnityEngine;

namespace Utilities
{
    public static class AndroidToastMessage
    {
        public static void ShowAndroidToastMessage(string message)
        {
#if (PLATFORM_ANDROID && !UNITY_EDITOR)
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
#else
            Debug.Log(message);
#endif
        }
    }
}