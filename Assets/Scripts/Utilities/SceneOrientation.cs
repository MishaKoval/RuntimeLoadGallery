using UnityEngine;

namespace Utilities
{
    public class SceneOrientation : MonoBehaviour
    {
        private enum RequiredSceneOrientation
        {
            Autorotate,
            Portrait
        }

        [SerializeField] private RequiredSceneOrientation requiredSceneOrientation;

        private void Awake()
        {
            if (requiredSceneOrientation == RequiredSceneOrientation.Autorotate)
            {
                DeviceOrientation.EnableAutoRotation();
            }
            else
            {
                DeviceOrientation.LockPortraitOrientation();
            }
        }
    }
}