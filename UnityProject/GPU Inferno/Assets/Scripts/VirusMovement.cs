using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
   public Transform target; // El objetivo al que se moverá el personaje
    public float speed = 5f; // Velocidad de movimiento

    void Start() {
        target = GameObject.Find("Capsule").transform;
    }

    void Update()
    {
        if (target != null)
    {
        // Mueve hacia el jugador
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Separación de otros enemigos cercanos
        Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, 1f);  // Radio de detección

        foreach (Collider enemy in nearbyEnemies)
        {
            if (enemy.gameObject != this.gameObject)  // Asegurarse de que no se detecte a sí mismo
            {
                Vector3 pushDirection = transform.position - enemy.transform.position;
                transform.position += pushDirection.normalized * 0.1f;  // Empujar ligeramente para evitar la superposición
            }
        }
    }
    }
    
}
