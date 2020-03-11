using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{

    string m_AudioPath = "Audio/";
    Vector3 m_Right = new Vector3(-2, 0.1f, -1);
    Vector3 m_Left = new Vector3(2, 0.1f, -1);

    // Use this for initialization
    void Start()
    {

    }

    public void LoadAllConditions()
    {
        if (!ConditionModel.conditionLib.ContainsKey(1))
        {
            // both right
            ConditionModel.Init(1, (m_AudioPath + "AgentA_Right1"), "AgentA_Right", m_Right, (m_AudioPath + "AgentB_Right1"), "AgentB_Right", m_Left);
            ConditionModel.Init(2, (m_AudioPath + "AgentA_Right1"), "AgentA_Right", m_Left, (m_AudioPath + "AgentB_Right1"), "AgentB_Right", m_Right);

            // both left
            ConditionModel.Init(3, (m_AudioPath + "AgentA_Left1"), "AgentA_Left", m_Right, (m_AudioPath + "AgentB_Left1"), "AgentB_Left", m_Left);
            ConditionModel.Init(4, (m_AudioPath + "AgentA_Left1"), "AgentA_Left", m_Left, (m_AudioPath + "AgentB_Left1"), "AgentB_Left", m_Right);

            // A right, B left
            ConditionModel.Init(5, (m_AudioPath + "AgentA_Right2"), "AgentA_Right", m_Right, (m_AudioPath + "AgentB_Left2"), "AgentB_Left", m_Left);
            ConditionModel.Init(6, (m_AudioPath + "AgentA_Right2"), "AgentA_Right", m_Left, (m_AudioPath + "AgentB_Left2"), "AgentB_Left", m_Right);

            // A left, B right
            ConditionModel.Init(7, (m_AudioPath + "AgentA_Left2"), "AgentA_Left", m_Right, (m_AudioPath + "AgentB_Right2"), "AgentB_Right", m_Left);
            ConditionModel.Init(8, (m_AudioPath + "AgentA_Left2"), "AgentA_Left", m_Left, (m_AudioPath + "AgentB_Right2"), "AgentB_Right", m_Right);
        }
    }
}
