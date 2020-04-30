using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.JSON
{
    public static class JSONTools
    {
        public static T[] JsonStringToArray<T>(string jsonString)
        {
            return JsonArrayHelper.FromJson<T>(FixJsonString(jsonString));
        }

        public static T[][] JsonStringToArray2<T>(string jsonString)
        {
            JSONObject jo = JSONObject.Parse(FixJsonString(jsonString));
            JSONArray ja = jo.GetArray("Items");
            T[][] array = new T[ja.Length][];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = JsonStringToArray<T>(FixJsonString(ja[i].ToString()));
            }
            return array;
        }

        public static string FixJsonString(string s)
        {
            s = s.Trim();
            if (s.StartsWith("["))
            {
                s = "{\"Items\":" + s + "}";
            }
            return s;
        }
    }





    public static class JsonArrayHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }






        // public static T[] FromJson<T>(string json)
        // {
        //     Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        //     return wrapper.Items;
        // }

        // public static string ToJson<T>(T[] array)
        // {
        //     Wrapper<T> wrapper = new Wrapper<T>();
        //     wrapper.Items = array;
        //     return JsonUtility.ToJson(wrapper);
        // }

        // public static string ToJson<T>(T[] array, bool prettyPrint)
        // {
        //     Wrapper<T> wrapper = new Wrapper<T>();
        //     wrapper.Items = array;
        //     return JsonUtility.ToJson(wrapper, prettyPrint);
        // }

        // [Serializable]
        // private class Wrapper<T>
        // {
        //     public T[] Items;
        // }





    }
}