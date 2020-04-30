namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorSkills
    {

        public partial class Options : RZOptions
        {
            public bool showTips = true;
        }

        /// <summary>
        /// Draw RZ Editor tips.
        /// </summary>
        public static void Tips()
        {
            EditorGUI.indentLevel++;
            // EditorGUILayout.BeginHorizontal();

            // EditorGUILayout.Space(-18);

            // RZEditorGUI.BeginRZBoxHorizontal();
            options.showTips = EditorGUILayout.Foldout(options.showTips, "RZ Tips", true);
            // EditorGUILayout.BeginVertical();

            if (options.showTips)
            {
                EditorGUI.indentLevel++;
                GUILayout.Button("test tip 0");
                GUILayout.Button("test tip 1");
                GUILayout.Button("test tip 2");
                GUILayout.Button("test tip 3");
                GUILayout.Button("test tip 4");
                EditorGUI.indentLevel--;
            }


            // EditorGUILayout.EndVertical();
            // RZEditorGUI.EndRZBoxHorizontal();

            // options.showTips =
            //     RZEditorGUI.BeginRZBoxFoldout(options.showTips, "Tips For Components",18);

            // if (options.showTips)
            // {



            EditorGUI.indentLevel--;
            //     




            // }


            // RZEditorGUI.EndRZBoxFoldout(4);
            // // EditorGUILayout.Space(4);
            // EditorGUILayout.EndHorizontal();


            // EditorGUILayout.Space(-4);
        }

    }

#endif

}