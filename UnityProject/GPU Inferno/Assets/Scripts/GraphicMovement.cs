using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicMovement : MonoBehaviour
{
   public float moveSpeed = 5f;  // Velocidad normal
    public float slowSpeedMultiplier = 0.5f; // Reducción de velocidad al esquivar
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capturar input de movimiento (Horizontal y Vertical)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizar la dirección para evitar que diagonales sean más rápidas
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        float speed = moveSpeed;
        
        // Si el jugador mantiene presionado Shift, se mueve más lento
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= slowSpeedMultiplier;
        }

        rb.velocity = movement * speed;
    }

}
