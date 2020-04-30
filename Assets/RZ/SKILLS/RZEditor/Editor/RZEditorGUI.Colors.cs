namespace RZ
{

#if UNITY_EDITOR

    using UnityEngine;

    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {
            public Color transparentColor = new Color(1, 1, 1, 0);

            // Bytes color GREEN 0, 150, 50, 1:
            public Color greenColor = new Color(0, 0.5882353f, 0.1882353f, 1);
            public Color greenLabelColor = new Color(0, 0.5882353f, 0.1882353f, 1);
            public Color greenIconColor = new Color(0, 0.5882353f, 0.1882353f, 1);

            public Color defaultTextColor = UnityEditor.EditorStyles.label.normal.textColor;
            public Color indicatorsColor = UnityEditor.EditorStyles.label.normal.textColor;

            public Options() : base("RZEditorGUI.Options")
            {
            }
        }


        /// <summary>
        /// Colors of RZEditorGUI controls.
        /// </summary>
        public partial class Colors
        {
            public static Color transparent { get { return options.transparentColor; } }

            public static Color defaultText { get { return options.defaultTextColor; } }

            // Bytes color GREEN 0, 150, 50, 1:
            public static Color green { get { return options.greenColor; } }
            public static Color greenLabel { get { return options.greenLabelColor; } }
            public static Color greenIcon { get { return options.greenIconColor; } }

            public static Color indicators { get { return options.indicatorsColor; } }
        }

    }

#endif

}