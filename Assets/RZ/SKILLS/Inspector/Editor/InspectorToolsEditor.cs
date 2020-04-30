namespace RZ
{
#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(InspectorTools))]
    public class InspectorToolsEditor : Editor
    {
        private InspectorTools me;
        private bool repaintOnceNow;

        public void OnEnable()
        {
            me = (InspectorTools)target;
            me.forceRepaint = false;
        }

        public override void OnInspectorGUI()
        { DrawInspectorTools(false); }


        private bool foldoutIT;
        public void DrawInspectorTools(bool drawHeader = true)
        {
            EditorGUILayout.BeginVertical();
            if (drawHeader)
            {
                GUIStyle style = EditorStyles.foldout;
                FontStyle previousStyle = style.fontStyle;
                style.fontStyle = FontStyle.Bold;
                foldoutIT = EditorGUILayout.Foldout(foldoutIT, "Inspector Tools", true);
                style.fontStyle = previousStyle;
            }
            else
            {
                foldoutIT = true;
            }

            if (foldoutIT || me.forceRepaint)
            {
                EditorGUILayout.BeginHorizontal();
                me.forceRepaint = EditorGUILayout.Toggle("Force repaint", me.forceRepaint);
                BlinkMessage_HARD(me.forceRepaint);
                GUILayout.FlexibleSpace();
                GUILayout.Space(-120);
                RZEditorGUI.IndicatorOfRedreaw();
                EditorGUILayout.EndHorizontal();

                repaintOnceNow = GUILayout.Button("Repaint Once Just Now");

                if (me.forceRepaint || repaintOnceNow) EditorUtility.SetDirty(me);
            }
            EditorGUILayout.EndVertical();
        }


        private static sbyte blinkLabelCycle = 4;
        private static sbyte blinkLabelState = 0;
        static void BlinkMessage_HARD(bool blink)
        {
            if (blink)
            {
                blinkLabelState++;
                if (blinkLabelState >= blinkLabelCycle) blinkLabelState = 0;
                RZEditorGUI.IndicatorLabels.HARD(blinkLabelState < blinkLabelCycle / 2);
            }
            else
            {
                RZEditorGUI.IndicatorLabels.HARD(false);
            }
        }

        public static readonly Color COLOR_INSPECTOR_TEXT = new Color(0.4f, 0.4f, 0.4f, 1);



    }
#endif
}