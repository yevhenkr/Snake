using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace RZ.Localizations
{
    [CustomPropertyDrawer(typeof(PhraseNameAttribute))]
    public class PhraseNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var left = position; left.xMax -= 40;
            var right = position; right.xMin = left.xMax + 2;

            EditorGUI.PropertyField(left, property);

            if (GUI.Button(right, "List") == true)
            {
                var menu = new GenericMenu();

                for (var i = 0; i < Localization.CurrentPhrases.Count; i++)
                {
                    var phraseName = Localization.CurrentPhrases[i];

                    menu.AddItem(new GUIContent(phraseName), property.stringValue == phraseName, () => { property.stringValue = phraseName; property.serializedObject.ApplyModifiedProperties(); });
                }

                if (menu.GetItemCount() > 0)
                {
                    menu.DropDown(right);
                }
                else
                {
                    Debug.LogWarning("Your scene doesn't contain any phrases, so the phrase name list couldn't be created.");
                }
            }
        }
    }
}
#endif

namespace RZ.Localizations
{
    // This attribute allows you to select a phrase from all the localizations in the scene
    public class PhraseNameAttribute : PropertyAttribute
    {
    }
    // This attribute allows you to select a phrases from all the localizations in the scene
    public class PhrasesNamesAttribute : PropertyAttribute
    {
    }
}