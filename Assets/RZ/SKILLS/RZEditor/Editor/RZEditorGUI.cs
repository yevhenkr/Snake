namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;


    /// <summary>
    /// Contains GUI Controls by RZ.
    /// </summary>
    public partial class RZEditorGUI
    {
        /// <summary>
        /// Contains GUI options like inner colors, styles, etc...
        /// </summary>
        public static Options options = new Options();

        /// <summary>
        /// Contains GUI options like inner colors, styles, etc...
        /// </summary>
        public partial class Options : RZOptions
        {

        }

    }

#endif

}