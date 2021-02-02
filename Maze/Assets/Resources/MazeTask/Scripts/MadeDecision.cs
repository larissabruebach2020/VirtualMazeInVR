using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uniwue.hci.vilearn;

public class MadeDecision : MonoBehaviour
{
    private MazeLogging logger;
    private SceneManagerScript sceneManager;

    public int m_NextRoom;

    public GameObject m_UIEnd;

    private void Awake()
    {
        logger = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MazeLogging>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
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
            // display end message for participant
            m_UIEnd = sceneManager.m_UIEnd;
            m_UIEnd.SetActive(true);

            GameState.Instance.isWalkingEnabled = false;
        } else
        {
            // start new trial
            sceneManager.StartTrial(m_NextRoom);
        }
        
    }
}
