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
            Vector3 rot = fakeEye.localRotation.eulerAngles;
            if ((rot.z > 358 || rot.z < 2) && (rot.y > 250 && rot.y < 270))
            {
                transform.LookAt(eyeDest);
                transform.Rotate(0, 90, 0);
            } 
        } 
        

    }

    public void OnTriggerExit(Collider other)
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
