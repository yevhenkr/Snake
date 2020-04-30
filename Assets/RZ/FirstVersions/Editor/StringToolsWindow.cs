using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace RZ
{
    using UnityEditor;
    ////////// EDITOR WINDOW: //////////
    [Serializable]
    public class StringToolsWindow : EditorWindow
    {
        public const string windowName = "String Tools";
        public const string fullName = "RZ/" + windowName;
        public static Vector2 minimalSize = new Vector2(400, 200);
        public static string sourceText = "";
        public static string resultText = "";

        [MenuItem(fullName)]
        static void Initialize()
        {
            var me = EditorWindow.GetWindow(typeof(StringToolsWindow), false, windowName);
            me.minSize = minimalSize;
            me.Show();
        }

        protected void OnEnable()
        {
            // Load preferenses:
            var data = EditorPrefs.GetString(fullName, JsonUtility.ToJson(this, false));
            JsonUtility.FromJsonOverwrite(data, this);
        }

        protected void OnDisable()
        {
            // Save preferenses:
            var data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString(fullName, data);
        }

        Vector2 scrollOriginal;
        Vector2 scrollResult;
        string _sourceText;
        public void OnGUI()
        {
            // EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Source:", EditorStyles.boldLabel, GUILayout.Width(60));
            GUILayout.Label("Lenght: " + sourceText.Length, GUILayout.Width(80));
            GUILayout.Label("Spaces: " + (sourceText.Split(' ').Length - 1), GUILayout.Width(80));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();
            scrollOriginal = EditorGUILayout.BeginScrollView(scrollOriginal);

            sourceText = EditorGUILayout.TextArea(sourceText, GUILayout.ExpandHeight(true));
            // sourceText = EditorGUILayout.TextField(sourceText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Result:", EditorStyles.boldLabel, GUILayout.Width(60));
            GUILayout.Label("Lenght: " + resultText.Length, GUILayout.Width(80));
            GUILayout.Label("Spaces: " + (resultText.Split(' ').Length - 1), GUILayout.Width(80));
            EditorGUILayout.EndHorizontal();

            scrollResult = EditorGUILayout.BeginScrollView(scrollResult);
            resultText =
            EditorGUILayout.TextArea(resultText, GUILayout.ExpandHeight(true));
            // GUILayout.Label(resultText, GUILayout.ExpandHeight(true));
            // resultText = 
            // EditorGUILayout.TextField(resultText, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();


            if (_sourceText != sourceText) resultText = sourceText;
            _sourceText = sourceText;


            EditorGUILayout.BeginVertical(GUILayout.Width(150));
            if (GUILayout.Button("Clear"))
            {
                sourceText = "";
                resultText = "";
            }


            if (GUILayout.Button("Original"))
            { resultText = sourceText; }

            if (GUILayout.Button("To lower"))
            { resultText = resultText.ToLower(); }

            if (GUILayout.Button("To upper"))
            { resultText = resultText.ToUpper(); }

            if (GUILayout.Button("First to upper"))
            { resultText = StringTools.FirstCharToUpper(resultText); }

            if (GUILayout.Button("Every first to upper"))
            { resultText = StringTools.EveryFirstCharToUpper(resultText); }


            if (GUILayout.Button("Remove doublespaces"))
            { resultText = resultText.Replace("  ", " "); }


            if (GUILayout.Button("Copy")) { GUIUtility.systemCopyBuffer = resultText; }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            // EditorGUI.EndChangeCheck();
        }
    }
}