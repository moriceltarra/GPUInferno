using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float attractionRange = 5f; // Distancia a la que las monedas se empiezan a mover hacia el jugador
    public float attractionSpeed = 10f; // Velocidad de atracción
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asegúrate de que el jugador tenga el tag "Player"
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < attractionRange) // Si está dentro del rango, mover la moneda hacia el jugador
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, attractionSpeed * Time.deltaTime);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<LevelScript>().PickCoin();
            Destroy(gameObject);
        }
    }
}
