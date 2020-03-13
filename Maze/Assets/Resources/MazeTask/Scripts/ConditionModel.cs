using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConditionModel
{

    public static int currentId = 1;

    public struct conditionMap
    {
        public string m_AudioAgent_A;
        public string m_AnimationAgent_A;
        public Vector3 m_PositionAgent_A;
        public Quaternion m_RotationAgent_A;
        public string m_AudioAgent_B;
        public string m_AnimationAgent_B;
        public Vector3 m_PositionAgent_B;
        public Quaternion m_RotationAgent_B;

    }

    public static Dictionary<int, conditionMap> conditionLib = new Dictionary<int, conditionMap>();

    public static void Init(int id, string audioAgent_A, string animationAgent_A, Vector3 positionAgent_A, Quaternion rotationAgent_A, string audioAgent_B, string animationAgent_B, Vector3 positionAgent_B, Quaternion rotationAgent_B)
    {
        var tmpMap = new conditionMap();
        tmpMap.m_AudioAgent_A = audioAgent_A;
        tmpMap.m_AnimationAgent_A = animationAgent_A;
        tmpMap.m_PositionAgent_A = positionAgent_A;
        tmpMap.m_RotationAgent_A = rotationAgent_A;
        tmpMap.m_AudioAgent_B = audioAgent_B;
        tmpMap.m_AnimationAgent_B = animationAgent_B;
        tmpMap.m_PositionAgent_B = positionAgent_B;
        tmpMap.m_RotationAgent_B = rotationAgent_B;

        conditionLib.Add(id, tmpMap);
    }


}

