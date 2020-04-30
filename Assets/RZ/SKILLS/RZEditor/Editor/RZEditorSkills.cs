using System.ComponentModel.Design;
namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// RZ advanced functionary.
    /// </summary>
    public partial class RZEditorSkills
    {

        public static Options options = new Options();

        public partial class Options : RZOptions
        {
            public bool showSkills = true;
            public bool foldoutTips = true;
        }

        /// <summary>
        /// Show/Hide RZ advanced functionary.
        /// </summary>
        public static bool showSkills
        {
            get { return options.showSkills; }
            set { options.showSkills = value; }
        }

        /// <summary>
        /// Show/Hide RZ advanced functionary tips.
        /// </summary>
        public static bool foldoutTips
        {
            get { return options.foldoutTips; }
            set { options.foldoutTips = value; }
        }

        public static void DrawAllSkills()
        {
            if (!RZEditorSkills.options.isLoaded)
            { RZEditorSkills.options.Load(); }
            EditorGUI.BeginChangeCheck();



            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(-1);
            GUILayout.Space(+1);

            RZEditorSkills.SkillsButton();

            EditorGUILayout.EndHorizontal();



            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(+2);
            EditorGUILayout.BeginVertical();

            if (RZEditorSkills.showSkills) RZEditorSkills.Tips();

            EditorGUILayout.EndVertical();
            GUILayout.Space(+4);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(+4);


            if (EditorGUI.EndChangeCheck())
            { RZEditorSkills.options.Save(); }
        }
    }

#endif

}