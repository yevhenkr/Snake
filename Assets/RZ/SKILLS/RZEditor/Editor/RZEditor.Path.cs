namespace RZ
{
    using UnityEngine;

#if UNITY_EDITOR

    public partial class RZEditor
    {

        /// <summary>
        /// Return path of current project.
        /// </summary>
        public static string GetProjectPath()
        { return System.IO.Directory.GetCurrentDirectory().Replace("\\", "/"); }


        /// <summary>
        /// Return path of Assets.
        /// </summary>
        public static string GetAssetsPath()
        { return Application.dataPath; }


        /// <summary>
        /// Return path of downloaded packages cache.
        /// </summary>
        public static string GetDownloadedPackagesPath()
        { return Application.dataPath.Replace("Assets", "Library/PackageCache"); }

    }

#endif
}