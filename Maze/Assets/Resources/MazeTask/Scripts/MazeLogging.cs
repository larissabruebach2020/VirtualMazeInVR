using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    public string m_TrialStartTime = ""; // set in SceneManager initially, then copied from m_DecisionTime
    public int m_RoomNumber = 0; // set in SceneManager
    public string m_RoomEnterTime = ""; // set in EnterRoom (onTriggerEnter)
    public string m_Condition = ""; // set in SceneManager

    // agent A
    public string m_AgentPosition_A = ""; // set in SceneManager
    public string m_AgentAnswer_A = ""; // set in SceneManager
    public string m_AgentAsked_A = ""; // set in AskAgent TODO
    public string m_AgentDistance_A = ""; // set in AskAgent TODO
    public string m_AgentTime_A = ""; // set in AskAgent TODO

    // agent B
    public string m_AgentPosition_B = ""; // set in SceneManager
    public string m_AgentAnswer_B = ""; // set in SceneManager
    public string m_AgentAsked_B = ""; // set in AskAgent
    public string m_AgentDistance_B = ""; // set in AskAgent
    public string m_AgentTime_B = ""; // set in AskAgent

    // decision
    public string m_Decision = ""; // set in MadeDecision
    public string m_DecisionTime = ""; // set in MadeDecision


    public void CreateNewLogfile(string subjectID)
    {
        m_Date = System.DateTime.UtcNow.ToString(m_DateFormat);
        m_Path = "Assets/" + m_Date + "_" + subjectID + ".csv";

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

        string headlines = generalInfo + "," + agentA + "," + agentB + "," + decision + "\n";

        File.AppendAllText(path, headlines);
    }

    public void SetFirstTrialStartTime()
    {
        m_TrialStartTime = System.DateTime.UtcNow.ToString(m_DateFormat);
    }

    public void WriteToLogFile()
    {
        string generalInfo = m_TrialNumber + "," + m_TrialStartTime + "," + m_RoomNumber + "," + m_RoomEnterTime + "," + m_Condition;
        string agentA = m_AgentPosition_A + "," + m_AgentAnswer_A + "," + m_AgentAsked_A + "," + m_AgentDistance_A + "," + m_AgentTime_A;
        string agentB = m_AgentPosition_B + "," + m_AgentAnswer_B + "," + m_AgentAsked_B + "," + m_AgentDistance_B + "," + m_AgentTime_B;
        string decision = m_Decision + "," + m_DecisionTime;

        string trial = generalInfo + "," + agentA + "," + agentB + "," + decision + "\n";

        File.AppendAllText(m_Path, trial);
    }

    public void ResetValues()
    {
        // general information
        m_TrialNumber = "";
        m_TrialStartTime = m_DecisionTime;
        m_RoomNumber = 0;
        m_RoomEnterTime = "";
        m_Condition = "";

        // agent A
        m_AgentPosition_A = "";
        m_AgentAnswer_A = "";
        m_AgentAsked_A = "";
        m_AgentDistance_A = "";
        m_AgentTime_A = "";

        // agent B
        m_AgentPosition_B = "";
        m_AgentAnswer_B = "";
        m_AgentAsked_B = "";
        m_AgentDistance_B = "";
        m_AgentTime_B = "";

        // decision
        m_Decision = "";
        m_DecisionTime = "";
    }


}
