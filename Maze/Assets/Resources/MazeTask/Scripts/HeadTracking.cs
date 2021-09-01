using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeadTracking : MonoBehaviour
{
    public Transform Target;
    public float Radius = 10f;

    List<PointofInterest> POIs;
    // Start is called before the first frame update
    void Start()
    {
        POIs = FindObjectsOfType<PointofInterest>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        Transform tracking = null;
        foreach (PointofInterest poi in POIs)
        {
            Vector3 delta = poi.transform.position - transform.position;
            if(delta.magnitude < Radius)
            {
                tracking = poi.transform;
                break;
            }
        }
        if(tracking != null)
        {
            Target.position = tracking.position;
        }
    }
}
