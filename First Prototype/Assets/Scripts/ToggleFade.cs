    using UnityEngine;
    using UnityEngine.UI;

    public class ToggleFade : MonoBehaviour {
        public GameObject targetObject;
        public float fadeDuration = 0.5f;
        private CanvasGroup canvasGroup;
        private bool isFadingIn = false;
        private bool isFadingOut = false;

        void Start() {
            canvasGroup = targetObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null) {
                Debug.LogError("Target object must have a CanvasGroup component.");
                enabled = false;
                return;
            }
        }

        public void ToggleActive() {
            if (targetObject.activeSelf) {
                StartCoroutine(FadeOut());
            } else {
                StartCoroutine(FadeIn());
            }
        }

        private System.Collections.IEnumerator FadeIn() {
            isFadingIn = true;
            canvasGroup.alpha = 0f;
            targetObject.SetActive(true);

            float startTime = Time.time;
            while (canvasGroup.alpha < 1f && !isFadingOut) {
                float elapsedTime = Time.time - startTime;
                canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = 1f;
            isFadingIn = false;
        }

        private System.Collections.IEnumerator FadeOut() {
            isFadingOut = true;
            canvasGroup.alpha = 1f;

            float startTime = Time.time;
            while (canvasGroup.alpha > 0f && !isFadingIn) {
                float elapsedTime = Time.time - startTime;
                canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
                yield return null;
            }

            targetObject.SetActive(false);
            canvasGroup.alpha = 0f;
            isFadingOut = false;
        }
    }