using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace RZ.Localizations
{
    [CustomEditor(typeof(LoaderExporter))]
    public class LoaderExporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var t = (LoaderExporter)target;

            DrawDefaultInspector();
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("Target"));
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("Source"));

            // GUILayout.BeginHorizontal();
            // {
            //     EditorGUILayout.TextField("Source Language", t.SourceLanguage);
            //     // EditorGUILayout.PropertyField(serializedObject.FindProperty("SourceLanguage"));

            if (GUILayout.Button("Languages List"))
            {
                var menu = new GenericMenu();

                for (var i = 0; i < Localization.CurrentLanguages.Count; i++)
                {
                    var language = Localization.CurrentLanguages[i];
                    menu.AddItem(new GUIContent(language), t.SourceLanguage == language, () => { t.SourceLanguage = language; });
                }

                if (menu.GetItemCount() > 0)
                {
                    menu.ShowAsContext();
                }
                else
                {
                    Debug.LogWarning("Your scene doesn't contain any languages, so the language name list couldn't be created.");
                }
            }
            // }
            // GUILayout.EndHorizontal();

            // EditorGUILayout.PropertyField(serializedObject.FindProperty("Separator"));
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("NewLine"));
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("Comment"));


            if (GUILayout.Button("Load From Source")) t.LoadFromSource();

#if !UNITY_WEBPLAYER
            if (GUILayout.Button("Export Text Asset")) t.ExportTextAsset();
#endif
        }
    }
}
#endif