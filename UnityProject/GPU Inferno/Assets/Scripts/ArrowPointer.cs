using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform player;   // Referencia al jugador
    public Transform target;   // Referencia al objetivo
    public float radius = 2f;  // Distancia fija de la flecha al jugador
    public float floatAmplitude = 0.2f;  // Cuánto se mueve adelante y atrás
    public float floatSpeed = 2f;        // Velocidad de la oscilación

    private float timeOffset;  // Offset aleatorio para que no todas las flechas (si hay varias) se muevan igual

    void Start()
    {
        timeOffset = Random.Range(0f, Mathf.PI * 2f); // Para variar la animación si hay más de una flecha
    }

    void Update()
    {
        if (player == null || target == null) return;

        // Obtener dirección del jugador al objetivo
        Vector3 direction = (target.position - player.position).normalized;

        // Movimiento de flotación (atrás y adelante)
        float offset = Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatAmplitude;

        // Colocar la flecha en el punto más cercano al objetivo con oscilación
        transform.position = player.position + direction * (radius + offset);

        // Rotar la flecha para que apunte al objetivo
        float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToTarget);
    }
    public void ActivateArrow(Transform target)
    {
        this.target = target;
    }
}
