using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KITalk : MonoBehaviour
{
    public bool shouldTalk = false;

    // KI komplex min 0.007 max 0.008
    // KI einfach min 0.9 max 1
    public float min = 0.9f;
    public float max = 1f;

    public float sin = 0f;
    public float speedSin = 3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldTalk == true)
        {
            ScaleObject();
        }

    }

    private void ScaleObject()
    {
        sin = Mathf.Sin(Time.time * speedSin); // -1 - 1
        sin = sin.Remap(-1f, 1f, min, max);
        Vector3 vec = new Vector3(sin, sin, sin);

        transform.localScale = vec;
    }
}

public static class ExtensionMethods
{
    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

}


