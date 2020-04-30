using UnityEngine;
using System.Collections.Generic;
using System;

namespace RZ.SqLite
{
    /// <summary>
    /// The AsyncManager will moves things to another thread to process, then bring them back to main thread.
    /// Very useful for SQLite so you can send a save/whatever to another thread without blocking the main
    /// Unity thread.  This is a modified version of the Unity Gems Async.
    /// </summary>
    public class AsyncManager : MonoBehaviour
    {
        public interface IAsync
        {
            void QueueOnMainThread(Action action);
        }

        private static NullAsync _nullAsync = new NullAsync();
        private static AsyncDispatcher _async;

        public static IAsync Async
        {
            get
            {
                if (_async != null)
                {
                    return _async as IAsync;
                }
                return _nullAsync as IAsync;
            }
        }

        void Awake()
        {
            _async = new AsyncDispatcher();
        }

        void OnDestroy()
        {
            _async = null;
        }

        void Update()
        {
            if (Application.isPlaying)
            {
                _async.Update();
            }
        }

        private class NullAsync : IAsync
        {
            public void QueueOnMainThread(Action action) { }
        }

        private class AsyncDispatcher : IAsync
        {
            private readonly List<Action> actions = new List<Action>();
            public void QueueOnMainThread(Action action)
            {
                lock (actions)
                {
                    actions.Add(action);
                }
            }
            public void Update()
            {
                // Pop the actions from the synchronized list
                Action[] actionsToRun = null;
                lock (actions)
                {
                    actionsToRun = actions.ToArray();
                    actions.Clear();
                }
                // Run each action
                foreach (Action action in actionsToRun)
                {
                    action();
                }
            }
        }
    }
}