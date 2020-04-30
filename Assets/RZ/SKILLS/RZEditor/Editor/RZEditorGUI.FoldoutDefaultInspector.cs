namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;

    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {

        }

        /// <summary>
        /// Draw default inspector in fold/foldout.
        /// </summary>
        public static bool FoldoutDefaultInspector(Editor editor, bool foldOut)
        {
            foldOut = EditorGUILayout.Foldout(foldOut, "Default Inspector (Danger)", true);
            if (foldOut)
            {
                EditorGUI.indentLevel++;
                editor.DrawDefaultInspector();
                EditorGUI.indentLevel--;
            }
            return foldOut;
        }

    }

#endif

}