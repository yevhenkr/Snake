using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RZ
{
    public static class SceneTools
    {
        ////////// ВЫГРУЗИТЬ ВСЕ СЦЕНЫ, КРОМЕ УКАЗАННОЙ: //////////
        public static void UnloadAllExcept(string sceneName)
        {
            int c = SceneManager.sceneCount;
            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != sceneName)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}
