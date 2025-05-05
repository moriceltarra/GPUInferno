using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBallScript : MonoBehaviour
{
      public Transform jugador; // Asigna aquí el transform del jugador en el Inspector
    public float radio = 2f; // Distancia a la que orbita la bola
    public float velocidadOrbita = 50f; // Velocidad de la órbita

    private float angulo; // Para controlar el ángulo de rotación
    public GameObject PowerBall2; // Asigna aquí la bola de poder en el Inspector   
    private bool firstBall = true; // Bandera para la primera bola de poder

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
        if(Input.GetKeyDown(KeyCode.E) && firstBall == true){
            lvlBallUp();
        }
    }
    public void lvlBallUp(){
        // Aumentar la velocidad de órbita y el radio
        velocidadOrbita += 0.05f; // Aumenta la velocidad de órbita
        if(firstBall == true){
            GameObject newBall= Instantiate(PowerBall2, transform.position, Quaternion.identity); // Instancia la bola de poder 2
            PowerBallScript newBallScript = newBall.GetComponent<PowerBallScript>();
            newBallScript.firstBall = false; // Cambia la bandera para la nueva bola
            newBallScript.jugador = jugador; // Asigna el transform del jugador a la nueva bola
            newBallScript.velocidadOrbita = 1f; // Asigna la velocidad de órbita a la nueva bola
            StartCoroutine(AnotherBall()); // Llama a la coroutine para instanciar otra bola
        }
    }

    IEnumerator AnotherBall(){
        // Espera 5 segundos antes de instanciar la nueva bola
        yield return new WaitForSeconds(0.5f);
        GameObject newBall= Instantiate(PowerBall2, transform.position, Quaternion.identity); // Instancia la bola de poder 2
        PowerBallScript newBallScript = newBall.GetComponent<PowerBallScript>();
        newBallScript.firstBall = false; // Cambia la bandera para la nueva bola
        newBallScript.jugador = jugador; // Asigna el transform del jugador a la nueva bola
        newBallScript.velocidadOrbita = 1f; // Asigna la velocidad de órbita a la nueva bola
    }
}
