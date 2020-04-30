using System.Collections.Generic;

namespace RZ.Localizations
{
    // This contains data about each phrase, which is then translated into different languages
    [System.Serializable]
    public class Phrase
    {
        // The name of this phrase
        public string Name;

        // All the translations for this phrase
        public List<Translation> Translations = new List<Translation>();

        // Find the translation using this language, or return null
        public Translation FindTranslation(string language)
        {
            return Translations.Find(t => t.Language == language);
        }

        // Add a new translation to this phrase, or return the current one
        public Translation AddTranslation(string language)
        {
            var translation = FindTranslation(language);

            // Add it?
            if (translation == null)
            {
                translation = new Translation();
                translation.Language = language;
                Translations.Add(translation);
            }

            return translation;
        }
    }
}