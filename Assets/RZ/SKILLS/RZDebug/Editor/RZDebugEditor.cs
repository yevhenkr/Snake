namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;

    [InitializeOnLoad]
    public class RZDebugEditor
    {

        static RZDebugEditor()
        {
            EditorApplication.wantsToQuit += WantsToQuit;
            // EditorApplication.quitting += Quit;
        }


        static bool WantsToQuit()
        {
            RZDebug.RestoreAllStackTrace();
            return true;
        }


        // static void Quit()
        // {
        //     RZDebug.RestoreStackTrace();
        // }

    }

#endif

}