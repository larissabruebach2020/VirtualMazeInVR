using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // managing objects
    public SceneManagerScript sceneManagerObject;
    public MazeLogging mazeLogging;

    // subject ID
    public string subjectID;

    // number of trials
    public int m_TrialNumber = 0;

    void Awake()
    {
        sceneManagerObject = this;

        //load first two scenes
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        // start logging and set first trial start time
        mazeLogging = GetComponent<MazeLogging>();
        mazeLogging.CreateNewLogfile(subjectID);

        StartTrial(1);
    }

    public void StartTrial(int roomNumber)
    {
        // increase trial number
        m_TrialNumber++;

        // setting trial start time for the first trial
        if (m_TrialNumber == 1)
        {
            mazeLogging.SetFirstTrialStartTime();
        }

        // set known values in logging file
        mazeLogging.m_TrialNumber = m_TrialNumber.ToString();
        mazeLogging.m_RoomNumber = roomNumber;

    }

}
