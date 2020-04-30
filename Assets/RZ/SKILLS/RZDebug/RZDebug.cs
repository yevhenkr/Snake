namespace RZ
{
    using UnityEngine;

    /// <summary>
    /// Extend UnityEngine.Debug.
    /// </summary>
    public partial class RZDebug : Debug
    {

        private static StackTraceLogType _traceLog =
            Application.GetStackTraceLogType(LogType.Log);
        private static StackTraceLogType _traceError =
            Application.GetStackTraceLogType(LogType.Error);
        private static StackTraceLogType _traceException =
            Application.GetStackTraceLogType(LogType.Exception);
        private static StackTraceLogType _traceWarning =
            Application.GetStackTraceLogType(LogType.Warning);
        private static StackTraceLogType _traceAssert =
            Application.GetStackTraceLogType(LogType.Assert);


        /// <summary>
        /// Is StackTraceLogType.None for some log types (is no stack trace in log).
        /// </summary>
        public static bool IsEnabledAllStackTrace()
        {
            if (_traceLog == StackTraceLogType.None) return false;
            if (_traceError == StackTraceLogType.None) return false;
            if (_traceException == StackTraceLogType.None) return false;
            if (_traceWarning == StackTraceLogType.None) return false;
            if (_traceAssert == StackTraceLogType.None) return false;
            return true;
        }


        /// <summary>
        /// Disable stack trace in log.
        /// </summary>
        public static void DisableAllStackTrace()
        {
            if (_traceLog != StackTraceLogType.None)
            {
                _traceLog = Application.GetStackTraceLogType(LogType.Log);
                Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            }

            if (_traceError != StackTraceLogType.None)
            {
                _traceError = Application.GetStackTraceLogType(LogType.Error);
                Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.None);
            }

            if (_traceException != StackTraceLogType.None)
            {
                _traceException = Application.GetStackTraceLogType(LogType.Exception);
                Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.None);
            }

            if (_traceWarning != StackTraceLogType.None)
            {
                _traceWarning = Application.GetStackTraceLogType(LogType.Warning);
                Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
            }

            if (_traceAssert != StackTraceLogType.None)
            {
                _traceAssert = Application.GetStackTraceLogType(LogType.Assert);
                Application.SetStackTraceLogType(LogType.Assert, StackTraceLogType.None);
            }
        }


        /// <summary>
        /// Restore stack trace in log from project settings.
        /// </summary>
        public static void RestoreAllStackTrace()
        {
            if (_traceLog != StackTraceLogType.None)
            { Application.SetStackTraceLogType(LogType.Log, _traceLog); }

            if (_traceError != StackTraceLogType.None)
            { Application.SetStackTraceLogType(LogType.Error, _traceError); }

            if (_traceException != StackTraceLogType.None)
            { Application.SetStackTraceLogType(LogType.Exception, _traceException); }

            if (_traceWarning != StackTraceLogType.None)
            { Application.SetStackTraceLogType(LogType.Warning, _traceWarning); }

            if (_traceAssert != StackTraceLogType.None)
            { Application.SetStackTraceLogType(LogType.Assert, _traceAssert); }
        }


        /// <summary>
        /// Enable stack trace in log from project settings.
        /// </summary>
        [System.Obsolete("Use 'RestoreAllStackTrace()' instead.")]
        public static void EnableAllStackTrace() { RestoreAllStackTrace(); }

    }
}
