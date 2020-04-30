namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;


    // [CustomEditor(typeof(Image)), CanEditMultipleObjects]
    [CustomEditor(typeof(Image), true)]
    // public class ImageEditor : RZDecoratorEditor
    public class ImageEditor : UnityEditor.UI.ImageEditor
    {

        // public ImageEditor() : base("ImageEditor") { }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // GameObjectInspector

            Debug.Log("ZZZZZZZZZZZZZZZZZZZZZzz");
            RZEditorSkills.SkillsButton();
        }

    }

#endif

}