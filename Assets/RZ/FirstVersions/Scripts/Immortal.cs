// RZ.Immortal 2.0

using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class Immortal : MonoBehaviour
    {
        private static HashSet<Object> _immortals = new HashSet<Object>();
        public static HashSet<Object> immortals { get { return _immortals; } }

        void Awake()
        {
            Add(this.gameObject);
        }

        public static void Add(Object o)
        {
            if (o != null && !_immortals.Contains(o))
            {
                _immortals.Add(o);
                MonoBehaviour.DontDestroyOnLoad(o);
            }
        }

        public static void Remove(Object o)
        {
            if (o != null)
            {
                if (_immortals.Contains(o)) _immortals.Remove(o);
                MonoBehaviour.Destroy(o);
            }
        }

        public static void RemoveAll()
        {
            foreach (Object o in _immortals)
            {
                MonoBehaviour.Destroy(o);
            }
            _immortals.Clear();
        }
    }
}