using UnityEngine;

namespace Utilities
{
    public static class DeviceOrientation
    {
        public static void LockPortraitOrientation()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        public static void EnableAutoRotation()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}