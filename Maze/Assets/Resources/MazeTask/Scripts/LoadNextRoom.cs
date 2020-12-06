using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextRoom : MonoBehaviour
{

    //scene to be loaded next and scene to unload
    private MazeLogging m_MazeLogger;
    private int m_SceneToLoad;

    //check, if everything is loaded/unloaded already
    bool loadUnloadDone = false;

    // current condition variables
    private SceneManagerScript m_SceneManagerScript;
    private int m_Condition;

    // agents
    private GameObject Agent_A;
    private GameObject Agent_B;
    private GameObject Agent_A_Instance;
    private GameObject Agent_B_Instance;
    private GameObject agents;

    // scene manager
    private GameObject m_SceneManager;

    // rotation angle
    private Quaternion m_BaseRotation = new Quaternion(0f, 1.0f, 0f, 0f);

    private void Awake()
    {
        m_SceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        m_MazeLogger = m_SceneManager.GetComponent<MazeLogging>();
        m_SceneToLoad = m_MazeLogger.m_RoomNumber;

        m_SceneManagerScript = m_SceneManager.GetComponent<SceneManagerScript>();
        m_Condition = m_SceneManagerScript.m_CurrentCondition;

        Agent_A = m_SceneManagerScript.Agent_A;
        Agent_B = m_SceneManagerScript.Agent_B;
    }

    private IEnumerator OnTriggerEnter()
    {
        //check, if we have already done this
        if (!loadUnloadDone)
        {
            //make sure this only happens once
            loadUnloadDone = true;

            // get next room number
            m_SceneToLoad = m_MazeLogger.m_RoomNumber;

            //load next scene and unload previous scene
            Scene[] activeScenes = new Scene[SceneManager.sceneCount];

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                activeScenes[i] = SceneManager.GetSceneAt(i);

            }


            foreach (Scene scene in activeScenes)
            {
                if (!gameObject.scene.Equals(scene) && !SceneManager.GetSceneByBuildIndex(0).Equals(scene))
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }

            SceneManager.LoadSceneAsync(m_SceneToLoad, LoadSceneMode.Additive);

            // wait until new room is fully loaded
            yield return new WaitUntil(() => SceneManager.GetSceneByName("Room" + m_SceneToLoad).isLoaded);

            // instantiate agents of next room
            Agent_A_Instance = Instantiate(Agent_A);
            Agent_B_Instance = Instantiate(Agent_B);
            agents = GameObject.FindGameObjectWithTag("Room" + m_SceneToLoad);
            Agent_A_Instance.transform.SetParent(agents.transform, false);
            Agent_B_Instance.transform.SetParent(agents.transform, false);

            // set agents to the right positions, rotations and assign audio files

            m_Condition = m_SceneManagerScript.m_CurrentCondition;

            Agent_A_Instance.transform.rotation *= ConditionModel.conditionLib[m_Condition].m_RotationAgent_A;
            Agent_A_Instance.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_Condition].m_AudioAgent_A);

            Agent_B_Instance.transform.rotation *= ConditionModel.conditionLib[m_Condition].m_RotationAgent_B;
            Agent_B_Instance.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_Condition].m_AudioAgent_B);

            // check if agents are rotated, adapt the position
            if (GameObject.FindGameObjectWithTag("Room" + m_SceneToLoad).transform.rotation.Equals(m_BaseRotation))
            {
                Agent_A_Instance.transform.position += ConditionModel.conditionLib[m_Condition].m_PositionAgent_B;
                Agent_B_Instance.transform.position += ConditionModel.conditionLib[m_Condition].m_PositionAgent_A;
            }
            else
            {
                Agent_A_Instance.transform.position += ConditionModel.conditionLib[m_Condition].m_PositionAgent_A;
                Agent_B_Instance.transform.position += ConditionModel.conditionLib[m_Condition].m_PositionAgent_B;
            }


        }

    }

    // for first scene
    public void AddAgentsForFirstRoom()
    {

    }
}
