using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float spawnRadius = 10f; // Radio en el que aparecerán los enemigos
    public float spawnInterval = 2f; // Tiempo entre spawns
    public GameObject[] enemies;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);

    }

    void SpawnEnemy()
    {
        if (player != null)
        {
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            int random = Random.Range(0, 3);
            Instantiate(enemies[random], spawnPosition, Quaternion.identity);
        }
    }
}
