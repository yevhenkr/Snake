using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class BoolTools
    {
        /// <summary>
        /// bool to 0 or 1:
        /// false = 0
        /// true = 1  
        /// </summary>
        public static int BoolToInt(bool b)
        {
            return b ? 1 : 0;
        }
    }
}

