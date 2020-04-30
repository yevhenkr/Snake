using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif


[ExecuteInEditMode]
// [ExecuteAlways]
public class RZTestSceneLoader : MonoBehaviour
{
    [Header("Controls:")]
    public string testSceneName = "RZTestScene";
    [Header("Read Only:")]
    public string testScenePath;
    public bool sceneFinded = false;
    public bool canLoad = false;

    // [HideInInspector] public Scene testScene;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Debug.Log(gameObject.activeInHierarchy);
    }
#endif

    private void Start()
    {
#if !UNITY_EDITOR
        LoadTestScene(true);
#endif
    }

    private void OnEnable()
    {
        EnableTestScene(true);
        RZTestCore.ShowHideTestCanvas(true);
    }

    private void OnDisable()
    {
        EnableTestScene(false);
        RZTestCore.ShowHideTestCanvas(false);
        LoadTestScene(false);
    }


    public void EnableTestScene(bool enable)
    {
        if (!Application.isPlaying)
        {
#if UNITY_EDITOR
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            foreach (var scene in scenes)
            {
                if (scene.path.EndsWith(testSceneName + ".unity"))
                {
                    sceneFinded = true;
                    scene.enabled = enable;
                    testScenePath = scene.path;
                    canLoad = sceneFinded && enabled;
                }
            }
            EditorBuildSettings.scenes = scenes;
#endif

        }
    }

    public void LoadTestScene(bool enable)
    {
        if (Application.isPlaying)
        {
            if (canLoad)
            {
                Scene testScene = SceneManager.GetSceneByPath(testScenePath);
                if (testScene == null) return;
                if (enable)
                {
                    if (!testScene.isLoaded && !RZTestCore.exists)
                    {
                        SceneManager.LoadScene(testScenePath);
                    }
                    // SceneManager.LoadScene(testScenePath, LoadSceneMode.Additive);
                }
                else
                {
                    if (testScene.isLoaded)
                        SceneManager.UnloadSceneAsync(testScenePath);
                }
            }
        }
    }


}
