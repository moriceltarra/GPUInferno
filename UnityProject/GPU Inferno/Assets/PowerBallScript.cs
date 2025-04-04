using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallScript : MonoBehaviour
{
      public Transform jugador; // Asigna aquí el transform del jugador en el Inspector
    public float radio = 2f; // Distancia a la que orbita la bola
    public float velocidadOrbita = 50f; // Velocidad de la órbita

    private float angulo; // Para controlar el ángulo de rotación

    void Update()
    {
        if (jugador != null)
        {
            // Incrementar el ángulo según la velocidad
            angulo += velocidadOrbita * Time.deltaTime;

            // Calcular la nueva posición
            float x = jugador.position.x + Mathf.Cos(angulo) * radio;
            float y = jugador.position.y + Mathf.Sin(angulo) * radio;

            // Establecer la nueva posición de la bola
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
