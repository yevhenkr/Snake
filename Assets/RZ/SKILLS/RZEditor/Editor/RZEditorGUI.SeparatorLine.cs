

namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {

        }


        /// <summary>
        /// Draw horizontal line like slider.
        /// </summary>
        public static void SeparatorLine(string label = "")
        { EditorGUILayout.LabelField(label, "", GUI.skin.horizontalSlider); }

    }

#endif

}