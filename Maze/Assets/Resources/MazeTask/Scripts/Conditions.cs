﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditions : MonoBehaviour
{

    string m_AudioPath = "MazeTask/Audio/";
    Vector3 m_RightPosition = new Vector3(-1.8f, 0.01f, -0.8f);
    Vector3 m_LeftPosition = new Vector3(1.8f, 0.01f, -0.8f);
    Quaternion m_RightRotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);
    Quaternion m_LeftRotation = Quaternion.Euler(0.0f, -30.0f, 0.0f);

    // Use this for initialization
    void Awake()
    {
        if (!ConditionModel.conditionLib.ContainsKey(1))
        {
            // both right
            ConditionModel.Init(1, (m_AudioPath + "AgentA_Right1"), "AgentA_Right1", "AgentA2_Right1", m_RightPosition, m_RightRotation, (m_AudioPath + "AgentB_Right1"), "AgentB_Right1", "AgentB2_Right1", m_LeftPosition, m_LeftRotation);
            ConditionModel.Init(2, (m_AudioPath + "AgentA_Right1"), "AgentA_Right1", "AgentA2_Right1", m_LeftPosition, m_LeftRotation, (m_AudioPath + "AgentB_Right1"), "AgentB_Right1", "AgentB2_Right1", m_RightPosition, m_RightRotation);

            // both left
            ConditionModel.Init(3, (m_AudioPath + "AgentA_Left1"), "AgentA_Left1", "AgentA2_Left1", m_RightPosition, m_RightRotation, (m_AudioPath + "AgentB_Left1"), "AgentB_Left1", "AgentB2_Left1", m_LeftPosition, m_LeftRotation);
            ConditionModel.Init(4, (m_AudioPath + "AgentA_Left1"), "AgentA_Left1", "AgentA2_Left1", m_LeftPosition, m_LeftRotation, (m_AudioPath + "AgentB_Left1"), "AgentB_Left1", "AgentB2_Left1", m_RightPosition, m_RightRotation);

            // A right, B left
            ConditionModel.Init(5, (m_AudioPath + "AgentA_Right2"), "AgentA_Right2", "AgentA2_Right2", m_RightPosition, m_RightRotation, (m_AudioPath + "AgentB_Left2"), "AgentB_Left2", "AgentB2_Left2", m_LeftPosition, m_LeftRotation);
            ConditionModel.Init(6, (m_AudioPath + "AgentA_Right2"), "AgentA_Right2", "AgentA2_Right2", m_LeftPosition, m_LeftRotation, (m_AudioPath + "AgentB_Left2"), "AgentB_Left2", "AgentB2_Left2", m_RightPosition, m_RightRotation);

            // A left, B right
            ConditionModel.Init(7, (m_AudioPath + "AgentA_Left2"), "AgentA_Left2", "AgentA2_Left2", m_RightPosition, m_RightRotation, (m_AudioPath + "AgentB_Right2"), "AgentB_Right2", "AgentB2_Right2", m_LeftPosition, m_LeftRotation);
            ConditionModel.Init(8, (m_AudioPath + "AgentA_Left2"), "AgentA_Left2", "AgentA2_Left2", m_LeftPosition, m_LeftRotation, (m_AudioPath + "AgentB_Right2"), "AgentB_Right2", "AgentB2_Right2", m_RightPosition, m_RightRotation);
            
        }
    }
}
