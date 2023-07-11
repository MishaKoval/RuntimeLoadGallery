﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI
{
    public class LoadingProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text percentText;
        [SerializeField] private Volume volume;
        [SerializeField] private VolumeProfile volumeProfile;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            if (volumeProfile.TryGet(out Vignette vignette));
            {
                vignette.intensity.value = 0.0f;
                vignette.smoothness.value = 0.0f;
            }
            volume.priority = -1;
        }

#if UNITY_EDITOR
        [ContextMenu("TEST5")]
        private async void Test5Sec()
        {
            await ShowProgressBar(5.0f,null);
        }
        
        [ContextMenu("TEST1")]
        private async void Test1Sec()
        {
            await ShowProgressBar(1.0f,null);
        }
        
        [ContextMenu("TEST20")]
        private async void Test20Sec()
        {
            await ShowProgressBar(20f,null);
        }
#endif


        public async UniTask ShowProgressBar(float waitTime,Action callback,CancellationToken cancellationToken = default)
        {
            canvas.SetActive(true);
            volume.priority = 1f;
            if (volumeProfile.TryGet(out Vignette vignette));
            {
                vignette.intensity.value = 1.0f;
                vignette.smoothness.value = 1.0f;
            }
            var step = waitTime / 100;
            slider.value = 0;
            while (slider.value < 1.0f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(step), cancellationToken: cancellationToken);
                slider.value += 0.01f;
                vignette.intensity.value -= 0.01f;
                vignette.smoothness.value -= 0.01f;
                percentText.text = (int)(slider.value * 100) + "%";
            }
            vignette.intensity.value = 0.0f;
            vignette.smoothness.value = 0.0f;
            volume.priority = -1;
            canvas.SetActive(false);
            callback?.Invoke();
        }
    }
}