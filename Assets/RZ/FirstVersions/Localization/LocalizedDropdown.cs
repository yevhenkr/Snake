using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RZ.Localizations
{
    using System.Linq;
#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(LocalizedDropdown))]
    public class LocalizedDropdownEditor : Editor
    {
        string[] phrases = { };

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LocalizedDropdown me = (LocalizedDropdown)target;

            string[] _phrases = phrases;
            phrases = me.Phrases.ToArray();
            if (!phrases.SequenceEqual(_phrases))
            {
                me.UpdateTranslation();
            }
        }
    }
#endif

    // This component will update a Dropdown component with localized text, or use a fallback if none is found
    // The PhraseName and FallbackText get from Dropdown Options
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Dropdown))]
    public class LocalizedDropdown : LocalizedBehaviour
    {
        public static char separator = '|';
        public string EmptyPhrase { get { return PhraseName; } set { PhraseName = value; } }
        public Dropdown dropdown;
        public List<string> Phrases;

        void Awake()
        {
            dropdown = gameObject.GetComponent<Dropdown>();
        }

        void Reset()
        {
            PhraseName = "Empty list";
            dropdown = GetComponent<Dropdown>();
            UpdateTranslation();
        }

        public int Count { get { return Phrases.Count; } }

        public void Clear()
        {
            dropdown.options.Clear();
            Phrases.Clear();
        }

        public void Add(string phrase)
        {
            Phrases.Add(phrase);
            AddToOptions(phrase);
        }

        public void AddRange(string[] phrases)
        {
            for (int i = 0; i < phrases.Length; i++)
            {
                Phrases.Add(phrases[i]);
                AddToOptions(phrases[i]);
            }
        }

        // public void Select(string phrase)
        public void Select(string phrase)
        {
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (Phrases[i] == phrase)
                {
                    dropdown.value = i;
                    dropdown.RefreshShownValue();
                }
            }
        }

        public string SelectedPhrase
        {
            get { return Phrases.Count == 0 ? EmptyPhrase : Phrases[dropdown.value]; }
        }

        void AddToOptions(string phrase)
        {
            dropdown.options.Add(new Dropdown.OptionData(Localization.Translate(phrase)));
        }

        public override void UpdateTranslation(Translation translation = null)
        {
            if (dropdown != null)
            {
                dropdown.options.Clear();
                if (Phrases.Count > 0)
                {
                    for (int i = 0; i < Phrases.Count; i++)
                    {
                        AddToOptions(Phrases[i]);
                    }
                    dropdown.RefreshShownValue();
                }
                else
                {
                    dropdown.captionText.text = Localization.Translate(EmptyPhrase);
                }
            }
        }
    }
}