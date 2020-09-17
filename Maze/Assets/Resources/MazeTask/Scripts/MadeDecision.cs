using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadeDecision : MonoBehaviour
{

    private MazeLogging logger;
    private SceneManagerScript sceneManager;
    private VRController walking;

    public int m_NextRoom;

    public GameObject m_UIEnd;

    private void Awake()
    {
        logger = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MazeLogging>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        walking = GameObject.FindGameObjectWithTag("VRController").GetComponent<VRController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // set values in maze logger
        logger.m_DecisionTime = System.DateTime.UtcNow;
        logger.m_Decision = gameObject.name;

        // save values for this trial
        logger.WriteToLogFile();

        // resetting the values of the old trial and set start time to decision time
        logger.ResetValues();

        // check if we reached max number of trials
        if (sceneManager.m_MaxNumberOfTrials == sceneManager.m_TrialNumber)
        {
            Debug.Log("end");
            // display end message for participant
            m_UIEnd = sceneManager.m_UIEnd;
            m_UIEnd.SetActive(true);
            walking.enabled = false;

        } else
        {
            Debug.Log("not end" + sceneManager.m_MaxNumberOfTrials + sceneManager.m_TrialNumber);
            // start new trial
            sceneManager.StartTrial(m_NextRoom);
        }
        
    }
}
