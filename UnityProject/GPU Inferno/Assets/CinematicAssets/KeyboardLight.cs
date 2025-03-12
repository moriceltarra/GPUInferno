using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KeyboardLight : MonoBehaviour
{
    public Light2D Light2D;
    public float speed = 2.0f; // Ajusta la velocidad del cambio de color
    private float hue = 0f;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        hue += Time.deltaTime * speed;
        if (hue > 1f) hue -= 1f;
        
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        Light2D.color = rainbowColor;
    }
}
