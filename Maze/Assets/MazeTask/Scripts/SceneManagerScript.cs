using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript sceneManagerObject;

    void Awake()
    {
        sceneManagerObject = this;

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        
    }

}
