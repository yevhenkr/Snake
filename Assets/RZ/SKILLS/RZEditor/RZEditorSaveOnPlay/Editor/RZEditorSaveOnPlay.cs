// 3.0

#if UNITY_EDITOR

namespace RZ
{

    using UnityEditor;

    [InitializeOnLoad]
    public static class RZEditorSaveOnPlay
    {
        static RZEditorSaveOnPlay()
        { EditorApplication.playModeStateChanged += AutoSaveWhenPlaymodeStarts; }


        public static bool enabled = true;


        private static void AutoSaveWhenPlaymodeStarts(PlayModeStateChange state)
        {
            if (!enabled) return;

            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                // Scene activeScene = EditorSceneManager.GetActiveScene();
                // EditorSceneManager.SaveScene(activeScene);
                // AssetDatabase.SaveAssets();
                // Debug.Log(activeScene.name + " was saved.");

                if (UnityEditor.SceneManagement.EditorSceneManager.
                    SaveCurrentModifiedScenesIfUserWantsTo())
                { AssetDatabase.SaveAssets(); }
            }
        }
    }

}

#endif