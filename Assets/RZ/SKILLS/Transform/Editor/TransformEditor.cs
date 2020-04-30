namespace RZ
{

#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    using System.Reflection;
    using System;


    [CustomEditor(typeof(Transform), true)]
    [CanEditMultipleObjects]
    public class TransformEditor : Editor
    {
        private Editor defaultEditor; // Unity's built-in editor
        private Transform transform;

        void OnEnable()
        {
            // When this inspector is created, also create the built-in inspector
            defaultEditor = Editor.CreateEditor(
                targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));

            transform = target as Transform;
        }


        void OnDisable()
        {
            // When OnDisable is called, the default editor we created should be
            // destroyed to avoid memory leakage.
            // Also, make sure to call any required methods like OnDisable
            MethodInfo disableMethod = defaultEditor.GetType().GetMethod("OnDisable",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (disableMethod != null)
                disableMethod.Invoke(defaultEditor, null);

            DestroyImmediate(defaultEditor);
        }


        public override void OnInspectorGUI()
        {
            //  RZEditorSkills.DrawAllSkills();

            defaultEditor.OnInspectorGUI(); // Unity's built-in editor

            if (RZEditorSkills.showSkills)
            {
                RZEditorGUI.BeginRZBoxHorizontal();


                if (GUILayout.Button(new GUIContent("Add Rect Transform",
                        "Add Rect Transform instead of Transform.\n" +
                        "(Values will be transferred.)"),
                            EditorStyles.miniButtonLeft))
                {
                    foreach (Transform transform in Selection.transforms)
                    {
                        if (transform.gameObject.GetComponent<RectTransform>() == null)
                            transform.gameObject.AddComponent<RectTransform>();
                    }
                }

                RZEditorGUI.EndRZBoxHorizontal();
            }
        }

    }

#endif
}