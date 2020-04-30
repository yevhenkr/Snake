using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    // [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasGroup))]
    public class Fader : MonoBehaviour
    {
        [Header("Fade In:")]
        public bool autoFadeIn = true;
        public float FadeInTime = 0.25f;
        public float FadeInAlphaFrom = 0f;
        public float FadeInAlphaTo = 1f;

        [Header("Fade Out:")]
        public bool autoFadeOut = true;
        public float FadeOutTime = 0.25f;
        public float FadeOutAlphaFrom = 1f;
        public float FadeOutAlphaTo = 0f;

        [Header("After Fade Out:")]
        // public bool autoEnableGameObject = true;
        public bool autoDisableGameObject = false;
        public bool autoDestroyGameObject = false;

        bool _visible;
        public bool isIn { get { return _visible; } }

        IEnumerator coroutine = None();

        void Awake()
        {
            // _visible = GetComponent<CanvasGroup>().alpha >= FadeInAlphaFrom;
            _visible = GetComponent<CanvasGroup>().alpha >= FadeInAlphaTo;
        }

        static IEnumerator None() { yield return null; }

        void OnEnable() { if (autoFadeIn) FadeIn(); }

        void OnDisable() { if (autoFadeOut) FadeOut(); }


        public void FadeIn()
        {
            _visible = true;

            // if (autoEnableGameObject)   {                    gameObject.SetActive(true);                }

            if (gameObject.activeInHierarchy)
            {
                // if (coroutine != null) 
                StopCoroutine(coroutine);
                coroutine = FadeTo(GetComponent<CanvasGroup>(), FadeInAlphaFrom, FadeInAlphaTo, FadeInTime);
                StartCoroutine(coroutine);
            }
            ////////// НА БУДУЩЕЕ: РЕАЛИЗОВАТЬ АВТО ВКЛЮЧЕНИЕ gameObject //////////

            // else
            // {
            //     if (autoEnableGameObject)
            //     {
            //         gameObject.SetActive(true);
            //         if ((!autoFadeIn))
            //         {
            //             StopCoroutine(coroutine);
            //             coroutine = FadeTo(GetComponent<CanvasGroup>(), FadeInAlphaFrom, FadeInAlphaTo, FadeInTime);
            //             StartCoroutine(coroutine);
            //         }
            //     }
            // }
        }

        public void FadeOut()
        {
            _visible = false;
            if (gameObject.activeInHierarchy)
            {
                // if (coroutine != null) 
                StopCoroutine(coroutine);
                coroutine = FadeTo(GetComponent<CanvasGroup>(), FadeOutAlphaFrom, FadeOutAlphaTo, FadeOutTime, autoDisableGameObject, autoDestroyGameObject);
                StartCoroutine(coroutine);
            }
        }

        public static IEnumerator FadeTo(CanvasGroup canvasGroup, float alphaFrom, float alphaTo, float time, bool autoDisable = false, bool autoDestroy = false)
        {
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += UnityEngine.Time.deltaTime / time;
                canvasGroup.alpha = Mathf.Lerp(alphaFrom, alphaTo, t);
                yield return null;
            }

            if (autoDestroy)
            {
                if (Application.isPlaying) Destroy(canvasGroup.gameObject);
            }
            else
            {
                if (autoDisable) canvasGroup.gameObject.SetActive(false);
            }
        }
    }
}