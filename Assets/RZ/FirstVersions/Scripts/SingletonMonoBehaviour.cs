// RZ.SingletonMonoBehaviour 4.0

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    // public abstract class SingletonMonoBehaviour<T> : RZ.MonoBehaviourExtended where T : MonoBehaviour
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviourExtended where T : MonoBehaviour
    {
        /// <summary>
        /// NOT-SAFE Return the instance of MonoBehaviour.
        /// </summary>
        public static T instance;
        // static T instance;

        /// <summary>
        /// SAFE Return the instance of MonoBehaviour.
        /// </summary>
        // public static T Instance { get { return GetInstance(); } }

        /// <summary>
        /// SAFE Return the instance of MonoBehaviour.
        /// </summary>
        public static T GetInstance()
        {
            if (instance == null) CreateInstance();
            return instance;
        }

        /// <summary>
        /// Creating the instance of MonoBehaviour.
        /// </summary>
        public static void CreateInstance() { instance = (T)FindObjectOfType(typeof(T)); }
    }
}
