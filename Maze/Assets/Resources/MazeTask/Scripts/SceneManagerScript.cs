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
    private Vector3 m_Right = new Vector3(-1.5f, 0.1f, -1);
    private Vector3 m_Left = new Vector3(1.5f, 0.1f, -1);

    // current condition
    public int m_CurrentCondition;

    // set number of max Trials
    public int m_MaxNumberOfTrials;

    // condition array
    private List<int> m_ConditionList;

    // agents
    private GameObject Agent_A;
    private GameObject Agent_B;


    void Start()
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

        // generate condition list
        GenerateCondition();

        // start first trial
        StartTrial(1);

        // setup first room, as load next room is not called
        StartCoroutine(StartFirstRoom());
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
            if (m_ConditionList.Count < 1 && m_TrialNumber < m_MaxNumberOfTrials)
            {
                GenerateCondition();
            }

            m_CurrentCondition = m_ConditionList[Random.Range(1, m_ConditionList.Count)];
            m_ConditionList.Remove(m_CurrentCondition);

            mazeLogging.m_Condition = m_CurrentCondition.ToString();

            // set known values in logging file
            mazeLogging.m_TrialNumber = m_TrialNumber.ToString();
            mazeLogging.m_RoomNumber = roomNumber;

            mazeLogging.m_AgentAnswer_A = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_A;
            mazeLogging.m_AgentAnswer_B = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_B;

            if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A.Equals(m_Right))
            {
                mazeLogging.m_AgentPosition_A = "right";
            }
            else if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A.Equals(m_Left))
            {
                mazeLogging.m_AgentPosition_A = "left";
            }

            if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_B.Equals(m_Right))
            {
                mazeLogging.m_AgentPosition_B = "right";
            }
            else if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_B.Equals(m_Left))
            {
                mazeLogging.m_AgentPosition_B = "left";
            }

        }

    }

    public void GenerateCondition()
    {
        m_ConditionList = new List<int>();
        m_ConditionList.Add(1);
        m_ConditionList.Add(2);
        m_ConditionList.Add(3);
        m_ConditionList.Add(4);
        m_ConditionList.Add(5);
        m_ConditionList.Add(6);
        m_ConditionList.Add(7);
        m_ConditionList.Add(8);
    }

    public IEnumerator StartFirstRoom()
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByName("Room1").isLoaded);

        // get agents of next room
        Agent_A = GameObject.FindGameObjectWithTag("Room1").transform.GetChild(0).gameObject;
        Agent_B = GameObject.FindGameObjectWithTag("Room1").transform.GetChild(1).gameObject;

        // set agents to the right positions and assign audio files

        Agent_A.transform.position += ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A;
        Agent_A.transform.rotation *= ConditionModel.conditionLib[m_CurrentCondition].m_RotationAgent_A;
        Agent_A.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_A);

        Agent_B.transform.position += ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_B;
        Agent_B.transform.rotation *= ConditionModel.conditionLib[m_CurrentCondition].m_RotationAgent_B;
        Agent_B.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_B);

    }


}
