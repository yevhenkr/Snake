using UnityEngine;

namespace RZ.Localizations
{
    // This contains data about the phrases after it's been translated to a specific language
    [System.Serializable]
    public class Translation
    {
        // The language of this translation
        public string Language;

        // The translated text
        public string Text;

        // The translated object (e.g. language specific texture)
        public Object Object;
    }
}