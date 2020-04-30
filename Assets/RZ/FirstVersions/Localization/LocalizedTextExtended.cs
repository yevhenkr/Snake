
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
    public class LocalizedTextExtended : LocalizedText
    {
        public string prefix = "";
        public string suffix = "";

        public override void SetText(string t)
        {
            var text = GetComponent<Text>();
            text.text = prefix + (toUpperCase ? t.ToUpper() : t) + suffix;
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

    }
}