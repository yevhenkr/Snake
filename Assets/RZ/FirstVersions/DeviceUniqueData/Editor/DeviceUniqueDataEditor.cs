namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;


    [CustomEditor(typeof(DeviceUniqueData)), CanEditMultipleObjects]
    public class DeviceUniqueDataEditor : Editor
    {

        private DeviceUniqueData me;


        private void OnEnable()
        { me = (DeviceUniqueData)target; }


        public override void OnInspectorGUI()
        { DrawDeviceData(); }


        private const int mbw = 50;
        private void DrawDeviceData()
        {
            EditorGUILayout.LabelField("Device Info", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Real", GUILayout.MinWidth(mbw)))
                DeviceUniqueData.UpdateDeviceInfo();

            if (GUILayout.Button("Rnd", GUILayout.MinWidth(mbw)))
            {
                string I = "\"";

                string date =
                //     System.DateTime.Now.ToString(DateTimeController.DATE_TIME_FORMAT);

                // DeviceUniqueData.device_data =
                    "{"
                    + I + "cpu" + I + ":" + I + "random_cpu" + I + ","
                    + I + "os" + I + ":" + I + "random_os" + I + ","
                    + I + "app_ver" + I + ":" + I + "random_app_ver" + I + ","
                    // + I + "date" + I + ":" + I + date + I
                    + "}";

                DeviceUniqueData.app_hash = DeviceUniqueData.GetRandomHash();
            }

            if (GUILayout.Button("Unk", GUILayout.MinWidth(mbw)))
            {
                DeviceUniqueData.app_hash = "unknown";
                DeviceUniqueData.device_data = "unknown";
            }

            if (GUILayout.Button("\"\"", GUILayout.MinWidth(mbw)))
            {
                DeviceUniqueData.app_hash = "";
                DeviceUniqueData.device_data = "";
            }

            EditorGUILayout.EndHorizontal();

            // DEVICE INFO HELPBOX:
            var tStyle = new GUIStyle(EditorStyles.helpBox);
            tStyle.richText = true;
            EditorGUILayout.TextArea(
                "<b>app_hash    :  </b>" + DeviceUniqueData.app_hash + "\n\n" +
                "<b>device_data :  </b>" + DeviceUniqueData.device_data + "\n\n" +
                "<b>app_hash_ok :  </b>" + DeviceUniqueData.app_hash_ok, tStyle);
        }
    }

#endif

}