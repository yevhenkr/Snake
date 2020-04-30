using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    /// <summary>
    /// This framework global values
    /// </summary>
    public static class Framework
    {
        /// <summary>
        /// Name of this framework
        /// </summary>
        public const string name = "RZ";

        /// <summary>
        /// Main color of framework icons
        /// </summary>
        public static readonly Color iconColor = new Color().RZ_From255(0, 150, 50, 255);
    }
}