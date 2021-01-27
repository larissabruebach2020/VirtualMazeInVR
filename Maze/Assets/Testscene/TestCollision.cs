using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with " + other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("End Collision with " + other.gameObject);
    }
}
