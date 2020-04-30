using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace RZ
{

#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(___TextExtended))]

    public class ___TextExtendedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
#endif

    // [ExecuteInEditMode]
    // [DisallowMultipleComponent]
    // [RequireComponent(typeof(Text))]
    public class ___TextExtended : Text
    {
        // [SerializeField] public bool b = false;
        public enum CaseMode
        {
            NoChange,
            ToLower,
            ToUpper,
            FirstUpper,
            EveryFirstUpper
        };

        public CaseMode _caseMode = CaseMode.NoChange;

        public CaseMode caseMode
        {
            get { return _caseMode; }
            set { _caseMode = value; base.text = CaseChange(text); }
        }

        // new public string text
        override public string text
        {
            get { return base.text; }
            set { base.text = CaseChange(value); }
        }

        string CaseChange(string s)
        {
            switch (caseMode)
            {
                default:
                case CaseMode.NoChange:
                    return s;

                case CaseMode.ToLower:
                    return s.ToLower();

                case CaseMode.ToUpper:
                    return s.ToUpper();

                case CaseMode.FirstUpper:
                    return StringTools.FirstCharToUpper(s);

                case CaseMode.EveryFirstUpper:
                    return StringTools.EveryFirstCharToUpper(s);

            }
        }
    }
}