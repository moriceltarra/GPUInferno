using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleLight : MonoBehaviour
{
    public Light2D light2D;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float speed = 2f; 
     

    
    void Update()
    {
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * speed, 1));
    }
}
