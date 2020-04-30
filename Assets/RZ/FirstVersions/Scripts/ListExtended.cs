using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// List begin from 1:
namespace RZ
{
    [System.Serializable]
    public class ListExtended<T> : List<T>, ISerializationCallbackReceiver
    {
        // new public T this[int index]
        // {
        //     get
        //     {
        //         index--;
        //         return base[index];
        //     }
        //     set
        //     {
        //         base.Add(value);
        //     }
        // }

        override public string ToString()
        {
            string s = "";
            for (int i = 0; i <= this.Count; i++)
            {
                s += this[i].ToString() + ",";
            }
            s.Remove(s.Length - 1, 1);
            return "[" + s + "]";
        }

        [SerializeField]
        private List<T> items = new List<T>();

        public void OnBeforeSerialize()
        {
            items.Clear();
            items.AddRange(this);
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            this.AddRange(items);

            // Освободить память:
            items.Clear();
        }
    }
}