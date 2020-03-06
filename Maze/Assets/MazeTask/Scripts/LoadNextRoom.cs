using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextRoom : MonoBehaviour
{

    //scene to be loaded next and scene to unload
    public int sceneToLoad;
    public int sceneToUnload;

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
            SceneManager.UnloadSceneAsync(sceneToUnload);
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }

    }
    
}
