
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RZ.Localizations
{
    using UnityEditor;
    // This component will update a Text component with localized text, or use a fallback if none is found
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class LocalizedText : LocalizedBehaviour
    {
        // [Tooltip("If PhraseName couldn't be found, this text will be used")]
        // [TextArea(3, 10)]
        // public string FallbackText;
        public bool toUpperCase = false;

        // This gets called every time the translation needs updating
        public override void UpdateTranslation(Translation translation)
        {
            SetText(translation != null ? translation.Text : PhraseName);
        }

        public virtual void SetText(string t)
        {
            var text = GetComponent<Text>();
            text.text = toUpperCase ? t.ToUpper() : t;
        }

        // protected virtual void Awake()
        // {
        //     // Should we set FallbackText?
        //     if (string.IsNullOrEmpty(FallbackText))
        //     {
        //         // Get the Text component attached to this GameObject
        //         var text = GetComponent<Text>();

        //         // Copy current text to fallback
        //         FallbackText = text.text;
        //     }
        // }

        protected virtual void Awake()
        {
            // Should we set Phrase?
            if (string.IsNullOrEmpty(PhraseName))
            {
                // Get the Text component attached to this GameObject
                var text = GetComponent<Text>();

                // Copy current text to phrase
                PhraseName = text.text;
            }
        }
    }
}