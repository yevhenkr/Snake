namespace RZ
{
    using System.Reflection;
    using UnityEngine;

    public partial class RZDebug : UnityEngine.Debug
    {

        /// <summary>
        /// Log message to console without stack trace.
        /// </summary>
        public static void LogNoTrace(string message)
        { Debug.LogFormat(LogType.Log, LogOption.NoStacktrace, null, message); }


        /// <summary>
        /// Log assertion message to console without stack trace.
        /// </summary>
        public static void LogAssertionNoTrace(string message)
        { Debug.LogFormat(LogType.Assert, LogOption.NoStacktrace, null, message); }


        /// <summary>
        /// Log error message to console without stack trace.
        /// </summary>
        public static void LogErrorNoTrace(string message)
        { Debug.LogFormat(LogType.Error, LogOption.NoStacktrace, null, message); }


        /// <summary>
        /// Log exception to console without stack trace.
        /// </summary>
        public static void LogExceptionNoTrace(System.Exception exception)
        { Debug.LogFormat(LogType.Exception, LogOption.NoStacktrace, null, exception.Message); }


        /// <summary>
        /// Log warning message to console without stack trace.
        /// </summary>
        public static void LogWarningNoTrace(string message)
        { Debug.LogFormat(LogType.Warning, LogOption.NoStacktrace, null, message); }

    }
}
