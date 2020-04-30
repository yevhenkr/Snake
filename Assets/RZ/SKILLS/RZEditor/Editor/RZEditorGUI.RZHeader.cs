namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorGUI
    {


        public partial class Options : RZOptions
        {
            public GUIStyle rzHeaderLabelStyle = null;
            public float rzHeaderLabelWidth = 24;
        }


        protected static GUIStyle Get_rzHeaderLabelStyle()
        {
            if (options.rzHeaderLabelStyle == null)
            {
                options.rzHeaderLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                options.rzHeaderLabelStyle.normal.textColor = Colors.greenLabel;
                options.rzHeaderLabelStyle.hover.textColor = Colors.greenLabel;
            }
            return options.rzHeaderLabelStyle;
        }


        /// <summary>
        /// Draw bold green label "RZ".
        /// </summary>
        public static void RZHeaderLabel()
        {
            EditorGUILayout.LabelField(Framework.name, Get_rzHeaderLabelStyle(),
                GUILayout.Width(options.rzHeaderLabelWidth));
        }



        /// <summary>
        /// Draw bold green foldout label "RZ" with custom text.
        /// </summary>
        public static bool RZHeaderFoldout(bool foldout, string text = "")
        {
            float spacing =
                EditorGUIUtility.singleLineHeight +
                EditorGUIUtility.standardVerticalSpacing;

            EditorGUILayout.LabelField(Framework.name, Get_rzHeaderLabelStyle());
            GUILayout.Space(-spacing);
            EditorGUILayout.LabelField(" ", text);
            GUILayout.Space(-spacing);

            GUILayout.BeginHorizontal();
            GUILayout.Space(-6);
            foldout = EditorGUILayout.Foldout(foldout, " ", true);
            GUILayout.EndHorizontal();

            return foldout;
        }


    }

#endif

}