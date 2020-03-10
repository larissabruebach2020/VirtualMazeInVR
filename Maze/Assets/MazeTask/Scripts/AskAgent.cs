using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AskAgent : MonoBehaviour {

    public SteamVR_Action_Boolean m_TalkPress = null;

    private Transform m_CameraRig = null;
    private Animator m_Animator;
    private AudioSource m_AudioSource;

    //public Material newMat;


    public void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    public IEnumerator OnTriggerStay(Collider other)
    {
        if (m_TalkPress.GetStateDown(SteamVR_Input_Sources.Any) && !m_AudioSource.isPlaying)
        {
            // get postition of camera and agent on ground (no y position)
            Vector2 cameraPos = new Vector2(m_CameraRig.transform.position.x, m_CameraRig.transform.position.z);
            Vector2 agentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

            // get distance between camera and agent
            float distanceToObject = Vector2.Distance(cameraPos, agentPos);
            Debug.Log("Distance " + distanceToObject);

            // play audio file from agent here and start animation
            m_Animator.SetTrigger("interactionStarted");
            m_AudioSource.Play();
            
            // wait until audio file is done playing to set agent animation back to idle
            yield return new WaitUntil(() => !m_AudioSource.isPlaying);
            m_Animator.SetTrigger("idle");

            // make sure previous animation is finished
            yield return new WaitUntil(() => m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SSquirrel_Eat_Anim"));
        }

        

    }
    
}
