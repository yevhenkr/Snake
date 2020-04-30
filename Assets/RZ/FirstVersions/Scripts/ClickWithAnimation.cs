using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RZ
{
    // [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public class ClickWithAnimation : MonoBehaviourExtended, IPointerClickHandler
    // public class ClickWithAnimation : Selectable, IPointerClickHandler, IEventSystemHandler
    // , ISubmitHandler
    {

        public const string EffectImage = "EffectImage";

        public float max_Scale = 1.8f;
        public float scale_Speed = 2f;
        public float alpha_Speed = 4f;
        // public float disabled_alpha = 0.5f;
        public Image effectImage;
        public Graphic takeColorFrom;

        public bool interactable
        {
            get
            {
                Selectable s = gameObject.GetComponent<Selectable>();
                return s == null ? true : s.interactable;
            }

            set
            {
                Selectable s = gameObject.GetComponent<Selectable>();
                if (s != null) s.interactable = value;
            }
        }

        // public bool interactable = true;

        [HideInInspector]
        public bool IsPlaying = false;

        // [Serializable]
        // public class ButtonClickedEvent : UnityEngine.Events.UnityEvent { }
        // [FormerlySerializedAs("onClick")]
        // [SerializeField]
        // private ButtonClickedEvent onClick = new ButtonClickedEvent();

        public UnityEngine.Events.UnityEvent onClick = new UnityEngine.Events.UnityEvent();
        public UnityEngine.Events.UnityEvent onClickForced = new UnityEngine.Events.UnityEvent();



        // new void Awake()
        void Awake()
        {
            // base.Awake();
            // if (takeColorFrom != null) effectImage.color = takeColorFrom.color;
        }

        // void Start() { Prepare(); }
        void OnEnable() { Prepare(); }

        void Reset()
        {
            interactable = true;
            Transform t = transform.Find(EffectImage);
            if (t == null)
            {
                GameObject o = new GameObject(EffectImage);
                o.transform.parent = this.gameObject.transform;
                // ei.transform.SetSiblingIndex(0);

                effectImage = o.AddComponent<Image>();
                effectImage.raycastTarget = false;

                RectTransform rt = o.GetComponent<RectTransform>();
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 1);
                rt.anchoredPosition = new Vector2(0, 0);
                rt.sizeDelta = new Vector2(0, 0);
            }
            else
            {
                effectImage = t.GetComponent<Image>();
            }

            takeColorFrom = effectImage;

            Prepare();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (interactable)
            {
                onClickForced.Invoke();
                Prepare();
                IsPlaying = true;
            }
        }

        void Prepare()
        {
            if (takeColorFrom != null) effectImage.color = takeColorFrom.color;

            var alfa2 = effectImage.color;
            alfa2.a = 0f;
            effectImage.color = alfa2;
            aStep = 1;

            Vector3 res = effectImage.transform.localScale;
            res.x = 1;
            res.y = 1;
            effectImage.transform.localScale = res;

            aStep = 1f;
            sStep = 0f;

            IsPlaying = false;
        }


        float aStep = 1f;
        float sStep = 0f;
        void Update()
        {
            // if (IsPlaying)
            // {
            //     var tempColor = effectImage.color;
            //     alfa -= alpha_Step;
            //     tempColor.a = alfa;
            //     effectImage.color = tempColor;
            //     effectImage.transform.localScale += new Vector3(scale_Step, scale_Step, 0);
            //     if (effectImage.transform.localScale.x >= max_Scale)
            //     {
            //         Prepare();
            //         onClick.Invoke();
            //     }
            // }

            if (IsPlaying)
            {
                var tempColor = effectImage.color;
                aStep -= alpha_Speed * 1f * UnityEngine.Time.deltaTime;
                tempColor.a = Mathf.Lerp(0f, 1f, aStep);
                effectImage.color = tempColor;

                sStep += scale_Speed * max_Scale * UnityEngine.Time.deltaTime;
                float s = Mathf.Lerp(1f, max_Scale, sStep);
                effectImage.transform.localScale = new Vector3(s, s, 0);

                if (sStep > max_Scale)
                {
                    Prepare();
                    onClick.Invoke();
                }
            }
        }
    }
}
