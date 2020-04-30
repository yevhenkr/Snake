using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RZ.TimeTools
{
    /// <summary>
    /// Time counter in seconds.
    /// For using from any script.
    /// Not used Update() or FixedUpdate().
    /// Not tied to game object.
    /// Not tied to frame rate.
    /// Uses time-cheat protection.
    /// </summary>
    [System.Serializable]
    public class SecondsCounter
    {
        /// <summary>
        /// Relative timer to UnityEngine.Time.realtimeSinceStartup or to UnityEngine.Time.Time.
        /// UnityEngine.Time.realtimeSinceStartup - ignore game pause/
        /// Set time relative before start timer!
        /// </summary>
        [Tooltip("Set time relative before start timer!")]
        [SerializeField]
        Relative timeRelative = Relative.AppRealtime_IgnoreGamePause;
        public enum Relative
        {
            AppRealtime_IgnoreGamePause,      // UnityEngine.Time.realtimeSinceStartup
            AppTime_PauseOnGamePause,         // UnityEngine.Time.Time
        }

        public Relative GetTimeRelative()
        {
            return timeRelative;
        }

        public void SetTimeRelative(Relative timeRelative)
        {
            if (IsStarted())
            {
                float n = Now();
                savedSeconds += n - timeMarker;
                this.timeRelative = timeRelative;
                timeMarker = Now();
            }
            else
            {
                this.timeRelative = timeRelative;
            }
        }

        float Now()
        {
            switch (timeRelative)
            {
                default:
                case Relative.AppRealtime_IgnoreGamePause: return Time.realtimeSinceStartup;
                case Relative.AppTime_PauseOnGamePause: return Time.time;
            }
        }

        [SerializeField] float savedSeconds;
        [SerializeField] float timeMarker;

        /// <summary>
        /// New time counter.
        /// </summary>
        public SecondsCounter() : this(0, false, Relative.AppRealtime_IgnoreGamePause) { }

        /// <summary>
        /// New time counter.
        /// </summary>
        public SecondsCounter(float setSeconds = 0, bool startNow = false) : this(setSeconds, startNow, Relative.AppRealtime_IgnoreGamePause) { }

        /// <summary>
        /// New time counter.
        /// </summary>
        public SecondsCounter(bool startNow = false) : this(0, startNow, Relative.AppRealtime_IgnoreGamePause) { }

        /// <summary>
        /// New time counter.
        /// </summary>
        public SecondsCounter(float setSeconds = 0, bool startNow = false, Relative timeRelative = Relative.AppRealtime_IgnoreGamePause)
        {
            this.timeRelative = timeRelative;
            savedSeconds = setSeconds;
            if (startNow) timeMarker = Now();
        }

        /// <summary>
        /// Get time in seconds.
        /// </summary>
        public float GetSeconds()
        {
            if (IsStarted())
            {
                float n = Now();
                // savedSeconds += n - timeMarker;
                // timeMarker = n;
                // return savedSeconds;

                return savedSeconds + n - timeMarker;
            }
            else
            {
                return savedSeconds;
            }

        }

        /// <summary>
        /// Set time in seconds.
        /// </summary>
        public void SetSeconds(float value)
        {
            if (IsStarted())
            {
                savedSeconds = value;
                timeMarker = Now();
            }
            else
            {
                savedSeconds = value;
            }
        }

        /// <summary>
        /// Is time counter running now.
        /// </summary>
        public bool IsStarted()
        {
            return timeMarker != 0;
        }

        /// <summary>
        /// Start/Stop time counter.
        /// </summary>
        public void SetStarted(bool value)
        {
            if (value)
            {
                if (timeMarker == 0)
                {
                    timeMarker = Now();
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Timer already started!");
                }
            }
            else
            {
                if (timeMarker != 0)
                {
                    savedSeconds += (Now() - timeMarker);
                    timeMarker = 0;
                }
                else
                {
                    UnityEngine.Debug.LogWarning("Timer already stopped!");
                }
            }
        }



        ///<summary>
        /// Reset counter without stopping.
        ///</summary>
        public void Reset() { SetSeconds(0); }

        /// <summary>
        /// Start the time counting.
        /// </summary>
        public void Start() { SetStarted(true); }

        /// <summary>
        /// Stop the time counting.
        /// </summary>
        public void Stop() { SetStarted(false); }

        /// <summary>
        /// Values to string.
        /// </summary>
        public override string ToString() { return base.ToString() + JsonUtility.ToJson(this); }


    }
}
