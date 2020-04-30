using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;

    /// <summary>
    /// Contains GUI Controls by RZ.
    /// </summary>
    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {

        }


        /// <summary>
        /// Start RZBox horizontal (header at left of content)
        /// </summary>
        public static void BeginRZBoxHorizontal(float spacePixels = 4)
        {
            GUILayout.Space(spacePixels);
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            RZEditorGUI.RZHeaderLabel();
        }


        /// <summary>
        /// Finish RZBox horizontal (header at left of content)
        /// </summary>
        public static void EndRZBoxHorizontal(float spacePixels = 0)
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(spacePixels);
        }


        /// <summary>
        /// Start RZBox vertical (header at top of content)
        /// </summary>
        public static void BeginRZBoxVertical(float spacePixels = 4)
        {
            GUILayout.Space(spacePixels);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            RZEditorGUI.RZHeaderLabel();
        }


        /// <summary>
        /// Finish RZBox vertical   (header at top of content)
        /// </summary>
        public static void EndRZBoxVertical(float spacePixels = 0)
        {
            GUILayout.EndVertical();
            GUILayout.Space(spacePixels);
        }


        /// <summary>
        /// Start RZBox vertical foldout (header at top of content)
        /// </summary>
        public static bool BeginRZBoxFoldout(bool foldout, string header = "",
            float spacePixels = 4)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(spacePixels);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            foldout = RZEditorGUI.RZHeaderFoldout(foldout, header);
            return foldout;
        }


        /// <summary>
        /// Finish RZBox vertical foldout (header at top of content)
        /// </summary>
        public static void EndRZBoxFoldout(float spacePixels = 0)
        {
            GUILayout.EndVertical();
            GUILayout.Space(spacePixels);
            GUILayout.EndHorizontal();
        }


    }

#endif

}
