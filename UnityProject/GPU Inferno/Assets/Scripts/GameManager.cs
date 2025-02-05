using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public GameObject enemyPrefab; // Prefab del enemigo a spawnear
    public Transform player; // Referencia al jugador
    public float spawnRadius = 10f; // Radio en el que aparecerán los enemigos
    public float spawnInterval = 2f; // Tiempo entre spawns

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player != null)
        {
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
