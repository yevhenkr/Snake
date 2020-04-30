namespace RZ
{

    using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

    public partial class InspectorTools : MonoBehaviour
    {
#if UNITY_EDITOR

        [System.NonSerialized]
        public bool forceRepaint = false;

        // public void OnEnable()
        public void Awake()
        { forceRepaint = false; }

#endif
    }

}