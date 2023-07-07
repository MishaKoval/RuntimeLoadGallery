using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CheckVisibility : MonoBehaviour
{
    private bool _isVisible;
    public event Action OnBecameVisible;
    
    [SerializeField] private RectTransform canvasRect;

    [SerializeField] private RectTransform rectToCheck;

    private void CheckVisible()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(canvasRect, rectToCheck.position))
        {
            if (!_isVisible)
            {
                _isVisible = true;
                OnBecameVisible?.Invoke();
            }
        }
        else
        {
            _isVisible = false;
        }
    }

    private void Update()
    {
        CheckVisible();
    }
}