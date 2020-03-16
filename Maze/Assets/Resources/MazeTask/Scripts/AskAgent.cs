using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AskAgent : MonoBehaviour
{

    public SteamVR_Action_Boolean m_TalkPress = null;

    private Transform m_CameraRig = null;
    private Animator m_Animator;
    private AudioSource m_AudioSource;

    // logging variables
    private bool m_LoggingNeeded = true;
    private MazeLogging m_MazeLogging;

    private float m_DistanceToAgent;

    // current condition variables
    private SceneManagerScript m_SceneManager;
    private int m_Condition;


    public void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();

        m_MazeLogging = GameObject.FindGameObjectWithTag(("SceneManager")).GetComponent<MazeLogging>();

        m_SceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagerScript>();
        m_Condition = m_SceneManager.m_CurrentCondition;
    }

    public IEnumerator OnTriggerStay(Collider other)
    {
        if (m_TalkPress.GetStateDown(SteamVR_Input_Sources.Any) && !m_AudioSource.isPlaying)
        {
            // get postition of camera and agent on ground (no y position)
            Vector2 cameraPos = new Vector2(m_CameraRig.transform.position.x, m_CameraRig.transform.position.z);
            Vector2 agentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

            // get distance between camera and agent
            m_DistanceToAgent = Vector2.Distance(cameraPos, agentPos);

            // play audio file from agent here and start animation
            if (name.Equals("Agent_A"))
            {
                m_Animator.SetTrigger(ConditionModel.conditionLib[m_Condition].m_AnimationAgent_A);
            }
            else if (name.Equals("Agent_B"))
            {
                m_Animator.SetTrigger(ConditionModel.conditionLib[m_Condition].m_AnimationAgent_B);

            }

            m_AudioSource.Play();

            // wait until audio file is done playing to set agent animation back to idle
            yield return new WaitUntil(() => !m_AudioSource.isPlaying);
            m_Animator.SetTrigger("idle");

            // make sure previous animation is finished
            yield return new WaitUntil(() => m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

            // log all needed variables for the agent interaction
            if (m_LoggingNeeded)
            {
                if (name.Equals("Agent_A"))
                {
                    m_MazeLogging.m_AgentAsked_A = "true";
                    m_MazeLogging.m_AgentDistance_A = m_DistanceToAgent.ToString();
                    m_MazeLogging.m_AgentTime_A = System.DateTime.UtcNow;
                }
                else if (name.Equals("Agent_B"))
                {
                    m_MazeLogging.m_AgentAsked_B = "true";
                    m_MazeLogging.m_AgentDistance_B = m_DistanceToAgent.ToString();
                    m_MazeLogging.m_AgentTime_B = System.DateTime.UtcNow;
                }

                m_LoggingNeeded = false;
            }
        }

        

    }

}
