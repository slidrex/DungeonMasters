using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Riptide;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static IEnumerator LoadSceneAsync(int sceneIndex, Action onLoaded = null)
    {
        Debug.Log("Loading scene " + sceneIndex);
        if (sceneIndex < 0) yield break;
        
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(sceneIndex);
        loadScene.allowSceneActivation = false;

        while (!loadScene.isDone)
        {
            if (loadScene.progress >= 0.9f)
            {
                loadScene.allowSceneActivation = true;
            }

            yield return null;
        }
        
        onLoaded?.Invoke();
    }
}