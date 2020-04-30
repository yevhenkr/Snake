using UnityEngine;
using UnityEngine.UI;

namespace RZ
{
    using System.Collections.Generic;

#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(SystemBackButton), true)]
    public class SystemBackButtonEditor : Editor
    {
        bool foldout;
        public override void OnInspectorGUI()
        {
            var me = (SystemBackButton)target;

            EditorGUI.BeginDisabledGroup(!me.isActiveAndEnabled);
            if (GUILayout.Button("⇦ / BACK / ESC"))
            {
                SystemBackButton.DoClick();
                EditorUtility.SetDirty(me);
            }
            EditorGUI.EndDisabledGroup();


            var stack = SystemBackButton.backStack;
            foldout = EditorGUILayout.Foldout(foldout, "Back Stack: " + stack.Count, true);
            if (foldout)
            {
                EditorGUI.indentLevel += 1;
                for (int i = 0; i < stack.Count; i++)
                {
                    EditorGUI.indentLevel += 1;
                    EditorGUILayout.ObjectField(stack[i], typeof(Button), true);
                }
                EditorGUI.indentLevel -= stack.Count;
                EditorGUI.indentLevel -= 1;
            }

            DrawDefaultInspector();
        }
    }
#endif

    ///////////////////////////////////////////////////////////////////////////////////////

    // [RequireComponent(typeof(Button))]
    public class SystemBackButton : MonoBehaviour
    {
        public static List<Button> backStack = new ListExtended<Button>();

        Button button = null;
        void OnEnable()
        {
            button = gameObject.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogWarning("Can not find a button!");
            }
            else
            {
                backStack.Insert(0, button);
            }
        }

        void OnDisable()
        {
            if (backStack.Contains(button))
            {
                backStack.Remove(button);
            }
            button = null;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) DoClick();
        }

        public static void DoClick()
        {
            if (backStack.Count > 0)
            {
                var b = backStack[0];
                if (b == null)
                {
                    backStack.Remove(b);
                    DoClick();
                }
                else
                {
                    if (b.isActiveAndEnabled && b.interactable)
                    {
                        b.onClick.Invoke();
                    }
                }
            }
        }
    }
}