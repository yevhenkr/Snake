namespace RZ
{


#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using System.Reflection;

    ////////// 0 //////////////////////////////////////////////////////////////////////////////

    // [CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
    // public class GameObjectEditor : Editor
    // {

    //     public override void OnInspectorGUI()
    //     {
    //         base.OnInspectorGUI(); // GameObjectInspector

    //         RZEditorSkills.DrawAllSkills();
    //     }

    // }

    ////////// 1 //////////////////////////////////////////////////////////////////////////////

    // [CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
    // public class GameObjectEditor : RZEditorDecorator
    // {

    //     public GameObjectEditor() : base("GameObjectInspector", "OnSceneGUI") { }

    //     public override void OnInspectorGUI()
    //     {
    //         base.OnInspectorGUI(); // GameObjectInspector

    //         RZEditorSkills.DrawAllSkills();
    //     }

    // }


    ////////// 2 //////////////////////////////////////////////////////////////////////////////

    // [CustomEditor(typeof(GameObject))]
    // public class GameObjectEditor : Editor
    // {
    //     private System.Type inspectorType;
    //     private Editor editorInstance;
    //     private _MethodInfo defaultHeaderGUI;

    //     public GameObjectEditor()
    //     {
    //         inspectorType = System.Reflection.Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.GameObjectInspector");
    //         defaultHeaderGUI = inspectorType.GetMethod("OnHeaderGUI", 
    //             System.Reflection.BindingFlags.Instance |
    //             System.Reflection.BindingFlags.Public |
    //             System.Reflection.BindingFlags.NonPublic);
    //     }

    //     protected override void OnHeaderGUI()
    //     {
    //         editorInstance = Editor.CreateEditor(target, inspectorType);
    //         defaultHeaderGUI.Invoke(editorInstance, null);

    //         //custom GUI code here
    //         RZEditorSkills.DrawAllSkills();
    //     }

    //     public override void OnInspectorGUI()
    //     {

    //         // editorInstance.OnInspectorGUI();
    //     }

    // private void OnDisable()
    // {
    //     // When OnDisable is called, the default editor we created should be
    //     // destroyed to avoid memory leakage.
    //     // Also, make sure to call any required methods like OnDisable
    //     if (editorInstance != null)
    //     {
    //         MethodInfo disableMethod = editorInstance.GetType().GetMethod("OnDisable",
    //             BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    //         if (disableMethod != null)
    //             disableMethod.Invoke(editorInstance, null);

    //         DestroyImmediate(editorInstance);
    //     }
    // }

    ////////// 3 //////////////////////////////////////////////////////////////////////////////












    //     [CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
    //     public class GameObjectEditor : Editor
    //     {
    //         private System.Type inspectorType;
    //         private Editor editorInstance;
    //         private _MethodInfo defaultHeaderGUI;

    //         public GameObjectEditor()
    //         {

    //             inspectorType = System.Reflection.Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.GameObjectInspector");
    //             defaultHeaderGUI = inspectorType.GetMethod("OnHeaderGUI",
    //             System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);


    //         }

    //         void OnEnable()
    //         {
    //             editorInstance = Editor.CreateEditor(target, inspectorType);
    //         }

    //         protected override void OnHeaderGUI()
    //         {
    //             // Debug.Log("target: " + target);
    //             defaultHeaderGUI.Invoke(editorInstance, null);

    //             //custom GUI code here
    //             RZEditorSkills.DrawAllSkills();
    //         }

    //         public override void OnInspectorGUI() { }

    //         // private bool destroyed = false;
    //         private void OnDisable()
    //         {

    //         //     // When OnDisable is called, the default editor we created should be
    //         //     // destroyed to avoid memory leakage.
    //         //     // Also, make sure to call any required methods like OnDisable
    //         //     if (editorInstance != null)
    //         //     {
    //         //         MethodInfo disableMethod = editorInstance.GetType().GetMethod("OnDisable",
    //         //             BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    //         //         if (disableMethod != null)
    //         //             disableMethod.Invoke(editorInstance, null);

    //         //         // if (!destroyed)
    //         //         // {
    //         //         //     destroyed = true;
    //         //         //     DestroyImmediate(editorInstance);
    //         //         //     //                 DestroyImmediate(this);
    //         //         // }

    //         //     }
    //         }

    // }




















    ////////// 4 //////////////////////////////////////////////////////////////////////////////

#endif

}