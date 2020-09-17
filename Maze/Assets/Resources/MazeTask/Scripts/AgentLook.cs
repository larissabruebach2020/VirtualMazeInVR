using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentLook : MonoBehaviour
{
    public Transform eyeDest;
    public Transform fakeEye;

    public bool animated = false;

    private void Start()
    {
        eyeDest = GameObject.FindGameObjectWithTag("MainCamera").transform;
        
    }

    void Update()
    {
        

    }

    public void OnTriggerStay(Collider other)
    {
        if (!animated)
        {
            fakeEye.LookAt(eyeDest);
            Vector3 rot = fakeEye.localEulerAngles;
            if ((rot.z > 356 || rot.z < 4) && (rot.y > 270 || rot.y < 280))
            {
                transform.LookAt(eyeDest);
                transform.Rotate(0, 90, 0);
            }
        } 
        

    }
}
