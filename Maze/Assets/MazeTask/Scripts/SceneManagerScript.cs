using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // managing objects
    private SceneManagerScript sceneManagerObject;
    private MazeLogging mazeLogging;
    private Conditions m_AllConditions;

    // subject ID
    public string subjectID;

    // number of trials
    public int m_TrialNumber = 0;

    // possible agent position
    private Vector3 m_Right = new Vector3(-2, 0.1f, -1);

    // current condition
    public int m_CurrentCondition;

    // set number of max Trials
    public int m_MaxNumberOfTrials;


    void Awake()
    {
        sceneManagerObject = this;

        //load first two scenes
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        // start logging and set first trial start time
        mazeLogging = GetComponent<MazeLogging>();
        mazeLogging.CreateNewLogfile(subjectID);

        // load dictionary with all conditions (8)
        m_AllConditions = GetComponent<Conditions>();

        // start first trial
        StartTrial(1);
    }

    public void StartTrial(int roomNumber)
    {
        // check if we reached the maximum number of trials
        if (m_TrialNumber < m_MaxNumberOfTrials)
        {
            // increase trial number
            m_TrialNumber++;

            // setting trial start time for the first trial
            if (m_TrialNumber == 1)
            {
                mazeLogging.SetFirstTrialStartTime();
            }

            // find condition for this trial; load conditions again, if necessary
            if (ConditionModel.conditionLib.Count < 1)
            {
                m_AllConditions.LoadAllConditions();
            }

            m_CurrentCondition = Random.Range(0, ConditionModel.conditionLib.Count);

            // set known values in logging file
            mazeLogging.m_TrialNumber = m_TrialNumber.ToString();
            mazeLogging.m_RoomNumber = roomNumber;

            mazeLogging.m_AgentAnswer_A = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_A;
            mazeLogging.m_AgentAnswer_B = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_B;

            if (ConditionModel.conditionLib[1].m_PositionAgent_A.Equals(m_Right))
            {
                mazeLogging.m_AgentPosition_A = "right";
            }
            else
            {
                mazeLogging.m_AgentPosition_A = "left";
            }

            if (ConditionModel.conditionLib[1].m_PositionAgent_B.Equals(m_Right))
            {
                mazeLogging.m_AgentPosition_B = "right";
            }
            else
            {
                mazeLogging.m_AgentPosition_B = "left";
            }
        }

    }


}
