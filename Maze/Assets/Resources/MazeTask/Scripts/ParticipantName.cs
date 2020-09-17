using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class ParticipantName : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField participantName;
    public TMP_InputField trialNumber;

    public TextMeshProUGUI error;

    private MazeLogging mazeLogging;
    private SceneManagerScript sceneManager;
    private VRController walking;

    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(GetNameAndStart);

        mazeLogging = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MazeLogging>();
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        walking = GameObject.FindGameObjectWithTag("VRController").GetComponent<VRController>();
    }

    void GetNameAndStart()
    {
        if(string.IsNullOrEmpty(participantName.text))
        {
            error.text = "No name entered!";
        } else if(string.IsNullOrEmpty(trialNumber.text))
        {
            error.text = "No trial number entered";
        } else
        {
            error.text = "";
            // get input from user and start logging
            mazeLogging.CreateNewLogfile(participantName.text);

            // set number of trials
            sceneManager.m_MaxNumberOfTrials = Int16.Parse(trialNumber.text);

            // enable walking

            // activate walking
            walking.enabled = true;

            this.gameObject.SetActive(false);
        }
        
    }
}
