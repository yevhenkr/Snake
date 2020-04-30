using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RZ
{

    [Serializable]
    public class StringStringDictionary : DictionaryExtended<string, string> { }

    [Serializable]
    public class StringColorDictionary : DictionaryExtended<string, Color> { }

    [Serializable]
    public class ObjectColorDictionary : DictionaryExtended<UnityEngine.Object, Color> { }

    [Serializable]
    public class ColorArrayStorage : DictionaryExtended.Storage<Color[]> { }

    [Serializable]
    public class StringColorArrayDictionary : DictionaryExtended<string, Color[], ColorArrayStorage> { }

    [Serializable]
    public class MyClass
    {
        public int i;
        public string str;
    }

    [Serializable]
    public class QuaternionMyClassDictionary : DictionaryExtended<Quaternion, MyClass> { }
}