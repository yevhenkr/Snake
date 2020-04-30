using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    using System;
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(RelativeTime))]
    public class RelativeTimeEditor : Editor
    {
        bool foldOutCommon = false;
        public override void OnInspectorGUI()
        {
            RelativeTime me = (RelativeTime)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Current object:", EditorStyles.boldLabel);

            DrawDefaultInspector();

            string valType = "";
            switch (me.relativeFrom)
            {
                default:
                case RelativeTime.Relative.Selected:
                    me.selectedDateTime = EditorGUILayout.TextField("Selected", me.selectedDateTime);
                    valType = "(d)";
                    break;
                case RelativeTime.Relative.StartApp: valType = "(f)"; break;
                case RelativeTime.Relative.RealStartApp: valType = "(f)"; break;
                case RelativeTime.Relative.RealStartOS: valType = "(f)"; break;
                case RelativeTime.Relative.UnixZero: valType = "(d)"; break;
            }

            if (string.IsNullOrEmpty(me.selectedDateTime)) me.selectedDateTime = DateTime.Now.ToString(Tools.DATE_TIME_FORMAT);
            EditorGUILayout.DoubleField("Seconds " + valType, me.GetSeconds());

            GUIStyle style = EditorStyles.foldout;
            FontStyle previousStyle = style.fontStyle;
            style.fontStyle = FontStyle.Bold;
            foldOutCommon = EditorGUILayout.Foldout(foldOutCommon, "Common info", true);
            style.fontStyle = previousStyle;
            if (foldOutCommon)
            {
                // EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Relative:", "Seconds:");
                EditorGUILayout.FloatField("Start App (f)", RelativeTime.GetSeconds_StartApp());
                EditorGUILayout.FloatField("Real Start App (f)", RelativeTime.GetSeconds_RealStartApp());
                EditorGUILayout.FloatField("Real Start OS (f)", RelativeTime.GetSeconds_RealStartOS());
                EditorGUILayout.DoubleField("Unix Zero (d)", RelativeTime.GetSeconds_UnixZero());
                // EditorGUI.indentLevel--;
            }
        }
    }
#endif
}

