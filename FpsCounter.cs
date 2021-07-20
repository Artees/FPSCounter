using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Artees.FPSCounter
{
    [RequireComponent(typeof(Image))]
    public class FpsCounter : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Sprite[] spriteSheet;
#pragma warning restore 0649

        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            StartCoroutine(Count());
        }

        private IEnumerator<WaitForEndOfFrame> Count()
        {
            const float smoothness = 0.8f;
            const float sharpness = 1f - smoothness;
            var wait = new WaitForEndOfFrame(); // Cached to avoid GC
            var smoothUnscaledDeltaTime = 1f / 60f;
            while (gameObject)
            {
                for (var i = 0; i < 60; i++)
                {
                    yield return wait;
                    smoothUnscaledDeltaTime = smoothness * smoothUnscaledDeltaTime + sharpness * Time.deltaTime;
                }

                var fps = Mathf.RoundToInt(1f / smoothUnscaledDeltaTime);
                if (fps < spriteSheet.Length) _image.sprite = spriteSheet[fps];
            }
        }
    }
}
