using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
        public float speed = 10f;  // Velocidad de la bala
    private Vector2 direction; // Dirección en la que se mueve la bala

    void Start()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;  // Asegurarse de que está en 2D

        // Calcular la dirección desde el punto de disparo hacia el ratón
        direction = (mousePos - transform.position).normalized;  // Normalizar para tener una dirección consistente
    }

    void Update()
    {
        // Mover la bala hacia la dirección del ratón
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
