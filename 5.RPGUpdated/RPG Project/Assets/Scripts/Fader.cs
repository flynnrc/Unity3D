using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        //otherwise fade out and fade in could be called at the same time
        private bool isFadingIn = false;
        private bool isFadingOut = false;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1 && !isFadingIn)
            {
                isFadingOut = true;
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
            isFadingOut = false;
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0 && !isFadingOut)
            {
                isFadingIn = true;
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
            isFadingIn = false;
        }
    }
}