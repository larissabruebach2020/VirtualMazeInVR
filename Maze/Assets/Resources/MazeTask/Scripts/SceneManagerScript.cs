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

    // number of trials
    public int m_TrialNumber = 0;

    // possible agent position
    private string m_Right = "(-1.8, 0.0, -0.8)";
    private string m_Left = "(1.8, 0.0, -0.8)";

    // current condition
    public int m_CurrentCondition;

    // set number of max Trials
    public int m_MaxNumberOfTrials;

    // condition array
    private List<int> m_ConditionList;

    // agents
    public GameObject Agent_A;
    public GameObject Agent_B;
    private GameObject agents_Room1;

    // end ui
    public GameObject m_UIEnd;


    void Start()
    {
        sceneManagerObject = this;

        //load first two scenes
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        // start logging and set first trial start time
        mazeLogging = GetComponent<MazeLogging>();

        // load dictionary with all conditions (8)
        m_AllConditions = GetComponent<Conditions>();

        // generate condition list
        GenerateCondition();

        // start first trial
        StartTrial(1);

        // setup first room, as load next room is not called
        StartCoroutine(StartFirstRoom());

        // find ui end and deactivate it
        m_UIEnd = GameObject.FindGameObjectWithTag("UI");
        m_UIEnd.SetActive(false);
    }

    public void StartTrial(int roomNumber)
    {
        // check if we reached the maximum number of trials
        if (m_TrialNumber <= m_MaxNumberOfTrials)
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

            m_CurrentCondition = m_ConditionList[Random.Range(0, m_ConditionList.Count)];
            m_ConditionList.Remove(m_CurrentCondition);

            mazeLogging.m_Condition = m_CurrentCondition.ToString();

            // set known values in logging file
            mazeLogging.m_TrialNumber = m_TrialNumber.ToString();
            mazeLogging.m_RoomNumber = roomNumber;

            mazeLogging.m_AgentAnswer_A = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_A;
            mazeLogging.m_AgentAnswer_B = ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_B;

            if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A.ToString().Equals(m_Right))
            {
                mazeLogging.m_AgentPosition_A = "right";
                mazeLogging.m_AgentPosition_B = "left";
            }
            else if (ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A.ToString().Equals(m_Left))
            {
                mazeLogging.m_AgentPosition_A = "left";
                mazeLogging.m_AgentPosition_B = "right";
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

        // Instantiate Agents
        GameObject Agent_A_Instance = Instantiate(Agent_A);
        GameObject Agent_B_Instance = Instantiate(Agent_B);
        agents_Room1 = GameObject.FindGameObjectWithTag("Room1");
        Agent_A_Instance.transform.SetParent(agents_Room1.transform, false);
        Agent_B_Instance.transform.SetParent(agents_Room1.transform, false);

        // set agents to the right positions and assign audio files

        Agent_A_Instance.transform.position += ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_A;
        Agent_A_Instance.transform.rotation *= ConditionModel.conditionLib[m_CurrentCondition].m_RotationAgent_A;
        Agent_A_Instance.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_A);

        Agent_B_Instance.transform.position += ConditionModel.conditionLib[m_CurrentCondition].m_PositionAgent_B;
        Agent_B_Instance.transform.rotation *= ConditionModel.conditionLib[m_CurrentCondition].m_RotationAgent_B;
        Agent_B_Instance.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>(ConditionModel.conditionLib[m_CurrentCondition].m_AudioAgent_B);

    }


}
