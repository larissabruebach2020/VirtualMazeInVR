using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class MazeLogging : MonoBehaviour
{
    // file name
    public string m_SubjectID;
    public string m_Date;
    private string m_DateFormat = "yyyy-MM-dd_HH-mm-ss";
    private string m_Directory = "Assets";

    // file path
    public string m_Path;

    // general information
    public string m_TrialNumber = ""; // set in SceneManager
    public DateTime m_TrialStartTime; // set in SceneManager initially, then copied from m_DecisionTime
    public int m_RoomNumber = 0; // set in SceneManager
    public DateTime m_RoomEnterTime; // set in EnterRoom (onTriggerEnter)
    public string m_Condition = ""; // set in SceneManager

    // agent A
    public string m_AgentPosition_A = ""; // set in SceneManager
    public string m_AgentAnswer_A = ""; // set in SceneManager
    public string m_AgentAsked_A = ""; // set in AskAgent
    public string m_AgentDistance_A = ""; // set in AskAgent 
    public DateTime m_AgentTime_A; // set in AskAgent

    // agent B
    public string m_AgentPosition_B = ""; // set in SceneManager
    public string m_AgentAnswer_B = ""; // set in SceneManager
    public string m_AgentAsked_B = ""; // set in AskAgent
    public string m_AgentDistance_B = ""; // set in AskAgent
    public DateTime m_AgentTime_B; // set in AskAgent

    // decision
    public string m_Decision = ""; // set in MadeDecision
    public DateTime m_DecisionTime; // set in MadeDecision

    // time deltas --> calculated in MazeLogging
    private string m_RoomEnter_Decicion = "";
    private string m_TrialStart_Decision = "";
    private string m_AgentAsked_A_Decision = "";
    private string m_AgentAsked_B_Decision = "";
    private string m_AgentAsked_A_AgentAsked_B = "";


    public void CreateNewLogfile(string subjectID)
    {
        m_Date = System.DateTime.UtcNow.ToString(m_DateFormat);
        m_Path = "Assets/LogFiles/" + m_Date + "_" + subjectID + ".csv";

        //check if a file already exists, if not create a new one
        if (!Directory.Exists(m_Directory))
        {
            Directory.CreateDirectory(m_Directory);
        }

        AddHeadlines(m_Path);
    }

    public void AddHeadlines(string path)
    {
        // add headlines to the csv file
        string generalInfo = "TrialNumber,TrialStartTime,RoomNumber,RoomEnterTime,Condition";
        string agentA = "AgentPosition_A,AgentAnswer_A,AgentAsked_A,AgentDistance_A,AgentTime_A";
        string agentB = "AgentPosition_B,AgentAnswer_B,AgentAsked_B,AgentDistance_B,AgentTime_B";
        string decision = "Decision,DecisionTime";
        string timedeltas = "RoomEnter_Decision,TrialStar_Decision,AgentAsked_A_Decision,m_AgentAsked_B_Decision,m_AgentAsked_A_AgentAsked_B";

        string headlines = generalInfo + "," + agentA + "," + agentB + "," + decision + "," + timedeltas + "\n";

        File.AppendAllText(path, headlines);
    }

    public void SetFirstTrialStartTime()
    {
        m_TrialStartTime = System.DateTime.UtcNow;
    }

    public void WriteToLogFile()
    {
        CalculateTimeDeltas();
        
        string generalInfo = m_TrialNumber + "," + m_TrialStartTime.ToString(m_DateFormat) + "," + m_RoomNumber + "," + m_RoomEnterTime + "," + m_Condition;
        string agentA = m_AgentPosition_A + "," + m_AgentAnswer_A + "," + m_AgentAsked_A + "," + m_AgentDistance_A + "," + m_AgentTime_A.ToString(m_DateFormat);
        string agentB = m_AgentPosition_B + "," + m_AgentAnswer_B + "," + m_AgentAsked_B + "," + m_AgentDistance_B + "," + m_AgentTime_B.ToString(m_DateFormat);
        string decision = m_Decision + "," + m_DecisionTime.ToString(m_DateFormat);
        string timedeltas = m_RoomEnter_Decicion + "," + m_TrialStart_Decision + "," + m_AgentAsked_A_Decision + "," + m_AgentAsked_B_Decision + "," + m_AgentAsked_A_AgentAsked_B;

        string trial = generalInfo + "," + agentA + "," + agentB + "," + decision + "," + timedeltas + "\n";

        File.AppendAllText(m_Path, trial);
    }

    public void ResetValues()
    {
        // general information
        m_TrialNumber = "";
        m_TrialStartTime = m_DecisionTime;
        m_RoomNumber = 0;
        m_RoomEnterTime = new DateTime();
        m_Condition = "";

        // agent A
        m_AgentPosition_A = "";
        m_AgentAnswer_A = "";
        m_AgentAsked_A = "";
        m_AgentDistance_A = "";
        m_AgentTime_A = new DateTime();

        // agent B
        m_AgentPosition_B = "";
        m_AgentAnswer_B = "";
        m_AgentAsked_B = "";
        m_AgentDistance_B = "";
        m_AgentTime_B = new DateTime();

        // decision
        m_Decision = "";
        m_DecisionTime = new DateTime();

        // time deltas
        m_RoomEnter_Decicion = "";
        m_TrialStart_Decision = "";
        m_AgentAsked_A_Decision = "";
        m_AgentAsked_B_Decision = "";
        m_AgentAsked_A_AgentAsked_B = "";

}

    public void CalculateTimeDeltas()
    {
        m_RoomEnter_Decicion = (m_DecisionTime - m_RoomEnterTime).ToString();
        m_TrialStart_Decision = (m_DecisionTime - m_TrialStartTime).ToString();
      
        if (!string.IsNullOrEmpty(m_AgentAsked_A))
        {
            m_AgentAsked_A_Decision = (m_DecisionTime - m_AgentTime_A).ToString();
        }

        if (!string.IsNullOrEmpty(m_AgentAsked_B))
        {
            m_AgentAsked_B_Decision = (m_DecisionTime - m_AgentTime_B).ToString();
        }

        if (!string.IsNullOrEmpty(m_AgentAsked_A) && !string.IsNullOrEmpty(m_AgentAsked_B)) {
            if (m_AgentTime_A < m_AgentTime_B)
                m_AgentAsked_A_AgentAsked_B = (m_AgentTime_B - m_AgentTime_A).ToString();
            else
                m_AgentAsked_A_AgentAsked_B = (m_AgentTime_A - m_AgentTime_B).ToString();
        }

    }



}
