using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class WeaponSctipt : MonoBehaviour
{
     public Light2D light;
    public float fallOff = 0.23f;
    public float pulseSpeed = 2f;  // Velocidad de la pulsación
    public float pulseAmount = 0.1f; // Intensidad de la variación
    public float scaleAmount = 0.1f; // Cuánto aumenta la escala

    private Vector3 initialScale; // Escala inicial del objeto

    void Start()
    {
        initialScale = transform.localScale; // Guarda la escala inicial
    }

    void Update()
    {
        ChangeLightFallOff();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GraphicCard")
        {
            collision.gameObject.GetComponent<GraphicMovement>().ActivatedGun(gameObject.name);
        }
    }

    void ChangeLightFallOff()
    {
        // Calcula la variación con una onda senoidal
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        // Modifica la luz
        light.falloffIntensity = fallOff + pulse;

        // Modifica la escala del objeto, asegurando que nunca sea menor que la original
        float scaleFactor = 1 + pulse * scaleAmount;
        transform.localScale = initialScale * scaleFactor;
    }
}
