
namespace RZ
{
    using UnityEditor;

    public class AssetBundlesBuild : Editor
    {
        [MenuItem("RZ/Build Asset Bundles/BuildAndroid")]
        static void BuildAssetAndroid()
        {
            BuildPipeline.BuildAssetBundles("Assets/APPLICATION/Common/AB",
                BuildAssetBundleOptions.None, BuildTarget.Android);
        }

        [MenuItem("RZ/Build Asset Bundles/BuildIOS")]
        static void BuildAssetIOS()
        {
            BuildPipeline.BuildAssetBundles("Assets/APPLICATION/Common/AB",
                BuildAssetBundleOptions.None, BuildTarget.iOS);
        }

        [MenuItem("RZ/Build Asset Bundles/BuildWindows")]
        static void BuildAssetWindows()
        {
            BuildPipeline.BuildAssetBundles("Assets/APPLICATION/Common/AB",
                BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
}