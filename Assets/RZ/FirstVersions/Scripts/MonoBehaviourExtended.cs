using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
#if UNITY_EDITOR
    using UnityEditor;
    using System.IO;
    using System;

    public class MonoBehaviourExtendedEditor : Editor
    {
        [MenuItem("Assets/Create/C# Extended Script")]
        static void CreateScript()
        {
            var selected = Selection.activeObject;
            string path = selected == null ? "Assets" : AssetDatabase.GetAssetPath(selected.GetInstanceID());

            if (path.Length > 0)
            {
                if (!Directory.Exists(path)) path = Path.GetDirectoryName(path);
            }
            else
            {
                Debug.Log("Not in assets folder");
                return;
            }

            // remove whitespace and minus
            // string name = selected.name.Replace(" ", "_").Replace("-", "_");

            string scriptName = "NewExtendedScript";
            string scriptExtension = ".cs";
            string targetClassName = "RZ.MonoBehaviourExtended";

            string copyPath = path + "/" + scriptName + scriptExtension;

            if (!File.Exists(copyPath))
            {
                // do not overwrite
                using (StreamWriter outfile = new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using UnityEngine;");
                    outfile.WriteLine("using System.Collections;");
                    outfile.WriteLine("");
                    outfile.WriteLine("public class " + scriptName + " : " + targetClassName + " {");
                    outfile.WriteLine(" ");
                    // outfile.WriteLine(" // Use this for initialization");
                    outfile.WriteLine(" void Start () {");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" }");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" ");
                    // outfile.WriteLine(" // Update is called once per frame");
                    outfile.WriteLine(" void Update () {");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" }");
                    outfile.WriteLine(" ");
                    outfile.WriteLine("}");
                }
            }
            AssetDatabase.Refresh();
        }
    }
#endif


    /// <summary>
    /// MonoBehaviour Extended !;
    /// </summary>
    public class MonoBehaviourExtended : MonoBehaviour
    {
        /// <summary>
        /// Invoke method;
        /// </summary>
        public void Invoke(System.Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }

        /// <summary>
        /// InvokeRepeating method;
        /// </summary>
        public void InvokeRepeating(System.Action method, float time, float repeatRate)
        {
            InvokeRepeating(method.Method.Name, time, repeatRate);
        }

        // /// <summary>
        // /// InvokeRepeating method;
        // /// </summary>
        // // List<System.Action> methodNames = new List<System.Action>();
        // public void InvokeRepeating(System.Action method, float time, float repeatRate, int repeatCount)
        // {
        //     InvokeRepeating(method.Method.Name, time, repeatRate, repeatCount);
        // }

        // List<
        // /// <summary>
        // /// InvokeRepeating method;
        // /// </summary>
        // // List<System.Action> methodNames = new List<System.Action>();
        // public void InvokeRepeating(string methodName, float time, float repeatRate, int repeatCount)
        // {
        //     InvokeRepeating(RepeatingMethod(methodName, repeatCount), time);
        // }

        // int repeatings;
        // void RepeatingMethod(string methodName, int repeatCount)
        // {
        //     base.Invoke(methodName, 0);
        // }

        /// <summary>
        /// CancelInvoke method;
        /// </summary>
        public void CancelInvoke(System.Action method)
        {
            base.CancelInvoke(method.Method.Name);
        }

        // // Cancels all Invoke calls with name methodName on this behaviour.
        // public void CancelInvoke(string methodName)
        // {
        //     base.CancelInvoke(methodName);
        // }

        /// <summary>
        /// IsInvoking method;
        /// </summary>
        public bool IsInvoking(System.Action method)
        {
            return base.IsInvoking(method.Method.Name);
        }

    }

    // private class RepeatingMethod
    // {
    //     public RepeatingMethod(string method.Method.Name)
    // }
}

