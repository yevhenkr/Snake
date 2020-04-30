// #define RZ_SAFE_AREA

namespace RZ
{

#if UNITY_EDITOR

    using UnityEngine;
    using UnityEditor;
    using UnityEngine.UI;

    /// <summary>
    /// Custom Editor for RZSafeArea
    /// </summary>
    [CustomEditor(typeof(RZSafeArea), true)]
    [CanEditMultipleObjects]
    public class RZSafeAreaEditor : Editor
    {
        /// <summary>
        /// Add RZSafeArea component to selected gameobject.
        /// </summary>
        [MenuItem("RZ/RZComponent/RZ Safe Area", false, RZSafeArea.RZMenuPriority)]
        [MenuItem("Component/UI/RZ/RZ Safe Area", false, RZSafeArea.RZMenuPriority)]
        private static void AddRZSafeArea()
        {
            Undo.AddComponent<RZSafeArea>(Selection.activeGameObject);
        }

        [MenuItem("RZ/RZComponent/RZ Safe Area", true, RZSafeArea.RZMenuPriority)]
        [MenuItem("Component/UI/RZ/RZ Safe Area", true, RZSafeArea.RZMenuPriority)]
        private static bool AddRZSafeAreaValidate()
        {
            return Selection.gameObjects.Length > 0;
        }


        /// <summary>
        /// Add RZSafeArea gameObject.
        /// </summary>
        [MenuItem("GameObject/UI/RZ/RZ Safe Area", false, RZSafeArea.RZMenuPriority)]
        [MenuItem("RZ/RZGameObject/RZ Safe Area", false, RZSafeArea.RZMenuPriority)]
        private static void CreateRZSafeArea(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;

            if (parent == null &&
                Selection.activeTransform != null &&
                Selection.activeTransform.parent != null)
                parent = Selection.activeTransform.parent.gameObject;

            GameObject go = new GameObject();

            GameObjectUtility.SetParentAndAlign(go, parent);

            Undo.RegisterCreatedObjectUndo(go, "Create RZ Safe Area");

            if (go.GetComponentInParent<Canvas>() == null)
            {
                go.name = "Canvas";
                go.AddComponent<Canvas>();
                go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                go.AddComponent<CanvasScaler>();
                go.AddComponent<GraphicRaycaster>();

                parent = go;
                go = new GameObject();
                GameObjectUtility.SetParentAndAlign(go, parent);
            }

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt == null)
            {
                go.AddComponent<RectTransform>();
                rt = go.GetComponent<RectTransform>();
            }
            rt.anchorMin = Vector2Int.zero;
            rt.anchorMax = Vector2Int.one;
            rt.offsetMin = Vector2Int.zero;
            rt.offsetMax = Vector2Int.zero;

            go.name = "RZSafeArea";
            go.AddComponent<RZSafeArea>();
            Selection.activeGameObject = go;
        }


        private RZSafeArea me;
        private string hierarchyWarning;
        private Rect currentSafeArea;
        private SerializedProperty _readyToWorkProp;
        private SerializedProperty _safeAxisXProp;
        private SerializedProperty _expandLeft;
        private SerializedProperty _expandRight;
        private SerializedProperty _safeAxisYProp;
        private SerializedProperty _expandTop;
        private SerializedProperty _expandBottom;

        private void OnEnable()
        {
            me = (RZSafeArea)target;
            serializedObject.Update();
            _readyToWorkProp = serializedObject.FindProperty("_readyToWork");
            _safeAxisXProp = serializedObject.FindProperty("_safeAxisX");
            _expandLeft = serializedObject.FindProperty("_expandLeft");
            _expandRight = serializedObject.FindProperty("_expandRight");
            _safeAxisYProp = serializedObject.FindProperty("_safeAxisY");
            _expandTop = serializedObject.FindProperty("_expandTop");
            _expandBottom = serializedObject.FindProperty("_expandBottom");
        }


        private void OnDisable()
        {

        }

        private void HierarchyErrorsHelpBox()
        {
            string errors = me.GetHierarchyErrors();
            if (string.IsNullOrEmpty(errors)) return;
            EditorGUILayout.HelpBox(errors, MessageType.Error);
        }

        private void AlreadyExistsHelpBox()
        {
            if (me.GetComponentsInParent<RZSafeArea>().Length == 1) return;
            EditorGUILayout.HelpBox(
                "RZSafeArea already exists in parents.\n" +
                "Maybe your hierarchy not optimal.",
                    MessageType.Info);
        }

        public override void OnInspectorGUI()
        {

            // DrawDefaultInspector();

            serializedObject.Update();

            float labelWidthOriginal = EditorGUIUtility.labelWidth;
            // EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth/3;
            EditorGUIUtility.labelWidth = 90;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.BeginVertical();
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(_readyToWorkProp,
                        new GUIContent("Ready To Work", "Read only."));
                    EditorGUI.EndDisabledGroup();

                    EditorGUILayout.Space(4);

                    EditorGUILayout.PropertyField(_safeAxisXProp,
                        new GUIContent("Safe Axis X", ""));
                    EditorGUI.BeginDisabledGroup(!_safeAxisXProp.boolValue);
                    EditorGUILayout.PropertyField(_expandLeft,
                        new GUIContent("Expand Left", ""));
                    EditorGUILayout.PropertyField(_expandRight,
                        new GUIContent("Expand Right", ""));
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndVertical();

                // GUILayout.FlexibleSpace();
                EditorGUILayout.Space(16);

                EditorGUILayout.BeginVertical();
                {
                    if (GUILayout.Button(
                        new GUIContent("Reinit", ""),
                        GUILayout.Width(EditorGUIUtility.labelWidth + 16)))
                    {
                        me.Init();
                        EditorUtility.SetDirty(target);
                    }

                    EditorGUILayout.Space(4);

                    EditorGUILayout.PropertyField(_safeAxisYProp,
                        new GUIContent("Safe Axis Y", ""));
                    EditorGUI.BeginDisabledGroup(!_safeAxisYProp.boolValue);
                    EditorGUILayout.PropertyField(_expandTop,
                        new GUIContent("Expand Top", ""));
                    EditorGUILayout.PropertyField(_expandBottom,
                        new GUIContent("Expand Bottom", ""));
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = labelWidthOriginal;


            HierarchyErrorsHelpBox();

            AlreadyExistsHelpBox();

            EditorGUILayout.Space(4);

            // EditorGUI.BeginChangeCheck();


            // EditorGUILayout.LabelField(
            //     new GUIContent("Active Status:","Is RZSafeArea active and work fine."),
            //     new GUIContent(me.activeStatus.ToString().ToUpper()));


            // EditorGUILayout.PropertyField(advancedCheckProp,
            //     new GUIContent("Advanced Check",
            //         "Use advanced check/fix of SafeArea.\n" +
            //         "More safe but little slower.\n" +
            //         "Recomended value: TRUE.\n\n" +
            //         "Advanced check fix problems on some divices:\n" +
            //         " - if SafeArea is too small;\n" +
            //         " - if SafeArea not inside of screen;\n" +
            //         " - if SafeArea goes beyond of screen.")
            // );

            // EditorGUILayout.PropertyField(useUpdateProp,
            //     new GUIContent("Use Update",
            //         "Use Update() instead of SetLayout().\n" +
            //         "More safe but little slower.\n" +
            //         "Recomended value: FALSE.")
            // );





            // me.logCheckWarnings = EditorGUILayout.Toggle(
            //     new GUIContent("Log Check Warnings",
            //     "Is need to write Advanced Check warnings to console/log."),
            //     me.logCheckWarnings);

            // EditorGUILayout.Space(6);

            // GUILayout.Label(
            //     new GUIContent("Non Safe:", "Non-Safe sides."));

            // EditorGUILayout.BeginHorizontal();

            // me.nonSafeLeft = EditorGUILayout.ToggleLeft(
            //     new GUIContent("Left", "Non-safe left side."),
            //     me.nonSafeLeft, GUILayout.Width(50));

            // me.nonSafeRight = EditorGUILayout.ToggleLeft(
            //     new GUIContent("Right", "Non-safe right side."),
            //     me.nonSafeRight, GUILayout.Width(60));

            // me.nonSafeTop = EditorGUILayout.ToggleLeft(
            //     new GUIContent("Top", "Non-safe top side."),
            //     me.nonSafeTop, GUILayout.Width(50));

            // me.nonSafeBottom = EditorGUILayout.ToggleLeft(
            //     new GUIContent("Bottom", "Non-safe bottom side."),
            //     me.nonSafeBottom, GUILayout.Width(60));

            // EditorGUILayout.EndHorizontal();



            // // EditorGUI.showMixedValue = false;

            // hierarchyWarning = me.CheckHierarchyErrors(false, false);
            // if (!string.IsNullOrEmpty(hierarchyWarning))
            // {
            //     EditorGUILayout.Space(6);
            //     EditorGUILayout.HelpBox(hierarchyWarning, MessageType.Warning);
            // }



            // // if (!string.IsNullOrEmpty(RZSafeArea.checkWarningString))
            // // {
            // //     EditorGUILayout.HelpBox(
            // RZSafeArea.checkWarningString, MessageType.Warning);
            // // }

            // // currentSafeArea = me.GetSafeArea(false);
            // // EditorGUILayout.HelpBox(
            // //     "Current SafeArea:\n" +
            // //     "width = " + currentSafeArea.width +
            // //             "   (of " + Screen.width + ")\n" +
            // //     "height = " + currentSafeArea.height +
            // //             "   (of " + Screen.height + ")\n" +
            // //     "x = " + currentSafeArea.x + "   (of " + Screen.width + ")\n" +
            // //     "y = " + currentSafeArea.y + "   (of " + Screen.height + ")\n" +
            // //     "xMin = " + currentSafeArea.xMin + "   (of " + Screen.width + ")\n" +
            // //     "yMin = " + currentSafeArea.yMin + "   (of " + Screen.height + ")\n" +
            // //     "xMax = " + currentSafeArea.xMax + "   (of " + Screen.width + ")\n" +
            // //     "yMax = " + currentSafeArea.yMax + "   (of " + Screen.height + ")\n"
            // //     , MessageType.Info);

            // if(EditorGUI.EndChangeCheck())me.SetDirty();

            if (serializedObject.ApplyModifiedProperties())
            {
                me.SetDirty();
                EditorUtility.SetDirty(target);
            }


        }

    }

#endif
}