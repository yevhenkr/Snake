﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace RZ
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif

    public class MetafileTools
    {
        static MetafileTools()
        {
            // Debug.Log("Up and running");
        }

        // public static T[] GetAtPath<T>(string path)
        // {

        //     ArrayList al = new ArrayList();
        //     string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);

        //     foreach (string fileName in fileEntries)
        //     {
        //         int assetPathIndex = fileName.IndexOf("Assets");
        //         string localPath = fileName.Substring(assetPathIndex);

        //         Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

        //         if (t != null)
        //             al.Add(t);
        //     }
        //     T[] result = new T[al.Count];
        //     for (int i = 0; i < al.Count; i++)
        //         result[i] = (T)al[i];

        //     return result;
        // }
    }
}