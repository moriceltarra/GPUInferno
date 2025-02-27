using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float spawnRadius = 10f; // Radio en el que aparecerán los enemigos
    public float[] spawnIntervals = {2f, 1.5f, 1f, 0.75f, 0.5f}; // Intervalos de spawn en diferentes minutos
    public GameObject[] enemies; // [0] = Virus, [1] = Cryptocoins, [2] = Chrome Shurikens

    public Transform bottomLeft;  // Esquina inferior izquierda del área permitida
    public Transform topRight;    // Esquina superior derecha del área permitida

    private float elapsedTime = 0f; // Tiempo transcurrido en segundos
    private int maxEnemyIndex = 0; // Índice máximo de enemigos disponibles para spawn
    private int currentIntervalIndex = 0; // Índice actual del intervalo de spawn

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnIntervals[currentIntervalIndex]);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int newIntervalIndex = Mathf.Min((int)(elapsedTime / 60), spawnIntervals.Length - 1);

        if (newIntervalIndex > currentIntervalIndex)
        {
            currentIntervalIndex = newIntervalIndex;
            CancelInvoke("SpawnEnemy");
            InvokeRepeating("SpawnEnemy", 0f, spawnIntervals[currentIntervalIndex]);
        }

        // A los 60 segundos, desbloqueamos Cryptocoins
        if (elapsedTime >= 60f && maxEnemyIndex < 1)
        {
            maxEnemyIndex = 1;
        }

        // A los 180 segundos (3 minutos), desbloqueamos Chrome Shurikens
        if (elapsedTime >= 180f && maxEnemyIndex < 2)
        {
            maxEnemyIndex = 2;
        }
    }

    void SpawnEnemy()
    {
        if (player != null)
        {
            Vector2 spawnPosition;
            int attempts = 10; // Número máximo de intentos para encontrar una posición válida

            do
            {
                spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;

                // Limitar la posición dentro del área permitida
                spawnPosition.x = Mathf.Clamp(spawnPosition.x, bottomLeft.position.x, topRight.position.x);
                spawnPosition.y = Mathf.Clamp(spawnPosition.y, bottomLeft.position.y, topRight.position.y);

                attempts--;
            }
            while (!IsValidSpawnPosition(spawnPosition) && attempts > 0);

            if (attempts > 0) // Solo instancia si encontró una posición válida
            {
                int random = Random.Range(0, maxEnemyIndex + 1); // Solo selecciona enemigos desbloqueados
                Instantiate(enemies[random], spawnPosition, Quaternion.identity);
            }
        }
    }

    bool IsValidSpawnPosition(Vector2 position)
    {
        // Comprobamos si la posición está dentro del área permitida
        return position.x >= bottomLeft.position.x && position.x <= topRight.position.x &&
               position.y >= bottomLeft.position.y && position.y <= topRight.position.y;
    }
}
