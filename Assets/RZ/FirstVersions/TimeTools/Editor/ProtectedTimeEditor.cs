using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    using System;
    // VER: 2.0

#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine.SceneManagement;

    [CustomEditor(typeof(ProtectedTime))]
    public class ProtectedTimeEditor : Editor
    {
        ProtectedTime me;

        bool changed;
        string protString;
        bool foldoutHistory;

        public override void OnInspectorGUI()
        {
            me = (ProtectedTime)target;

            DateTime syst = DateTime.Now;
            DateTime prot = ProtectedTime.Get();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("DateTime", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("System", syst.ToString(Tools.DATE_TIME_FORMAT));

            EditorGUI.BeginChangeCheck();
            if (!changed) protString = prot.ToString(Tools.DATE_TIME_FORMAT);
            protString = EditorGUILayout.TextField("Protected", protString);
            if (EditorGUI.EndChangeCheck()) { changed = true; }

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!changed);
            if (GUILayout.Button(" Apply ")) { ProtectedTime.Set(protString, false); changed = false; }
            if (GUILayout.Button("Ignore")) { changed = false; }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("-60")) { ProtectedTime.Add(-60); }
            if (GUILayout.Button("-10")) { ProtectedTime.Add(-10); }
            if (GUILayout.Button("-1")) { ProtectedTime.Add(-1); }
            if (GUILayout.Button("+0 -0")) { ProtectedTime.Add(0); }
            if (GUILayout.Button("+1")) { ProtectedTime.Add(+1); }
            if (GUILayout.Button("+10")) { ProtectedTime.Add(+10); }
            if (GUILayout.Button("+60")) { ProtectedTime.Add(+60); }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set from system")) ProtectedTime.SetFromSystem();
            EditorGUI.BeginDisabledGroup(ProtectedTime.history == null);
            if (GUILayout.Button("Set from history")) ProtectedTime.SetFromHistory();
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Deviation from system", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            var ptm = (ProtectedTime.Accuracy)EditorGUILayout.EnumPopup("Accuracy", ProtectedTime.accuracy);
            if (EditorGUI.EndChangeCheck()) ProtectedTime.accuracy = ptm;

            EditorGUILayout.LabelField("Current ", ProtectedTime.currentDeviation.ToString());
            EditorGUI.BeginChangeCheck();
            var allow = EditorGUILayout.IntField("Allowed (absolute)", ProtectedTime.allowedDeviation);
            if (EditorGUI.EndChangeCheck()) ProtectedTime.allowedDeviation = Mathf.Clamp(allow, 0, int.MaxValue);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("History", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            var cap = EditorGUILayout.IntField("Max history", ProtectedTime.maxHistory);
            if (EditorGUI.EndChangeCheck()) ProtectedTime.maxHistory = (sbyte)Mathf.Clamp(cap, sbyte.MinValue, sbyte.MaxValue);
            EditorGUI.BeginDisabledGroup(ProtectedTime.maxHistory <= 0);
            if (GUILayout.Button("Clear")) { ProtectedTime.history.ClearStack(); }
            EditorGUILayout.EndHorizontal();

            if (ProtectedTime.history != null)
            {
                EditorGUILayout.LabelField("Often deviation", ProtectedTime.history.OftenD.ToString());
                EditorGUILayout.LabelField("Max quantity", ProtectedTime.history.MaxQ.ToString());
                EditorGUILayout.LabelField("timeOfStart", ProtectedTime.history.timeOfStart);
                EditorGUILayout.LabelField("uptimeSeconds", ProtectedTime.history.uptimeSeconds.ToString());
            }

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();
            foldoutHistory = EditorGUILayout.Foldout(foldoutHistory, "Stack", true);
            if (ProtectedTime.history != null && foldoutHistory)
            {
                EditorGUILayout.LabelField("[Deviations]", "[  Count   ]");
                for (int i = 0; i < ProtectedTime.history.Count; i++)
                {
                    var pare = ProtectedTime.history.GetPair_Dev_Quant(i);
                    EditorGUILayout.LabelField("  " + pare.dev, "  " + pare.quant);
                }
                // EditorGUILayout.LabelField("__________", "__________");
            }
            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;


            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Info (read Only)", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            ProtectedTime.trustMe = EditorGUILayout.Toggle("Trust me", ProtectedTime.trustMe);

            if (ProtectedTime.trustMe)
                RZEditorGUI.IndicatorLabels.GOOD(true);
            else
                RZEditorGUI.IndicatorLabels.BAD(true);

            EditorGUILayout.EndHorizontal();

            ProtectedTime.isCheatDetected = EditorGUILayout.Toggle("Is cheat detected", ProtectedTime.isCheatDetected);
            EditorGUILayout.BeginHorizontal();
            bool cheatNow = ProtectedTime.IsCheatNow();
            EditorGUILayout.Toggle("Is cheat now", cheatNow);

            if (!cheatNow)
                RZEditorGUI.IndicatorLabels.GOOD(true);
            else
                RZEditorGUI.IndicatorLabels.BAD(true);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EnumPopup("Used time system", SystemUptime.usedTimeSystem);
            EditorGUILayout.LabelField("Time of start", ProtectedTime.timeOfStart.ToString(Tools.DATE_TIME_FORMAT));
            EditorGUILayout.LabelField("Uptime (seconds)", SystemUptime.WholeSeconds.ToString());
            EditorGUILayout.LabelField("Uptime (time span)", SystemUptime.TimeSpan.ToString());
            EditorGUILayout.LabelField("Reboot detected", ProtectedTime.rebootDetected ? "True" : "False", EditorStyles.boldLabel);


            // DrawDefaultInspector();
        }
    }
#endif
}
