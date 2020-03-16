using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadeDecision : MonoBehaviour
{

    private MazeLogging logger;

    private SceneManagerScript sceneManager;

    public int m_NextRoom;

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

        // start new trial
        sceneManager.StartTrial(m_NextRoom);
    }
}
