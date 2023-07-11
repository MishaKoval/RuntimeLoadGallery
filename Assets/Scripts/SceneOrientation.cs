using UnityEngine;

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
            Utilities.DeviceOrientation.EnableAutoRotation();
        }
        else
        {
            Utilities.DeviceOrientation.LockPortraitOrientation();
        }
    }
}