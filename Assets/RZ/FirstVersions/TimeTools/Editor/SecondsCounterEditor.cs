using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine.SceneManagement;

    [CustomEditor(typeof(SecondsCounterBehaviour))]
    public class SecondsCounterEditor : Editor
    {
        SecondsCounterBehaviour me;
        // [InitializeOnLoadMethod]
        // public static void InitUpdate() { EditorApplication.update += Update; }

        // static void Update()
        // {
        //     // Debug.Log(Time.realtimeSinceStartup+" "+Time.time+"       "+(Time.realtimeSinceStartup-Time.time));
        //     Debug.Log(Time.timeScale);
        // }

        public override void OnInspectorGUI()
        {
            me = (SecondsCounterBehaviour)target;

            seconds = EditorGUILayout.FloatField("Seconds", seconds);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Start")) me.counter.Start();
            if (GUILayout.Button("Stop")) me.counter.Stop();
            if (GUILayout.Button("Reset")) { me.counter.Reset(); seconds = 0; }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Time Relative:");
            bool realTime = me.counter.GetTimeRelative() == SecondsCounter.Relative.AppRealtime_IgnoreGamePause;


            EditorGUI.BeginChangeCheck();
            bool realtime = me.counter.GetTimeRelative() == SecondsCounter.Relative.AppRealtime_IgnoreGamePause;
            realtime = GUILayout.Toggle(realtime, CheckMark(realtime) + " AppRealtime_IgnoreGamePause", "Button");
            realtime = !GUILayout.Toggle(!realtime, CheckMark(!realtime) + "    AppTime_PauseOnGamePause", "Button");
            bool change = EditorGUI.EndChangeCheck();
            if (change)
            {
                if (realtime) me.counter.SetTimeRelative(SecondsCounter.Relative.AppRealtime_IgnoreGamePause);
                if (!realtime) me.counter.SetTimeRelative(SecondsCounter.Relative.AppTime_PauseOnGamePause);
            }

            // EditorGUI.BeginDisabledGroup(realTime);
            // if (GUILayout.Button("AppRealtime_IgnoreGamePause")) me.counter.SetTimeRelative(SecondsCounter.Relative.AppRealtime_IgnoreGamePause);
            // EditorGUI.EndDisabledGroup();

            // EditorGUI.BeginDisabledGroup(!realTime);
            // if (GUILayout.Button("AppTime_PauseOnGamePause")) me.counter.SetTimeRelative(SecondsCounter.Relative.AppTime_PauseOnGamePause);
            // EditorGUI.EndDisabledGroup();

            DrawDefaultInspector();
        }

        float seconds
        {
            get
            {
                return me.counter.GetSeconds();
            }
            set
            {
                if (me.counter.GetSeconds() != value) me.counter.SetSeconds(value);
            }
        }

        // ✔ ✓ ☐ ☑
        string CheckMark(bool active) { return active ? "☑" : "☐"; }
    }
#endif
}
