using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Weapons;
    private int gunLvL = 0;
    public Transform player; // Referencia al jugador
    public float spawnRadius = 0.5f; // Radio en el que aparecerán los enemigos
    public float[] spawnIntervals = { 2f, 1.5f, 1f, 0.75f, 0.5f }; // Intervalos de spawn en diferentes minutos
    public GameObject[] enemies; // [0] = Virus, [1] = Cryptocoins, [2] = Chrome Shurikens
    public GameObject CoinDrop; // Moneda que suelta el enemigo
    public Transform bottomLeft;  // Esquina inferior izquierda del área permitida
    public Transform topRight;    // Esquina superior derecha del área permitida
    public int[] probabilityOfDrop; // Probabilidad de que un enemigo suelte un arma
    private float elapsedTime = 0f; // Tiempo transcurrido en segundos
    private int maxEnemyIndex = 0; // Índice máximo de enemigos disponibles para spawn
    private int currentIntervalIndex = 0; // Índice actual del intervalo de spawn
    //para saber si se puede dropear un arma
    public bool canDrop = false;
    private bool firstTime = true;
    public GameObject[] advices; 

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
        if (elapsedTime >= 30f && maxEnemyIndex < 1)
        {
            maxEnemyIndex = 1;
            advices[1].SetActive(true);
        }

        // A los 180 segundos (3 minutos), desbloqueamos Chrome Shurikens
        if (elapsedTime >= 60f && maxEnemyIndex < 2)
        {
            maxEnemyIndex = 2;
            advices[2].SetActive(true);
        }
        if (elapsedTime >= 120f && maxEnemyIndex < 3)
        {
            maxEnemyIndex = 3;
            advices[3].SetActive(true);
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
                GameObject enemy = Instantiate(enemies[random], spawnPosition, Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<EnemyScript>().SetWeaponToDrop(CoinDrop);
                Debug.Log("Enemy: " + enemy.name + " " + gunLvL + " " + GetObjectIndex(enemies, enemy));
                //Si el enemigo es el mismo que el arma que le toca dropear
                if (canDrop == true || firstTime == true)
                {
                    if (gunLvL == GetObjectIndex(enemies, enemy))
                    {
                        if (Random.Range(0, 100) < probabilityOfDrop[gunLvL])
                        {
                            canDrop = false;
                            firstTime = false;
                            Debug.Log("Se va a dropear un arma");
                            AddWeapon(enemy);
                        }
                    }
                }
                         
            }
        }
    }
    void AddWeapon(GameObject enemy)
    {
        enemy.GetComponent<EnemyScript>().SetWeaponToDrop(Weapons[gunLvL]);
        Debug.Log("Drop Arma");
        gunLvL++;
    }
    bool IsValidSpawnPosition(Vector2 position)
    {
        // Comprobamos si la posición está dentro del área permitida
        return position.x >= bottomLeft.position.x && position.x <= topRight.position.x &&
               position.y >= bottomLeft.position.y && position.y <= topRight.position.y;
    }
    //Para saber el nivel del objeto en el array
    int GetObjectIndex(GameObject[] array, GameObject target)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i].gameObject + " " + target.gameObject.name.Replace("(Clone)", ""));
            if (array[i].gameObject.name == target.gameObject.name.Replace("(Clone)", "")) // Compara referencias
            {
                return i; // Devuelve el índice si lo encuentra
            }
        }
        return -1; // Devuelve -1 si no está en el array
    }

}
