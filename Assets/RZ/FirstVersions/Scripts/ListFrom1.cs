using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// List begin from 1:
namespace RZ
{
    [System.Serializable]
    public class ListExtendedFrom1<T> : ListExtended<T>
    // , ISerializationCallbackReceiver
    {
        new public T this[int index]
        {
            get
            {
                index = index - 1;
                return base[index];
            }
            set
            {
                base.Add(value);
            }
        }

        override public string ToString()
        {
            string s = "";
            for (int i = 1; i <= this.Count; i++)
            {
                s += this[i].ToString() + ",";
            }
            s.Remove(s.Length - 1, 1);
            return "[" + s + "]";
        }

        // [SerializeField]
        // private List<T> items = new List<T>();

        // public void OnBeforeSerialize()
        // {
        //     items.Clear();
        //     items.AddRange(this);
        // }

        // public void OnAfterDeserialize()
        // {
        //     this.Clear();
        //     this.AddRange(items);

        //     // Освободить память:
        //     items.Clear();
        // }
    }
}