using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RZ
{
    //     #if UNITY_EDITOR

    //     using UnityEngine;
    //     using UnityEditor;

    //     [CustomEditor(typeof(PasswordChar), true)]
    //     [CanEditMultipleObjects]
    //     public class PasswordCharEditor : Editor
    //     {
    //         public override void OnInspectorGUI()
    //         {
    //             var me = (PasswordChar)target;

    //             EditorGUILayout.TextField()
    //             GUILayout.Label("Current value", EditorStyles.boldLabel);
    //             EditorGUILayout.BeginHorizontal();
    //             cv= EditorGUILayout.LongField(""+me.currentValue, cv);
    //             if (GUILayout.Button("Set"))me.currentValue = cv;
    //             if (GUILayout.Button("Reset"))me.currentValue = 0;
    //             EditorGUILayout.EndHorizontal();

    //             GUILayout.Label("Target value", EditorStyles.boldLabel);
    //             EditorGUILayout.BeginHorizontal();
    //              tv = EditorGUILayout.LongField(""+me.targetValue, tv);
    //             if (GUILayout.Button("Set"))me.targetValue = tv;
    //             if (GUILayout.Button("Reset"))me.targetValue = 0;
    //             EditorGUILayout.EndHorizontal();

    //             DrawDefaultInspector();
    //         }
    //     }

    // #endif

    //ЗАМЕНА СИМВОЛА, СКРЫВАЮЩЕГО ПАРОЛЬ:
    public class PasswordChar : MonoBehaviour
    {
        public char _asteriskChar = '•';
        public char asteriskChar
        {
            get
            {
                return _asteriskChar;
            }
            set
            {
                _asteriskChar = value;
                gameObject.GetComponent<InputField>().asteriskChar = _asteriskChar;
            }
        }

        void Start()
        {
            asteriskChar = _asteriskChar;
        }

#if UNITY_EDITOR
        // Inspector modified?
        protected virtual void OnValidate()
        {
            asteriskChar = _asteriskChar;
        }
#endif

    }
}