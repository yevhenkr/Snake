#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class CountSelectedObjects : EditorWindow
{
    [MenuItem("RZ/Count selected objects")]
    public static void ShowWindow()
    {
        //Создать инстанс окна
        EditorWindow.GetWindow(typeof(CountSelectedObjects));
    }

    //Обрабатываем код окна
    void OnGUI()
    {
        if (GUILayout.Button("Count objects"))
        {
            Debug.Log(Selection.GetTransforms(SelectionMode.TopLevel).Length);
        }
    }
}
#endif