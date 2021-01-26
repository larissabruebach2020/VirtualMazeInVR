using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotaion : MonoBehaviour
{
    float speed = 0.2f;
    public bool shouldRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldRotate == true)
        {
            transform.Rotate(speed, speed, 0); //makes the Object rotate on the y and x axis with the value of speed
        }
       
    }
}
