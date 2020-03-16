using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{

    private MazeLogging logger;

    private void Awake()
    {
        logger = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MazeLogging>();
    }

    private void OnTriggerEnter(Collider other)
    {
        logger.m_RoomEnterTime = System.DateTime.UtcNow;
    }
}
