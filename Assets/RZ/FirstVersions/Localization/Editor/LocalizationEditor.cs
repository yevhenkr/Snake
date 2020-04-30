using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace RZ.Localizations
{
    [CustomEditor(typeof(Localization))]
    public class LocalizationEditor : Editor
    {
        // Languages
        private static List<string> presetLanguages = new List<string>();

        // Currently expanded language
        private int languageIndex = -1;

        // Currently expanded language phrase
        private int reverseIndex = -1;

        // Currently expanded phrase
        private int phraseIndex = -1;

        // Currently expanded translation
        private int translationIndex = -1;

        private bool dirty;

        private List<string> existingLanguages = new List<string>();
        private List<string> existingPhrases = new List<string>();

        private GUIStyle labelStyle;
        private GUIStyle LabelStyle
        {
            get
            {
                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.clipping = TextClipping.Overflow;
                }
                return labelStyle;
            }
        }

        static LocalizationEditor()
        {
            presetLanguages.Add("English");
            presetLanguages.Add("Russian");
            presetLanguages.Add("Ukrainian");

            // presetLanguages.Add("Chinese");
            // presetLanguages.Add("Spanish");
            // presetLanguages.Add("Arabic");
            // presetLanguages.Add("German");
            // presetLanguages.Add("Korean");
            // presetLanguages.Add("French");
            // presetLanguages.Add("Japanese");
            // presetLanguages.Add("Italian");

            presetLanguages.Add("Other...");
        }

        [MenuItem("GameObject/UI/Localization", false, 1)]
        public static void CreateLocalization()
        {
            var gameObject = new GameObject(typeof(Localization).Name);
            UnityEditor.Undo.RegisterCreatedObjectUndo(gameObject, "Create Localization");
            gameObject.AddComponent<Localization>();
            Selection.activeGameObject = gameObject;
        }


        // private bool foldoutLangList = false;                // LISTS FOLDOUT
        // private bool foldoutPhrasesList = false;             // LISTS FOLDOUT
        // private GUIStyle myFoldoutStyle;                     // LISTS FOLDOUT

        // Draw the whole inspector
        public override void OnInspectorGUI()
        {
            // myFoldoutStyle = new GUIStyle(EditorStyles.foldout);     // LISTS FOLDOUT
            // myFoldoutStyle.fontStyle = FontStyle.Bold;

            var localization = (Localization)target;
            Validate(localization);

            DrawCurrentLanguage();
            DrawDefaultLanguage(localization);

            EditorGUILayout.Separator();
            DrawSaveLanguage(localization);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("DetectLanguage"));
            EditorGUILayout.Separator();

            DrawFindMissed(localization);
            EditorGUILayout.Separator();

            DrawLanguages(localization);
            EditorGUILayout.Separator();

            DrawPhrases(localization);

            // Update if dirty?
            if (serializedObject.ApplyModifiedProperties() == true || dirty == true)
            {
                dirty = false;
                Localization.UpdateTranslations();
                SetDirty(localization);
            }
        }

        private void DrawCurrentLanguage()
        {
            var labelA = Reserve();
            var valueA = Reserve(ref labelA, 50);

            Localization.CurrentLanguage = EditorGUI.TextField(labelA, "Current Language", Localization.CurrentLanguage);

            if (GUI.Button(valueA, "List"))
            {
                var menu = new GenericMenu();

                for (var i = 0; i < Localization.CurrentLanguages.Count; i++)
                {
                    var language = Localization.CurrentLanguages[i];

                    menu.AddItem(new GUIContent(language), Localization.CurrentLanguage == language, () => { Localization.CurrentLanguage = language; MarkAsModified(); });
                }

                if (menu.GetItemCount() > 0)
                {
                    menu.DropDown(valueA);
                }
                else
                {
                    Debug.LogWarning("Your scene doesn't contain any languages, so the language name list couldn't be created.");
                }
            }
        }


        private void DrawDefaultLanguage(Localization localization)
        {
            var labelA = Reserve();
            var valueA = Reserve(ref labelA, 50);

            localization.DefaultLanguage = EditorGUI.TextField(labelA, "Default Language", localization.DefaultLanguage);

            if (GUI.Button(valueA, "List"))
            {
                var menu = new GenericMenu();

                for (var i = 0; i < Localization.CurrentLanguages.Count; i++)
                {
                    var language = Localization.CurrentLanguages[i];
                    menu.AddItem(new GUIContent(language), localization.DefaultLanguage == language, () => { localization.DefaultLanguage = language; MarkAsModified(); });
                }

                if (menu.GetItemCount() > 0)
                {
                    menu.DropDown(valueA);
                }
                else
                {
                    Debug.LogWarning("Your scene doesn't contain any languages, so the language name list couldn't be created.");
                }
            }
        }

        private void DrawSaveLanguage(Localization localization)
        {
            var labelA = Reserve();
            var valueA = Reserve(ref labelA, 50);

            localization.SaveLanguage = EditorGUI.Toggle(labelA, "Save Language", localization.SaveLanguage);

            if (GUI.Button(valueA, "Clear"))
            {
                localization.SaveLanguage = false;
                localization.ClearSave();
            }
        }

        private void DrawFindMissed(Localization localization)
        {
            if (GUILayout.Button("Find missed translations"))
            {
                int missedCount = 0;
                for (int i = 0; i < localization.Languages.Count; i++)
                {
                    string language = localization.Languages[i];
                    for (int j = 0; j < localization.Phrases.Count; j++)
                    {
                        var phrase = localization.Phrases[j];
                        var translation = phrase.Translations.Find(t => t.Language == language);
                        if (translation == null)
                        {
                            missedCount++;
                            Debug.LogWarning("Missed translation for: " + "\"" + phrase.Name + "\"" + " - " + language);
                        }
                    }
                }
                Debug.LogWarning("Missed translations count: " + missedCount);
            }
        }


        private void DrawLanguages(Localization localization)
        {
            EditorGUI.LabelField(Reserve(), "Languages:", EditorStyles.boldLabel);

            // foldoutLangList = EditorGUILayout.Foldout(foldoutLangList, "Languages:", myFoldoutStyle, true);    // LISTS FOLDOUT
            // if (foldoutLangList)                                                                         // LISTS FOLDOUT
            // {                                                                                            // LISTS FOLDOUT

            existingLanguages.Clear();

            // Draw all added languages
            for (var i = 0; i < localization.Languages.Count; i++)
            {
                var language = localization.Languages[i];
                var labelB = Reserve();
                var valueB = Reserve(ref labelB, 20.0f);

                // Edit language name or remove
                if (languageIndex == i)
                {
                    BeginModifications();
                    {
                        localization.Languages[i] = EditorGUI.TextField(labelB, language);
                    }
                    EndModifications();

                    if (GUI.Button(valueB, "X"))
                    {
                        MarkAsModified();
                        localization.Languages.RemoveAt(i); languageIndex = -1;
                    }
                }

                // Expand language?
                if (EditorGUI.Foldout(labelB, languageIndex == i, languageIndex == i ? "" : language, true))
                {
                    if (languageIndex != i)
                    {
                        languageIndex = i;
                        reverseIndex = -1;
                    }

                    EditorGUI.indentLevel += 1;
                    {
                        DrawReverse(localization, language);
                    }
                    EditorGUI.indentLevel -= 1;

                    EditorGUILayout.Separator();
                }
                else if (languageIndex == i)
                {
                    languageIndex = -1;
                    reverseIndex = -1;
                }

                // Already added?
                if (existingLanguages.Contains(language))
                {
                    EditorGUILayout.HelpBox("This language already exists in the list!", MessageType.Warning);
                }
                else
                {
                    existingLanguages.Add(language);
                }
            }
            // }    // LISTS FOLDOUT

            var labelC = Reserve(20.0f);
            var valueC = Reserve(ref labelC, 100f);
            if (GUI.Button(valueC, "Add language") == true)
            {
                // foldoutLangList = true;  // LISTS FOLDOUT

                MarkAsModified();
                var menu = new GenericMenu();
                for (var i = 0; i < presetLanguages.Count; i++)
                {
                    var presetLanguage = presetLanguages[i];
                    menu.AddItem(new GUIContent(presetLanguage), false, () => localization.AddLanguage(presetLanguage));
                }
                menu.DropDown(valueC);
            }

        }

        // Reverse lookup the phrases for this language
        private void DrawReverse(Localization localization, string language)
        {
            for (var i = 0; i < localization.Phrases.Count; i++)
            {
                var phrase = localization.Phrases[i];
                var labelA = Reserve();

                var translation = phrase.Translations.Find(t => t.Language == language);

                BeginModifications();
                {
                    EditorGUI.LabelField(labelA, phrase.Name);
                }
                EndModifications();

                if (translation != null)
                {
                    if (reverseIndex == i)
                    {
                        BeginModifications();
                        {
                            phrase.Name = EditorGUI.TextField(labelA, "", phrase.Name);
                        }
                        EndModifications();
                    }

                    if (EditorGUI.Foldout(labelA, reverseIndex == i, reverseIndex == i ? "" : phrase.Name, true) == true)
                    {
                        reverseIndex = i;
                        EditorGUI.indentLevel += 1;
                        {
                            DrawTranslation(translation);
                        }
                        EditorGUI.indentLevel -= 1;
                        EditorGUILayout.Separator();
                    }
                    else if (reverseIndex == i)
                    {
                        reverseIndex = -1;
                    }
                }
                else
                {
                    var valueA = Reserve(ref labelA, 120.0f);

                    if (GUI.Button(valueA, "Create Translation") == true)
                    {
                        MarkAsModified();
                        var newTranslation = new Translation();
                        newTranslation.Language = language;
                        newTranslation.Text = phrase.Name;
                        phrase.Translations.Add(newTranslation);

                    }
                }
            }
        }

        private void DrawPhrases(Localization localization)
        {
            EditorGUI.LabelField(Reserve(), "Phrases:", EditorStyles.boldLabel);

            // foldoutPhrasesList = EditorGUILayout.Foldout(foldoutPhrasesList, "Phrases:", myFoldoutStyle, true);       // LISTS FOLDOUT
            // if (foldoutPhrasesList)                                                                             // LISTS FOLDOUT
            // {                                                                                                   // LISTS FOLDOUT
            existingPhrases.Clear();

            for (var i = 0; i < localization.Phrases.Count; i++)
            {
                var phrase = localization.Phrases[i];
                var labelB = Reserve();
                var valueB = Reserve(ref labelB, 20.0f);

                if (phraseIndex == i)
                {
                    BeginModifications();
                    {
                        phrase.Name = EditorGUI.TextField(labelB, "", phrase.Name);
                    }
                    EndModifications();

                    if (GUI.Button(valueB, "X") == true)
                    {
                        MarkAsModified();
                        localization.Phrases.RemoveAt(i); phraseIndex = -1;
                    }
                }

                if (EditorGUI.Foldout(labelB, phraseIndex == i, phraseIndex == i ? "" : phrase.Name, true) == true)
                {
                    if (phraseIndex != i)
                    {
                        phraseIndex = i;
                        translationIndex = -1;
                    }

                    EditorGUI.indentLevel += 1;
                    {
                        DrawTranslations(localization, phrase);
                    }
                    EditorGUI.indentLevel -= 1;

                    EditorGUILayout.Separator();
                }
                else if (phraseIndex == i)
                {
                    phraseIndex = -1;
                    translationIndex = -1;
                }

                if (existingPhrases.Contains(phrase.Name) == true)
                {
                    ////////// RZ FIX FIND: //////////
                    EditorGUILayout.HelpBox("This phrase already exists in the list!", MessageType.Warning);
                    if (GUILayout.Button("Find (move here)") == true)
                    {
                        MarkAsModified();
                        int findedIndex = existingPhrases.IndexOf(phrase.Name);
                        localization.Phrases[i] = localization.Phrases[findedIndex];
                        localization.Phrases.RemoveAt(findedIndex);
                        phraseIndex = i - 1;
                        // break;
                    }
                }
                else
                {
                    existingPhrases.Add(phrase.Name);
                }
            }
            // }   // LISTS FOLDOUT

            // Add a new phrase?
            var labelC = Reserve(20.0f);
            var valueC = Reserve(ref labelC, 100f);
            if (GUI.Button(valueC, "Add phrase") == true)
            {
                // foldoutPhrasesList = true;  // LISTS FOLDOUT

                MarkAsModified();
                var newPhrase = localization.AddPhrase("New Phrase");
                phraseIndex = localization.Phrases.IndexOf(newPhrase);
            }
        }

        private void DrawTranslations(Localization localization, Phrase phrase)
        {
            existingLanguages.Clear();

            for (var i = 0; i < phrase.Translations.Count; i++)
            {
                var labelA = Reserve();
                var valueA = Reserve(ref labelA, 20.0f);
                var translation = phrase.Translations[i];

                if (translationIndex == i)
                {
                    BeginModifications();
                    {
                        translation.Language = EditorGUI.TextField(labelA, "", translation.Language);
                    }
                    EndModifications();

                    if (GUI.Button(valueA, "X") == true)
                    {
                        MarkAsModified();

                        phrase.Translations.RemoveAt(i); translationIndex = -1;
                    }
                }

                if (EditorGUI.Foldout(labelA, translationIndex == i, translationIndex == i ? "" : translation.Language, true) == true)
                {
                    translationIndex = i;

                    EditorGUI.indentLevel += 1;
                    {
                        DrawTranslation(translation);
                    }
                    EditorGUI.indentLevel -= 1;

                    EditorGUILayout.Separator();
                }
                else if (translationIndex == i)
                {
                    translationIndex = -1;
                }

                if (existingLanguages.Contains(translation.Language) == true)
                {
                    EditorGUILayout.HelpBox("This phrase has already been translated to this language!", MessageType.Warning);
                }
                else
                {
                    existingLanguages.Add(translation.Language);
                }

                if (localization.Languages.Contains(translation.Language) == false)
                {
                    EditorGUILayout.HelpBox("This translation uses a language that hasn't been set in the localization!", MessageType.Warning);
                }
            }

            for (var i = 0; i < localization.Languages.Count; i++)
            {
                var language = localization.Languages[i];

                if (phrase.Translations.Exists(t => t.Language == language) == false)
                {
                    var labelA = Reserve();
                    var valueA = Reserve(ref labelA, 120.0f);

                    EditorGUI.LabelField(labelA, language);

                    if (GUI.Button(valueA, "Create Translation") == true)
                    {
                        MarkAsModified();

                        var newTranslation = new Translation();

                        newTranslation.Language = language;
                        newTranslation.Text = phrase.Name;

                        translationIndex = phrase.Translations.Count;

                        phrase.Translations.Add(newTranslation);
                    }
                }
            }
        }

        private void DrawTranslation(Translation translation)
        {
            BeginModifications();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Text", LabelStyle, GUILayout.Width(50.0f));

                    translation.Text = EditorGUILayout.TextArea(translation.Text);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Object", LabelStyle, GUILayout.Width(50.0f));

                    translation.Object = EditorGUILayout.ObjectField(translation.Object, typeof(Object), true);
                }
                EditorGUILayout.EndHorizontal();
            }
            EndModifications();
        }

        private static Rect Reserve(ref Rect rect, float rightWidth = 0.0f, float padding = 2.0f)
        {
            if (rightWidth == 0.0f)
            {
                rightWidth = rect.width - EditorGUIUtility.labelWidth;
            }

            var left = rect; left.xMax -= rightWidth;
            var right = rect; right.xMin = left.xMax;

            left.xMax -= padding;

            rect = left;

            return right;
        }

        private static Rect Reserve(float height = 16.0f)
        {
            var rect = EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("", GUILayout.Height(height));
            }
            EditorGUILayout.EndVertical();
            return rect;
        }

        private static void SetDirty(Object target)
        {
            EditorUtility.SetDirty(target);

#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
			EditorApplication.MarkSceneDirty();
#else
            UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
#endif
        }

        private void MarkAsModified()
        {
            dirty = true;
        }

        private void BeginModifications()
        {
            EditorGUI.BeginChangeCheck();
        }

        private void EndModifications()
        {
            if (EditorGUI.EndChangeCheck()) dirty = true;
        }

        private static void Validate(Localization localization)
        {
            if (localization.Languages == null) localization.Languages = new List<string>();
            if (localization.Phrases == null) localization.Phrases = new List<Phrase>();

            for (var i = localization.Phrases.Count - 1; i >= 0; i--)
            {
                var phrase = localization.Phrases[i];
                if (phrase.Translations == null) phrase.Translations = new List<Translation>();
            }
        }
    }
}