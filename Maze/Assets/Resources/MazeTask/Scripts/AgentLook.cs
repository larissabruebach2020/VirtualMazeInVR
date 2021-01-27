using UnityEngine;
using uniwue.hci.vilearn;

public class AgentLook : MonoBehaviour
{
    public Transform eyeDest;
    public Transform fakeEye;
    public string Agent;

    public bool animated = false;

    private void Start()
    {
        eyeDest = GameState.Instance.GetPlayerCamera();
    }

    public void OnTriggerStay(Collider other)
    {
        if (!animated)
        {
            fakeEye.LookAt(eyeDest);
            Vector3 rot = fakeEye.localEulerAngles;
            if (Agent.Equals("A"))
            {
                if ((rot.x > 350 || rot.x < 10) && (rot.y > 330 || rot.y < 30))
                {
                    transform.LookAt(eyeDest);
                } 
            } else if (Agent.Equals("B"))
            {
                if ((rot.x > 350 || rot.x < 10) && (rot.y > 255 || rot.y < 305))
                {
                    transform.LookAt(eyeDest);
                }
            }
            
        } 

    }

    public void OnTriggerExit(Collider other)
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
