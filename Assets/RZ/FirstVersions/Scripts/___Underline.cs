using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RZ
{

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(___Underline))]
    public class LocalizationComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ___Underline u = (___Underline)target;
            DrawDefaultInspector();
            if (!u.underlineAllText)
            {
                u.underlineStart = EditorGUILayout.IntField("Underline start", u.underlineStart);
                u.underlineEnd = EditorGUILayout.IntField("Underline end", u.underlineEnd);
            }
        }
    }
#endif


    [ExecuteInEditMode]
    public class ___Underline : MonoBehaviour
    {
        public bool drawUnderline = true;
        public bool underlineAllText = true;
        [HideInInspector]
        public int underlineStart = 0;
        [HideInInspector]
        public int underlineEnd = 0;





        private bool _underlineAllText = true;
        private int _underlineStart = 0;
        private int _underlineEnd = 0;
        private int _textLength = 0;
        private Text text = null;
        private RectTransform textRectTransform = null;
        private TextGenerator textGenerator = null;

        private GameObject lineGameObject = null;
        private Image lineImage = null;
        private RectTransform lineRectTransform = null;

        void Start()
        {
            text = gameObject.GetComponent<Text>();
            textRectTransform = gameObject.GetComponent<RectTransform>();
            textGenerator = text.cachedTextGenerator;
            lineGameObject = new GameObject("Underline");
            lineImage = lineGameObject.AddComponent<Image>();
            lineImage.color = text.color;
            lineRectTransform = lineGameObject.GetComponent<RectTransform>();
            lineRectTransform.SetParent(transform, false);
            lineRectTransform.anchorMin = textRectTransform.pivot;
            lineRectTransform.anchorMax = textRectTransform.pivot;
        }

        void Update()
        {
            if (drawUnderline)
            {
                DrawLine();
                lineImage.enabled = true;
            }
            else
            {
                lineImage.enabled = false;
                return;
            }
        }

        private void DrawLine()
        {
            if (text.text.Length <= 0)
                return;

            if (_textLength == text.text.Length
            && (_underlineAllText == underlineAllText
            || (_underlineStart == underlineStart
            && _underlineEnd == underlineEnd)))
                return;

            _textLength = text.text.Length;
            _underlineAllText = underlineAllText;

            if (_underlineAllText)
            {
                _underlineStart = 0;
                _underlineEnd = text.text.Length;
            }
            else
            {
                _underlineStart = Mathf.Max(0, underlineStart);
                _underlineEnd = Mathf.Min(_textLength, underlineEnd);
            }

            UICharInfo[] charactersInfo = textGenerator.GetCharactersArray();
            UILineInfo[] linesInfo = textGenerator.GetLinesArray();
            // if (!(_underlineEnd > underlineStart && underlineEnd < charactersInfo.Length))
            // // if (_underlineEnd < _underlineStart || _underlineStart > _underlineEnd))
            //    return;

            // if(linesInfo.Length<1)
            if (text.text.Length <= 0 || linesInfo.Length <= 0)
                return;

            Debug.Log("DRAWLINE");

            Canvas canvas = gameObject.GetComponentInParent<Canvas>();
            float factor = 1.0f / canvas.scaleFactor;
            float height = linesInfo[0].height;

            lineRectTransform.anchoredPosition = new Vector2(
                factor * (charactersInfo[_underlineStart].cursorPos.x + charactersInfo[_underlineEnd].cursorPos.x) / 2.0f,
                factor * (charactersInfo[_underlineStart].cursorPos.y - height / 1.0f)
                );

            lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, factor * Mathf.Abs(charactersInfo[_underlineStart].cursorPos.x - charactersInfo[_underlineEnd].cursorPos.x));
            lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height / 10.0f);
        }
    }
}

