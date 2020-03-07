using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextRoom : MonoBehaviour
{

    //scene to be loaded next and scene to unload
    public int sceneToLoad;

    //check, if everything is loaded/unloaded already
    bool loadUnloadDone = false;

    private void OnTriggerEnter()
    {
        //check, if we have already done this
        if (!loadUnloadDone)
        {
            //make sure this only happens once
            loadUnloadDone = true;

            //load next scene and unload previous scene
            Scene[] activeScenes = new Scene[SceneManager.sceneCount];
            
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                activeScenes[i] = SceneManager.GetSceneAt(i);
            }

            foreach(Scene scene in activeScenes)
            {
                if (!gameObject.scene.Equals(scene) && !SceneManager.GetSceneByBuildIndex(0).Equals(scene))
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
            
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }

    }
    
}
