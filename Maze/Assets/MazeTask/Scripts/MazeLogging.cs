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
    private string m_Directory = "Asstes";

    // file path
    public string m_Path;

    // general information
    public string m_TrialNumber = "";
    public string m_TrialStartTime = "";
    public int m_RoomNumber = 0;
    public string m_RoomEnterTime = "";

    // agent A
    public string m_AgentName_A = "";
    public string m_AgentPosition_A = "";
    public string m_AgentAnswer_A = "";
    public string m_AgentDistance_A = "";
    public string m_AgentTime_A = "";

    // agent B
    public string m_AgentName_B = "";
    public string m_AgentPosition_B = "";
    public string m_AgentAnswer_B = "";
    public string m_AgentDistance_B = "";
    public string m_AgentTime_B = "";

    // decision
    public string m_Decision = "";
    public string m_DecisionTime = "";


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
        string generalInfo = "TrialNumber,TrialStartTime,RoomNumber,RoomEnterTime";
        string agentA = "AgentName_A,AgentPosition_A,AgentAnswer_A,AgentDistance_A,AgentTime_A";
        string agentB = "AgentName_B,AgentPosition_B,AgentAnswer_B,AgentDistance_B,AgentTime_B";
        string decision = "Decision,DecisionTime";

        string headlines = generalInfo + "," + agentA + "," + agentB + "," + decision;

        File.AppendAllText(path, headlines);
    }

    public void SetFirstTrialStartTime()
    {
        m_TrialStartTime = System.DateTime.UtcNow.ToString(m_DateFormat);
    }

    public void WriteToLogFile()
    {
        string generalInfo = m_TrialNumber + "," + m_TrialStartTime + "," + m_RoomNumber + "," + m_RoomEnterTime;
        string agentA = m_AgentName_A + "," + m_AgentPosition_A + "," + m_AgentAnswer_A + "," + m_AgentDistance_A + "," + m_AgentTime_A;
        string agentB = m_AgentName_B + "," + m_AgentPosition_B + "," + m_AgentAnswer_B + "," + m_AgentDistance_B + "," + m_AgentTime_B;
        string decision = m_Decision + "," + m_DecisionTime;

        string trial = generalInfo + "," + agentA + "," + agentB + "," + decision;

        File.AppendAllText(m_Path, trial);
    }

    public void ResetValues()
    {
        // general information
        m_TrialNumber = "";
        m_TrialStartTime = m_DecisionTime;
        m_RoomNumber = 0;
        m_RoomEnterTime = "";

        // agent A
        m_AgentName_A = "";
        m_AgentPosition_A = "";
        m_AgentAnswer_A = "";
        m_AgentDistance_A = "";
        m_AgentTime_A = "";

        // agent B
        m_AgentName_B = "";
        m_AgentPosition_B = "";
        m_AgentAnswer_B = "";
        m_AgentDistance_B = "";
        m_AgentName_A = "";

        // decision
        m_Decision = "";
        m_DecisionTime = "";
    }


}
