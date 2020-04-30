using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace RZ
{
    public class FixNumberCommaToDot : MonoBehaviour
    {
        void Awake()
        {
            ////////// FIX PARSING DOUBLE WITH "," : //////////
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        }
    }
}