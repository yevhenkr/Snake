using UnityEngine;
using UnityEngine.UI;

namespace RZ.Localizations
{
    // This component will update a Text component with localized text, or use a fallback if none is found, and format the string with custom arguments
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class LocalizedTextArgs : LocalizedBehaviour
    {
        // [Tooltip("If PhraseName couldn't be found, this text will be used")]
        // [TextArea(3, 10)]
        // public string FallbackText;
        public bool toUpperCase = false;

        [System.NonSerialized]
        public object[] Args;

        public void SetArgs(object[] args)
        {
            if (Args != args)
            {
                Args = args;
                UpdateLocalization();
            }
        }

        // This gets called every time the translation needs updating
        public override void UpdateTranslation(Translation translation)
        {
            var text = GetComponent<Text>();
            string t;
            if (translation != null)
            {
                t = translation.Text;
            }
            else
            {
                t = PhraseName;
            }
            // Format string
            t = Args != null ? string.Format(t, Args) : t;
            text.text = toUpperCase ? t.ToUpper() : t;

            // var text = GetComponent<Text>();
            // string t;
            // if (translation != null)
            // {
            //     t = translation.Text;
            // }
            // else
            // {
            //     t = string.IsNullOrEmpty(FallbackText) ? PhraseName : FallbackText;
            // }
            // // Format string
            // t = Args != null ? string.Format(t, Args) : t;
            // text.text = toUpperCase ? t.ToUpper() : t;
        }

        // protected virtual void Awake()
        // {
        //     // Should we set FallbackText?
        //     if (string.IsNullOrEmpty(FallbackText) == true)
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