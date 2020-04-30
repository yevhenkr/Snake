using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RZ
{

    using UnityEditor;

    [Serializable]
    public class ScreenToolsWindow : EditorWindow
    {
        public const string windowName = "Screen Tools";
        public const string fullName = "RZ/" + windowName;

        public static Vector2 minimalSize = new Vector2(250, 250);



        public const string defaultShotsDirectory = "Screenshots";
        public const string defaultShotPrefix = "Shot";
        public const string extension = ".png";
        public const string DATE_TIME_FORMAT = "yyyyMMdd_HHmmss";

        [SerializeField] public string shotsDirectory = defaultShotsDirectory;
        [SerializeField] public string shotPrefix = defaultShotPrefix;
        [SerializeField] public string lastShot = null;

        [MenuItem(fullName)]
        static void Initialize()
        {
            var me = EditorWindow.GetWindow(typeof(ScreenToolsWindow), false, windowName);
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


        // Vector2 scrollPosition = Vector2.zero;
        int w1 = 100; int h1 = 18;
        int w2 = 16; int h2 = 16;
        string X = "X";

        public void OnGUI()
        {
            // scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(Screen.width), GUILayout.Height(Screen.height - 20));

            EditorGUILayout.LabelField("Screenshot", EditorStyles.boldLabel);


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Set resolution in:");
            if (GUILayout.Button("Game window", GUILayout.Width(w1))) ActivateEditorWindow(GAME_WINDOW);
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Separator();


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Shots directory", GUILayout.Width(w1), GUILayout.Height(h1));
            shotsDirectory = EditorGUILayout.TextField(shotsDirectory, GUILayout.Height(h1));
            if (GUILayout.Button(X, GUILayout.Width(w2), GUILayout.Height(h2))) shotsDirectory = defaultShotsDirectory;
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Shot prefix", GUILayout.Width(w1), GUILayout.Height(h1));
            shotPrefix = EditorGUILayout.TextField(shotPrefix, GUILayout.Height(h1));
            if (GUILayout.Button(X, GUILayout.Width(w2), GUILayout.Height(h2))) shotPrefix = defaultShotPrefix;
            EditorGUILayout.EndHorizontal();


            if (GUILayout.Button("Take screenshot")) TakeScreenshot();

            bool noShot = string.IsNullOrEmpty(lastShot);


            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Last shot:", EditorStyles.boldLabel);
            GUI.enabled = !noShot;
            if (GUILayout.Button(X, GUILayout.Width(w2), GUILayout.Height(h2))) { lastShot = null; }
            EditorGUILayout.EndHorizontal();


            var wrapLabel = EditorStyles.label;
            wrapLabel.wordWrap = true;
            EditorGUILayout.LabelField((noShot ? "No last shots" : lastShot), wrapLabel);


            EditorGUILayout.BeginHorizontal();
            bool dirExists = !noShot && Directory.Exists(Path.GetDirectoryName(lastShot));
            GUI.enabled = dirExists;
            if (GUILayout.Button("Open directory")) Application.OpenURL(Path.GetDirectoryName(lastShot));
            GUI.enabled = dirExists && File.Exists(lastShot);
            if (GUILayout.Button("Open file")) Application.OpenURL(lastShot);
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            // GUILayout.EndScrollView();
        }


        void TakeScreenshot()
        {
            ActivateEditorWindow(GAME_WINDOW);

            string d = string.IsNullOrEmpty(shotsDirectory) ?
                  Path.GetFullPath(".") : Path.GetFullPath(shotsDirectory);

            if (!Directory.Exists(d)) Directory.CreateDirectory(d);

            string extender = string.IsNullOrEmpty(shotPrefix) ? "" : "_";

            string dateTime = System.DateTime.Now.ToString(DATE_TIME_FORMAT);

            lastShot = Path.Combine(d, shotPrefix + extender + dateTime + extension);

            TakeScreenshot(lastShot);

            this.Focus();
        }

        public static void TakeScreenshot(string path)
        {
            ScreenCapture.CaptureScreenshot(path);
        }

        public const string GAME_WINDOW = "UnityEditor.GameView";
        public static void ActivateEditorWindow(string name)
        {
            System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
            Type type = assembly.GetType(name);
            EditorWindow.GetWindow(type);
            SceneView.FocusWindowIfItsOpen(type);
        }
    }
}