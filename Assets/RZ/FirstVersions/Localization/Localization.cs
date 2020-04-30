using UnityEngine;
using System.Collections.Generic;

namespace RZ.Localizations
{
    // This component stores a list of phrases, their translations, as well as manage a global list of translations for easy access
    [ExecuteInEditMode]
    public class Localization : MonoBehaviour
    {
        // If the application is started and no language has been loaded or auto detected, this language will be used
        // [LanguageName]
        // public string DefaultLanguage;
        // [HideInInspector]
        public string DefaultLanguage;

        // Automatically save/load the CurrentLanguage selection to PlayerPrefs. (can be cleared with ClearSave context menu option)
        [HideInInspector]
        public bool SaveLanguage = true;

        // Try to detect the user's device language
        public bool DetectLanguage = true;

        // The list of languages defined by this localization
        public List<string> Languages;

        // The list of phrases defined by this localization
        public List<Phrase> Phrases;

        // Called when the language or translations change
        public static System.Action OnLocalizationChanged;

        // All active and enabled Localization components
        public static List<Localization> AllLocalizations = new List<Localization>();

        // The currently set language
        private static string currentLanguage;

        // The list of currently supported languages
        private static List<string> currentLanguages = new List<string>();

        // The list of currently supported phrases
        private static List<string> currentPhrases = new List<string>();

        // Dictionary of all the phrase names mapped to their current translations
        private static Dictionary<string, Translation> currentTranslations = new Dictionary<string, Translation>();

        // The list of languages that you can currently switch between
        public static List<string> CurrentLanguages
        {
            get { return currentLanguages; }
        }

        // The list of languages that you can currently switch between
        public static List<string> CurrentPhrases
        {
            get { return currentPhrases; }
        }

        // Does at least one localization have 'SaveLanguage' set?
        public static bool CurrentSaveLanguage
        {
            get
            {
                for (var i = AllLocalizations.Count - 1; i >= 0; i--)
                {
                    if (AllLocalizations[i].SaveLanguage == true)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        // Change the current language of this instance?
        public static string CurrentLanguage
        {
            set
            {
                if (currentLanguage != value)
                {
                    currentLanguage = value;
                    UpdateTranslations();
                    if (CurrentSaveLanguage) PlayerPrefs.SetString("Localization.CurrentLanguage", value);
                }
            }

            get { return currentLanguage; }
        }

        [ContextMenu("Clear Save")]
        public void ClearSave()
        {
            PlayerPrefs.DeleteKey("Localization.CurrentLanguage");
        }

        // Sets the 'CurrentLanguage' property, this is useful for GUI events
        public void SetCurrentLanguage(string newLanguage)
        {
            CurrentLanguage = newLanguage;
        }

        // Get the current translation for this phrase, or return null
        public static Translation GetTranslation(string phraseName)
        {
            var translation = default(Translation);
            if (currentTranslations != null && phraseName != null)
            {
                currentTranslations.TryGetValue(phraseName, out translation);
            }
            return translation;
        }

        // Get the current text for this phrase, or return null
        public static string GetTranslationText(string phraseName)
        {
            var translation = GetTranslation(phraseName);
            if (translation != null)
            {
                return translation.Text;
            }
            return null;
        }

        // Get the current Object for this phrase, or return null
        public static Object GetTranslationObject(string phraseName)
        {
            var translation = GetTranslation(phraseName);

            if (translation != null)
            {
                return translation.Object;
            }
            return null;
        }

        // Add a new language to this localization
        public void AddLanguage(string language)
        {
            if (Languages == null) Languages = new List<string>();

            // Add language to languages list?
            if (Languages.Contains(language) == false)
            {
                Languages.Add(language);
            }
        }

        // Add a new phrase to this localization, or return the current one
        public Phrase AddPhrase(string phraseName)
        {
            if (Phrases == null) Phrases = new List<Phrase>();

            var phrase = Phrases.Find(p => p.Name == phraseName);

            if (phrase == null)
            {
                phrase = new Phrase();

                phrase.Name = phraseName;

                Phrases.Add(phrase);
            }

            return phrase;
        }

        // Add a new translation to this localization, or return the current one
        public Translation AddTranslation(string language, string phraseName)
        {
            AddLanguage(language);
            return AddPhrase(phraseName).AddTranslation(language);
        }

        // This rebuilds the dictionary used to quickly map phrase names to translations for the current language
        public static void UpdateTranslations()
        {
            currentTranslations.Clear();
            currentLanguages.Clear();
            currentPhrases.Clear();

            // Go through all enabled localizations
            for (var i = 0; i < AllLocalizations.Count; i++)
            {
                var localization = AllLocalizations[i];

                // Add all phrases to currentTranslations
                if (localization.Phrases != null)
                {
                    for (var j = localization.Phrases.Count - 1; j >= 0; j--)
                    {
                        var phrase = localization.Phrases[j];
                        var phraseName = phrase.Name;

                        if (currentPhrases.Contains(phraseName) == false)
                        {
                            currentPhrases.Add(phraseName);
                        }

                        // Make sure this phrase hasn't already been added
                        if (currentTranslations.ContainsKey(phraseName) == false)
                        {
                            // Find the translation for this phrase
                            var translation = phrase.FindTranslation(currentLanguage);

                            // If it exists, add it
                            if (translation != null)
                            {
                                currentTranslations.Add(phraseName, translation);
                            }
                        }
                    }
                }

                // Add all languages to currentLanguages
                if (localization.Languages != null)
                {
                    for (var j = localization.Languages.Count - 1; j >= 0; j--)
                    {
                        var language = localization.Languages[j];

                        if (currentLanguages.Contains(language) == false)
                        {
                            currentLanguages.Add(language);
                        }
                    }
                }
            }

            // Notify changes?
            if (OnLocalizationChanged != null)
            {
                OnLocalizationChanged();
            }
        }

        // Set the instance, merge old instance, and update translations
        protected virtual void OnEnable()
        {
            AllLocalizations.Add(this);

            // Load saved language?
            if (string.IsNullOrEmpty(currentLanguage) && SaveLanguage)
                currentLanguage = PlayerPrefs.GetString("Localization.CurrentLanguage");

            // Detect system language?
            if (string.IsNullOrEmpty(currentLanguage) && DetectLanguage)
                currentLanguage = Application.systemLanguage.ToString();

            // Use default language?
            if (string.IsNullOrEmpty(currentLanguage))
                currentLanguage = DefaultLanguage;

            UpdateTranslations();
        }

        // Unset instance?
        protected virtual void OnDisable()
        {
            AllLocalizations.Remove(this);
            UpdateTranslations();
        }
#if UNITY_EDITOR
        // Inspector modified?
        protected virtual void OnValidate()
        {
            UpdateTranslations();
        }
#endif

        // The TextArgs component actually supports for placeholders within phrases.
        //  To enable it from script, use this wrapper.
        public static string Translate(string phraseName)
        {
            var translation = GetTranslation(phraseName);
            if (translation != null && !string.IsNullOrEmpty(translation.Text))
            {
                return translation.Text;
            }
            return phraseName;
        }

        public static string Translate(string phraseName, string parameter)
        {
            return string.Format(Translate(phraseName), parameter);
        }

        public static string Translate(string phraseName, string[] parameters)
        {
            return string.Format(Translate(phraseName), parameters);
        }


    }
}