using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public static class ApplicationTools
    {
        public static void QuitApp()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}