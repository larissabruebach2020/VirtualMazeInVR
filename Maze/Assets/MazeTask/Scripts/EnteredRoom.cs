using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteredRoom : MonoBehaviour
{

    private MazeLogging logger;
    private string m_DateFormat = "yyyy-MM-dd_HH-mm-ss";

    private void Awake()
    {
        logger = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<MazeLogging>();
    }

    private void OnTriggerEnter(Collider other)
    {
        logger.m_RoomEnterTime = System.DateTime.UtcNow.ToString(m_DateFormat);
    }
}
