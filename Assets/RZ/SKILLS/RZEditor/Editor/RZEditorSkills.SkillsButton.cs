namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorSkills
    {

        public partial class Options : RZOptions
        {
            public GUIStyle editorSkillsButtonStyle = null;
            public Color[] editorSkillsButtonColors_ON =
                {   RZEditorGUI.Colors.greenIcon ,Color.white };
            public Color[] editorSkillsButtonColors_OFF =
                { RZEditorGUI.Colors.greenIcon/2 ,Color.white};

            public Options() : base("keyName")
            {
            }
        }

        /// <summary>
        /// Draw button to Enable/Disable RZ advanced functionary.
        /// </summary>
        public static bool SkillsButton()
        {
            RZEditorSkills.Options megrate = new Options();

            if (options.editorSkillsButtonStyle == null)
            {
                options.editorSkillsButtonStyle = new GUIStyle(GUI.skin.button);
                options.editorSkillsButtonStyle.fontStyle = FontStyle.Bold;
            }

            Color oldColor = GUI.backgroundColor;
            if (showSkills)
            {
                GUI.backgroundColor = options.editorSkillsButtonColors_ON[0];
                options.editorSkillsButtonStyle.normal.textColor =
                    options.editorSkillsButtonColors_ON[1];

                if (GUILayout.Button(
                    new GUIContent("RZ SKILLS [ON->OFF]", "Show / Hide RZ advanced functionary."),
                        options.editorSkillsButtonStyle))
                    showSkills = false;

                GUI.backgroundColor = oldColor;

                return showSkills;
            }
            else
            {
                GUI.backgroundColor = options.editorSkillsButtonColors_OFF[0];
                options.editorSkillsButtonStyle.normal.textColor =
                    options.editorSkillsButtonColors_OFF[1];

                if (GUILayout.Button(
                    new GUIContent("RZ SKILLS [OFF->ON]", "Show / Hide RZ advanced functionary."),
                        options.editorSkillsButtonStyle))
                    showSkills = true;

                GUI.backgroundColor = oldColor;

                return showSkills;
            }

        }

    }

#endif

}