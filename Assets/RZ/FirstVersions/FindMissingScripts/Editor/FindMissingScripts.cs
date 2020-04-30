// Usage:
//
// In the editor, a script that has been assigned to an object, but subsequently deleted has the string "Missing (Mono Script)"
// where the script class/filename should be.
// It is possible to search a project to find all missing scripts using this editor script.
// To use it, save the file as "assets/editor/FindMissingScripts.cs".
// Note that it is important to save it into the editor directory.
// Next for each level in your unity project, run the script. It's located under the window menu.

using UnityEngine;


namespace RZ
{

    using UnityEditor;

    public class FindMissingScripts : EditorWindow
    {
        [MenuItem("RZ/FindMissingScripts")]
        // [MenuItem("Window/RZ/FindMissingScripts")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(FindMissingScripts));
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Find Missing Scripts in selected prefabs"))
            {
                FindInSelected();
            }
        }
        private static void FindInSelected()
        {
            GameObject[] go = Selection.gameObjects;
            int go_count = 0, components_count = 0, missing_count = 0;
            foreach (GameObject g in go)
            {
                go_count++;
                Component[] components = g.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    components_count++;
                    if (components[i] == null)
                    {
                        missing_count++;
                        string s = g.name;
                        Transform t = g.transform;
                        while (t.parent != null)
                        {
                            s = t.parent.name + "/" + s;
                            t = t.parent;
                        }
                        Debug.Log(s + " has an empty script attached in position: " + i, g);
                    }
                }
            }

            Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", go_count, components_count, missing_count));
        }
    }
}