using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    [RequireComponent(typeof(InspectorTools))]
    public class RelativeTime : MonoBehaviour
    {
        public Relative relativeFrom = Relative.RealStartApp;
        public enum Relative
        {
            Selected,             // DateTime.Now (from selectedDateTime)
            StartApp,             // UnityEngine.Time.Time
            RealStartApp,         // UnityEngine.Time.realtimeSinceStartup
            RealStartOS,          // SystemUptime.GetSeconds()
            UnixZero              // DateTime.Now (from 1970, 1, 1, 0, 0, 0, 0)
        }


        [HideInInspector]
        public string selectedDateTime = DateTime.Now.ToString(Tools.DATE_TIME_FORMAT);

        public double GetSeconds()
        {
            return GetSeconds(relativeFrom, Tools.ToDateTime(selectedDateTime));
        }

        public double GetSeconds(DateTime selected)
        {
            selectedDateTime = selected.ToString(Tools.DATE_TIME_FORMAT);
            return GetSeconds(relativeFrom, selected);
        }

        public static double GetSeconds(Relative from, DateTime dateTime = new DateTime())
        {
            switch (from)
            {
                default:
                case Relative.Selected: return GetSeconds_Selected(dateTime);
                case Relative.StartApp: return GetSeconds_StartApp();
                case Relative.RealStartApp: return GetSeconds_RealStartApp();
                case Relative.RealStartOS: return GetSeconds_RealStartOS();
                case Relative.UnixZero: return GetSeconds_UnixZero();
            }
        }

        public static double GetSeconds_Selected(DateTime selected) { return new TimeSpan(DateTime.Now.Ticks - selected.Ticks).TotalSeconds; }
        public static float GetSeconds_StartApp() { return Time.time; }
        public static float GetSeconds_RealStartApp() { return Time.realtimeSinceStartup; }
        public static float GetSeconds_RealStartOS() { return SystemUptime.WholeSeconds; }
        public static double GetSeconds_UnixZero() { return new TimeSpan(DateTime.Now.Ticks - Tools.UNIX_ZERO_TICKS).TotalSeconds; }
    }
}