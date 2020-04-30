namespace RZ
{

#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    using System.Reflection;
    using System;
    using System.Collections.Generic;

    [CustomEditor(typeof(RectTransform), true)]
    [CanEditMultipleObjects]
    public class RectTransformEditor : Editor
    {

        private Editor defaultEditor; // Unity's built-in editor
        private RectTransform rectTransform;
        private bool isDisableCurrent;
        private bool isDisableRecursive;
        private Transform parent;
        private int childCount;

        void OnEnable()
        {
            // Check for disable buttons:
            CheckCurrent();
            CheckRecursive();

            // When this inspector is created, also create the built-in inspector
            defaultEditor = Editor.CreateEditor(
                targets, Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));

            rectTransform = target as RectTransform;

            // Assembly ass = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            //     Type rtEditor = ass.GetType("UnityEditor.RectTransformEditor");
            //     _editorInstance = CreateEditor(target, rtEditor);
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

            // RZ BUGS:
            DestroyImmediate(defaultEditor);
        }


        private void CheckCurrent()
        {
            if (Selection.activeGameObject == null) parent = null;
            else parent = Selection.activeGameObject.transform.parent;

            foreach (var item in Selection.gameObjects)
            {
                var pt = item.transform.parent;
                if (pt != null)
                {
                    var prt = pt.GetComponent<RectTransform>();
                    if (prt != null)
                    {
                        isDisableCurrent = false;
                        return;
                    }
                }
            }
            isDisableCurrent = true;
            return;
        }


        private void CheckRecursive()
        {
            if (Selection.activeGameObject == null) childCount = 0;
            else childCount = Selection.activeGameObject.transform.childCount;

            foreach (var item in Selection.gameObjects)
            {
                if (item.GetComponentsInChildren<RectTransform>().Length > 1)
                {
                    isDisableRecursive = false;
                    return;
                }
            }
            isDisableRecursive = true;
            return;
        }


        public override void OnInspectorGUI()
        {
            if (Selection.activeGameObject != null)
            {
                if (parent != Selection.activeGameObject.transform.parent) CheckCurrent();
                if (childCount != Selection.activeGameObject.transform.childCount) CheckRecursive();
            }

            defaultEditor.OnInspectorGUI(); // Unity's built-in editor

            if (RZEditorSkills.showSkills)
            {

                RZEditorGUI.BeginRZBoxHorizontal();
                EditorGUILayout.BeginVertical();


                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUI.BeginDisabledGroup(isDisableCurrent);
                if (GUILayout.Button(new GUIContent("  Anchors To Corners  ",
                    "Move anchors to object's corners."),
                        EditorStyles.miniButtonLeft))
                {
                    UnityEditor.Undo.RecordObjects(
                        Selection.transforms, "RZ Anchors To Corners");

                    foreach (var item in Selection.gameObjects)
                    { RectTransformExtensions.RZAnchorsToCorners(item); }
                }
                EditorGUI.EndDisabledGroup();


                EditorGUI.BeginDisabledGroup(isDisableRecursive);
                if (GUILayout.Button(new GUIContent("  Recursive  ",
                    "Move anchors to object's corners,\n" +
                    "and do it with object's children,\n" +
                    "children of children..."),
                        EditorStyles.miniButtonRight))
                {
                    List<UnityEngine.Object> undoList = new List<UnityEngine.Object>();
                    foreach (var item in Selection.transforms)
                    { undoList.AddRange(item.GetComponentsInChildren<Transform>()); }
                    UnityEditor.Undo.RecordObjects(
                        undoList.ToArray(), "RZ Anchors To Corners Recursive");

                    foreach (var item in Selection.gameObjects)
                    { RectTransformExtensions.RZAnchorsToCornersRecursive(item); }
                }
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();




                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUI.BeginDisabledGroup(isDisableCurrent);
                if (GUILayout.Button(new GUIContent("  Corners To Anchors  ",
                    "Move corners to object's anchors."),
                        EditorStyles.miniButtonLeft))
                {
                    UnityEditor.Undo.RecordObjects(
                        Selection.transforms, "RZ Corners To Anchors");

                    foreach (var item in Selection.gameObjects)
                    { RectTransformExtensions.RZCornersToAnchors(item); }
                }
                EditorGUI.EndDisabledGroup();


                EditorGUI.BeginDisabledGroup(isDisableRecursive);
                if (GUILayout.Button(new GUIContent("  Recursive  ",
                    "Move corners to object's anchors,\n" +
                    "and do it with object's children,\n" +
                    "children of children..."),
                        EditorStyles.miniButtonRight))
                {
                    List<UnityEngine.Object> undoList = new List<UnityEngine.Object>();
                    foreach (var item in Selection.transforms)
                    { undoList.AddRange(item.GetComponentsInChildren<Transform>()); }
                    UnityEditor.Undo.RecordObjects(
                        undoList.ToArray(), "RZ Corners To Anchors Recursive");

                    foreach (var item in Selection.gameObjects)
                    { RectTransformExtensions.RZCornersToAnchorsRecursive(item); }
                }
                EditorGUI.EndDisabledGroup();
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.EndVertical();
                RZEditorGUI.EndRZBoxHorizontal();

            }
        }


        private void OnSceneGUI()
        {
            if (defaultEditor != null)
            {
                MethodInfo onSceneGUI_Method = defaultEditor.GetType().GetMethod("OnSceneGUI",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (onSceneGUI_Method != null)
                    onSceneGUI_Method.Invoke(defaultEditor, null);
            }
        }

    }

#endif

}