
using UnityEngine;
using UnityEngine.UI;

public class RZTestCore : MonoBehaviour
{
    public static bool exists = false;
    public static RZTestCore instance = null;
    public RZTestCore() { instance = this; }

    public RZTestCanvas rzTestCanvas;
    public RZTestButton rzTestButton;
    public RZTestUI rzTestUI;

    public void Reset() { ReInit(); }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (rzTestCanvas == null || rzTestButton == null || rzTestUI == null) ReInit();
    }
#endif

    public void ReInit()
    {
        rzTestCanvas = GetComponentInChildren<RZTestCanvas>(true);
        rzTestUI = GetComponentInChildren<RZTestUI>(true);
        rzTestButton = GetComponentInChildren<RZTestButton>(true);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        exists = true;
        rzTestCanvas.gameObject.SetActive(true);
        rzTestUI.gameObject.SetActive(false);
        rzTestButton.gameObject.SetActive(true);
        rzTestButton.GetComponent<Button>().onClick.AddListener(ShowHideTestUI);
    }

    private void OnDestroy()
    {
        exists = false;
    }

    public void ShowHideTestUI()
    {
        if (rzTestUI != null)
            rzTestUI.gameObject.SetActive(!rzTestUI.gameObject.activeInHierarchy);
    }

    public static void ShowHideTestCanvas(bool show)
    {
        if (instance != null) instance.rzTestCanvas.gameObject.SetActive(show);
    }

    public static void ShowPingTestUI()
    {
        if (instance != null)
        {
            if (instance.rzTestUI != null) instance.rzTestUI.gameObject.SetActive(true);
#if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(instance.rzTestUI);
#endif
        }
    }

}
