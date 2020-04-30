using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class WebTools
    {
        public static string FixStringForURL(string s)
        {
            return UnityEngine.Networking.UnityWebRequest.EscapeURL(s)
                .Replace("+", "%20")
                .Replace(" ", "%20");
        }
    }
}
