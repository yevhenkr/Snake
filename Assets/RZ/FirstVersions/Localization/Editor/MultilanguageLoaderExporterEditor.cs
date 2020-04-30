using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace RZ.Localizations
{
    [CustomEditor(typeof(MultilanguageLoaderExporter))]

    public class MultilanguageLoaderExporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var thisObject = (MultilanguageLoaderExporter)target;

            if (GUILayout.Button("Load From Source"))
            {
                thisObject.LoadFromSource();
            }

#if !UNITY_WEBPLAYER
            if (GUILayout.Button("Export Text Asset"))
            {
                thisObject.ExportTextAsset();
            }
#endif
        }
    }
}
#endif
