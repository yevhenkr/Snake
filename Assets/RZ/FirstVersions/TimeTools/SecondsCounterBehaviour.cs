using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    [RequireComponent(typeof(InspectorTools))]
    /// <summary>
    /// Time counter in seconds.
    /// For using as component.
    /// Not used Update() or FixedUpdate().
    /// Not tied to game object.
    /// Not tied to frame rate.
    /// Uses time-cheat protection.
    /// </summary>

    [ExecuteInEditMode]
    public class SecondsCounterBehaviour : MonoBehaviour
    {
        [Tooltip("Reset count on enable script or gameObject.")]
        public bool autoReset;
        /// <summary>
        /// Reset count on enable script or gameObject.
        /// </summary>

        [Tooltip("Start count on enable script or gameObject.")]
        public bool autoStart = false;

        [Tooltip("Stop count on disable script or gameObject.")]
        public bool autoStop = false;

        [Tooltip("true:  Do not stop count on application pause.\nfalse: Pause count on application pause and continue on application resume.")]
        public bool ignoreAppPause = true;

        [Tooltip("true:  Do not stop count on application lost focus.\nfalse: Pause count on application lost focus and continue on application focus.")]
        public bool ignoreFocusLost = true;

        [Tooltip("Inner time counter.")]
        public SecondsCounter counter = new SecondsCounter();

        void OnEnable()
        {
            if (autoReset) counter.Reset();
            if (autoStart) counter.Start();
        }

        void OnDisable() { if (autoStop) counter.Stop(); }

        void OnApplicationPause(bool p) { if (!ignoreAppPause) SetPause(p); }

        void OnApplicationFocus(bool f) { if (!ignoreFocusLost) SetPause(!f); }

        bool _isPause;
        void SetPause(bool b)
        {
            if (b)
            {
                counter.Stop();
            }
            else
            {
                if (_isPause) counter.Start();
            }
            _isPause = b;
        }

        /// <summary>
        /// Values to string.
        /// </summary>
        public override string ToString() { return base.ToString() + JsonUtility.ToJson(this); }
    }
}