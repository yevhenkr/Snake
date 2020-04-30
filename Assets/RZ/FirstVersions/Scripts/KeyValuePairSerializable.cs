using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    [System.Serializable]
    public struct KeyValuePairSerializable<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }
}