using UnityEngine;
using System.Collections;

// Will rotate given objects to look in the direction of target gameObject ("CenterEyeAnchor" recommended for rift)
// If the object is close enough, and within default angle.
// Script is made for imported MMD models, and relies on the animation setting the default angle every frame for the object

[System.Serializable]
public class LookingEntry
{
    public GameObject item;
    public float maximumAngle = 150f;
    public float turnSpeed = 1f;
    public float maximumRectionDistance = 30f;
    [Range(0.1f, 1.0f)]
    public float turnPercentage = 1f;
}

public class CameraLook : MonoBehaviour
{
    public GameObject target;
    public LookingEntry[] parts;

    Quaternion[] rotations;

    // Use this for initialization

    void Start()
    {
        rotations = new Quaternion[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            rotations[i] = parts[i].item.transform.rotation;
        }
    }

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Follow");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Follow");
        GameObject item = GameObject.FindGameObjectWithTag("Head");
        LookingEntry entry;
        for (int i = 0; i < parts.Length; i++)
        {
            entry = parts[i];
            item = entry.item;
            if (item != null)
            {
                Vector3 angle = (target.transform.position - item.transform.position).normalized;

                Quaternion lookRotation;

                if ((Vector3.Angle(item.transform.forward, angle) < entry.maximumAngle) && (Vector3.Distance(item.transform.position, target.transform.position) < entry.maximumRectionDistance))
                {
                    lookRotation = Quaternion.Slerp(item.transform.rotation, Quaternion.LookRotation(angle), entry.turnPercentage);
                }
                else
                {
                    lookRotation = item.transform.rotation;
                }
                rotations[i] = item.transform.rotation = Quaternion.Slerp(rotations[i], lookRotation, Time.deltaTime * entry.turnSpeed);
            }
        }
    }
}