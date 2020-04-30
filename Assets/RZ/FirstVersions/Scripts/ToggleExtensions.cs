using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RZ
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Toggle))]
    public class ToggleExtensions : MonoBehaviour
    {

        public Toggle toggle;

        public Image background;
        // public Color backColor = Color.white;

        public Image checkmark;
        public enum CheckMode
        {
            ShowHide,
            MirrorX,
            MirrorY,
            MirrorXY
        };


        public CheckMode checkMode = CheckMode.ShowHide;
        public bool changeCheckColor = false;
        public Color checkOnColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        public Color checkOffColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        public Color checkDisabledColor = new Color(0.7f, 0.7f, 0.7f, 0.5f);
        public Text label;
        public bool changeLabelColor = false;
        public Color labelOnColor = Color.black;
        public Color labelOffColor = Color.gray;
        public Color labelDisabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        void Reset()
        {
            if (toggle == null) toggle = GetComponent<Toggle>();

            Image[] t = GetComponentsInChildren<Image>();
            if (background == null) background = t[0];
            if (checkmark == null) checkmark = t[1];

            if (label == null) label = GetComponentInChildren<Text>();
        }

        void Awake()
        {
            ChangeFull();
        }

        CheckMode _checkMode;
        bool _interactable;
        bool _isOn;
        void Update()
        {
            if (_checkMode != checkMode)
            {
                ChangeFull();
            }
            else
            {
                if (_interactable != toggle.interactable || _isOn != toggle.isOn)
                    Change();
            }
            // _checkMode = checkMode;
        }


        Vector2 onVector = new Vector2(1, 1);
        Vector2 offVector = new Vector2(1, 1);
        void ChangeFull()
        {
            _checkMode = checkMode;
            switch (checkMode)
            {
                default:
                case CheckMode.ShowHide:
                    toggle.graphic = checkmark;
                    toggle.targetGraphic = background;
                    offVector = new Vector2(1, 1);
                    break;

                case CheckMode.MirrorX:
                    toggle.graphic = null;
                    toggle.targetGraphic = checkmark;
                    offVector = new Vector2(-1, 1);
                    break;

                case CheckMode.MirrorY:
                    toggle.graphic = null;
                    toggle.targetGraphic = checkmark;
                    offVector = new Vector2(1, -1);
                    break;

                case CheckMode.MirrorXY:
                    toggle.graphic = null;
                    toggle.targetGraphic = checkmark;
                    offVector = new Vector2(-1, -1);
                    break;
            }
            Change();
        }

        void Change()
        {
            _interactable = toggle.interactable;
            _isOn = toggle.isOn;

            if (changeCheckColor)
            {
                if (toggle.interactable)
                {
                    checkmark.color = toggle.isOn ? checkOnColor : checkOffColor;
                }
                else
                {
                    checkmark.color = checkDisabledColor;
                }
            }

            if (label != null && changeLabelColor)
            {
                if (toggle.interactable)
                {
                    label.color = toggle.isOn ? labelOnColor : labelOffColor;
                }
                else
                {
                    label.color = labelDisabledColor;
                }
            }

            if (checkMode != CheckMode.ShowHide)
            {
                checkmark.transform.localScale = toggle.isOn ? onVector : offVector;
            }
            else
            {
                checkmark.transform.localScale = onVector;
                toggle.enabled = false;
                toggle.enabled = true;
            }
        }
    }
}