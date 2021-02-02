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
    
    public int window = 100;

    public AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldTalk == true)
        {
            ScaleToAudioSource();
        }

    }

    private void ScaleToAudioSource()
    {
        // get window of audio source output
        var samples = new float[6000];
        audioSource.GetOutputData(samples, 0);

        float total = 0;
        foreach (var sample in samples)
        {
            total += sample * sample;
        }
        float scale = Mathf.Sqrt(total / samples.Length);

        // remap for scaling
        scale = scale.Remap(0.25f, 0f, min, max);

        transform.localScale = new Vector3(scale, scale, scale);
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


