using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace RZ.TimeTools
{
    [RequireComponent(typeof(InspectorTools))]
    [Serializable]
    public class ProtectedTime : RZ.SingletonMonoBehaviour<ProtectedTime>, ISerializationCallbackReceiver
    {
        public const int DEFAULT_DEVIATION = 12; // default: 12 hours
        public const Accuracy DEFAULT_ACCURACY = Accuracy.Hours;
        public const sbyte DEFAULT_MAX_HISTORY = 100;

        //--------------------------------------------------------------------------------

        static DateTime _timeOfStart = Tools.UNIX_ZERO_DATE;
        public static DateTime timeOfStart
        {
            get
            {
                if (_timeOfStart == Tools.UNIX_ZERO_DATE) SetFromHistory();
                return _timeOfStart;
            }
            set
            {
                _timeOfStart = value;
            }
        }

        //--------------------------------------------------------------------------------

        public static Accuracy accuracy = DEFAULT_ACCURACY;

        public enum Accuracy
        {
            Hours,
            Minutes
            // ,
            // Seconds
        }

        public static long currentDeviation
        {
            get
            {
                var d = Get() - DateTime.Now;
                switch (accuracy)
                {
                    default:
                    case Accuracy.Hours: return (long)(Math.Round(d.TotalHours));
                    case Accuracy.Minutes: return (long)(Math.Round(d.TotalMinutes));
                        // case DMetric.Seconds: return (long)(Math.Round(d.TotalSeconds));
                }
            }
        }

        public static int allowedDeviation = DEFAULT_DEVIATION;

        //--------------------------------------------------------------------------------

        public static bool trustMe = false;
        static void CheckTrust()
        {
            if (history != null)
            {
                string tos = timeOfStart.ToString(Tools.DATE_TIME_FORMAT);
                tos = tos.Substring(0, tos.Length - 3);
                string htos = history.timeOfStart;
                htos = htos.Substring(0, htos.Length - 3);
                trustMe = tos == htos;
            }
            else
            {
                trustMe = false;
            }
        }

        public static bool rebootDetected
        {
            get
            {
                if (history == null)
                {
                    return true;
                }
                else
                {
                    return history.uptimeSeconds > SystemUptime.WholeSeconds;
                }
            }
        }

        //--------------------------------------------------------------------------------

        [HideInInspector] [SerializeField] [FormerlySerializedAs("accuracy")] Accuracy _accuracy = accuracy;
        [HideInInspector] [SerializeField] [FormerlySerializedAs("allowedDeviation")] int _allowedDeviation = allowedDeviation;
        [HideInInspector] [SerializeField] [FormerlySerializedAs("maxHistory")] sbyte _maxHistory = maxHistory;

        public void OnBeforeSerialize()
        {
            _accuracy = accuracy;
            _allowedDeviation = allowedDeviation;
            _maxHistory = maxHistory;
        }

        public void OnAfterDeserialize()
        {
            accuracy = _accuracy;
            allowedDeviation = _allowedDeviation;
            maxHistory = _maxHistory;
        }

        public static void ResetToDefaults()
        {
            SystemUptime.Reset();
            History.Clear();
            isCheatDetected = false;
            accuracy = DEFAULT_ACCURACY;
            allowedDeviation = DEFAULT_DEVIATION;
            timeOfStart = Tools.UNIX_ZERO_DATE;
            maxHistory = DEFAULT_MAX_HISTORY;
            accuracy = DEFAULT_ACCURACY;
            SetFromSystem();
        }

        void Reset() { ResetToDefaults(); }

        //--------------------------------------------------------------------------------

        public static bool isCheatDetected;
        public static bool IsCheatNow()
        {
            bool cheat = Math.Abs(currentDeviation) > Math.Abs(ProtectedTime.allowedDeviation);
            if (cheat) isCheatDetected = true;
            return cheat;
        }

        //--------------------------------------------------------------------------------

        public static void SetFromSystem()
        {
            timeOfStart = DateTime.Now.AddSeconds(-SystemUptime.WholeSeconds);
            CheckTrust();
        }

        public static void SetFromHistory()
        {
            History.Load();

            if (history != null)
            {
                switch (accuracy)
                {
                    default:
                    case Accuracy.Hours:
                        timeOfStart = DateTime.Now.AddSeconds(-SystemUptime.WholeSeconds).AddHours(history.OftenD);
                        break;
                    case Accuracy.Minutes:
                        timeOfStart = DateTime.Now.AddSeconds(-SystemUptime.WholeSeconds).AddMinutes(history.OftenD);
                        break;
                        // case Accuracy.Seconds:
                        //     timeOfStart = DateTime.Now.AddSeconds(-SystemUptime.WholeSeconds).AddSeconds(history.MaxD);
                        //     break;
                }
                CheckTrust();
            }
            else
            {
                SetFromSystem();
            }
        }


        public static void Set(string dateTime, bool showExceptions = true)
        {
            try
            {
                timeOfStart = Tools.ToDateTime(dateTime).AddSeconds(-SystemUptime.WholeSeconds);
                trustMe = true;
                History.Save(true);
            }
            catch (Exception ex)
            {
                if (showExceptions) Debug.LogWarning(dateTime + " : " + ex.Message.ToString());
            }
        }

        public static void Set(DateTime dateTime)
        {
            timeOfStart = dateTime.AddSeconds(-SystemUptime.WholeSeconds);
            trustMe = true;
            History.Save(true);
        }

        public static void Set(float timeStamp)
        {
            timeOfStart = Tools.ToDateTime(timeStamp).AddSeconds(-SystemUptime.WholeSeconds);
            trustMe = true;
            History.Save(true);
        }

        public static void Add(int time)
        {
            switch (accuracy)
            {
                default:
                case Accuracy.Hours:
                    timeOfStart = timeOfStart.AddHours(time); ;
                    break;
                case Accuracy.Minutes:
                    timeOfStart = timeOfStart.AddMinutes(time);
                    break;
                    // case Accuracy.Seconds:
                    //     timeOfStart = timeOfStart.AddSeconds(time);
                    //     break;
            }
            trustMe = true;
            History.Save(true);
        }

        public static DateTime Get()
        {
            return timeOfStart.AddSeconds(SystemUptime.WholeSeconds);
        }

        public static string GetString()
        {
            return Get().ToString(Tools.DATE_TIME_FORMAT);
        }

        //--------------------------------------------------------------------------------
        static sbyte maxH = DEFAULT_MAX_HISTORY;
        public static sbyte maxHistory
        {
            get { return maxH; }
            set
            {
                if (value <= 0)
                {
                    maxH = 0;
                    History.Clear();
                }
                else
                {
                    maxH = value;
                    if (history == null) history = new History();
                    else history.SetMax(maxH);
                }
            }
        }


        public static History history = null;
        [System.Serializable]
        public class PairDQ
        {
            public int dev = 0;
            public int quant = 0;
            public PairDQ(int d = 0, int q = 0) { dev = d; quant = q; }
        }

        [System.Serializable]
        public class History
        {
            public string timeOfStart = Tools.UNIX_ZERO_DATE.ToString(Tools.DATE_TIME_FORMAT);
            public float uptimeSeconds = 0;

            [SerializeField] int oftenD = 0; public int OftenD { get { return oftenD; } }
            [SerializeField] int maxQ = 0; public int MaxQ { get { return maxQ; } }

            [SerializeField] public List<PairDQ> stackDQ = new List<PairDQ>();

            public History() { stackDQ = new List<PairDQ>(); }

            public PairDQ GetPair_Dev_Quant(int index) { return stackDQ[index]; }

            public int Count { get { return stackDQ.Count; } }

            public void ClearStack() { stackDQ.Clear(); }

            public void SetMax(sbyte value)
            {
                if (stackDQ.Count > value) stackDQ.RemoveRange(value, stackDQ.Count - value);
            }

            public void Add(int d)
            {
                int q = 1;
                int index = -1;

                for (int i = 0; i < stackDQ.Count; i++)
                {
                    if (stackDQ[i].dev == d)
                    {
                        index = i;
                        q = stackDQ[index].quant + 1;
                        stackDQ.RemoveAt(index);
                        break;
                    }
                }

                if (index == -1 && stackDQ.Count >= maxHistory) stackDQ.RemoveAt(stackDQ.Count - 1);

                stackDQ.Insert(0, new PairDQ(d, q));

                if (q >= maxQ)
                {
                    oftenD = d;
                    maxQ = q;
                }
            }

            public const string P_PREFS_KEY = Framework.name + ".TimeTools.ProtectedTime.history";

            public static void Load()
            {
                if (maxHistory > 0)
                {
                    try
                    {
                        if (PlayerPrefs.HasKey(P_PREFS_KEY))
                        {
                            history = JsonUtility.FromJson<History>(PlayerPrefs.GetString(P_PREFS_KEY));
                        }
                        if (history != null) history.SetMax(maxHistory);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning(ex.Message.ToString());
                        Clear();
                    }
                }
            }

            public static void Save(bool addCurrentDeviation)
            {
                if (history != null)
                {
                    if (addCurrentDeviation) history.Add((int)currentDeviation);
                    history.timeOfStart = ProtectedTime.timeOfStart.ToString(Tools.DATE_TIME_FORMAT);
                    history.uptimeSeconds = SystemUptime.WholeSeconds;
                    PlayerPrefs.SetString(P_PREFS_KEY, JsonUtility.ToJson(history));
                    Debug.Log(JsonUtility.ToJson(history));
                }
            }

            public static void Clear()
            {
                if (history != null) ProtectedTime.history.ClearStack();

                if (maxHistory > 0)
                    history = new History();
                else history = null;

                if (PlayerPrefs.HasKey(P_PREFS_KEY)) PlayerPrefs.DeleteKey(P_PREFS_KEY);
            }
        }
    }
}