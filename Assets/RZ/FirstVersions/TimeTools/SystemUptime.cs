using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    public static class SystemUptime
    {
        public static TimeSystem usedTimeSystem { get { return _usedTimeSystem; } }
        static TimeSystem _usedTimeSystem = TimeSystem.None;
        public enum TimeSystem
        {
            None,
            NativeOsUptime,
            AlternativeOsUptime,
            ApplicationUptime
        }

        public static bool isExceptions = false;
        static void AddShowWarning(Exception e, string tip)
        {
            isExceptions = true;
            Debug.LogWarning(Framework.name + ": " + "Fail on getting system uptime, but RELAX! We will to use alternative. " + tip + "\n" + e.ToString());
        }

        public static void Reset()
        {
            isExceptions = false;
            _usedTimeSystem = TimeSystem.None;
        }

        public static TimeSpan TimeSpan
        {
            get { return System.TimeSpan.FromSeconds(WholeSeconds); }
        }

        public static long WholeSeconds
        {
            get { return (long)TotalSeconds; }
        }

        public static float TotalSeconds
        {
            get
            {
                if (_usedTimeSystem == TimeSystem.None)
                {
                    try
                    {
                        if (Application.platform != RuntimePlatform.WindowsEditor
                            && Application.platform != RuntimePlatform.WindowsPlayer)
                            _usedTimeSystem = TimeSystem.NativeOsUptime;

                        return NATIVE;
                    }
                    catch (Exception e)
                    {
                        _usedTimeSystem = TimeSystem.ApplicationUptime;
                        AddShowWarning(e, "[Trying to use Time.realtimeSinceStartup]");
                        return APP;
                    }
                }
                else
                {
                    if (_usedTimeSystem == TimeSystem.NativeOsUptime)
                        return NATIVE;
                    else
                        return APP;
                }
            }
        }


#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        [System.Runtime.InteropServices.DllImport("kernel32")]
        extern static System.UInt64 GetTickCount64();
        static float WIN64 { get { return GetTickCount64() * 0.001f; } }

        static float NATIVE
        {
            get
            {
                if (_usedTimeSystem == TimeSystem.None)
                {
                    try
                    {
                        _usedTimeSystem = TimeSystem.NativeOsUptime;
                        return WIN64;
                    }
                    catch (Exception e)
                    {
                        AddShowWarning(e, "[Trying to use System.Environment.TickCount]");
                        _usedTimeSystem = TimeSystem.AlternativeOsUptime;
                        return ALT;
                    }
                }
                else
                {
                    if (_usedTimeSystem == TimeSystem.NativeOsUptime)
                        return WIN64;
                    else
                        return ALT;
                }
            }
        }

#elif UNITY_ANDROID
        static AndroidJavaObject jo = new AndroidJavaObject("android.os.SystemClock");
        static float NATIVE
        {
            get
            {
                long milliseconds = jo.CallStatic<long>("elapsedRealtime");
                return (float) milliseconds * 0.001f;
            }
        }
		
#elif UNITY_IPHONE || UNITY_IOS
        [System.Runtime.InteropServices.DllImport ("__Internal")]
	    extern static float GetSystemUptime();
	    static float NATIVE { get { return GetSystemUptime(); } }

#else
	    static float NATIVE { get { return ALT; } }
#endif

        static float ALT { get { return System.Environment.TickCount * 0.001f; } }

        public static float APP { get { return Time.realtimeSinceStartup; } }
    }
}