using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform player;       // Referencia al jugador
    public Transform rightHandPosition; // Posición de la pistola en la mano derecha
    public Transform  leftHandPosition;  // Posición de la pistola en la mano izquierda
    private bool isLeftHand = false; // Para verificar si está mirando a la izquierda
    [SerializeField] private float _gunFireCD = .1f;
    [SerializeField] private GameObject _bulletPrefab;
    private float _lastFireTime = 0f;
    public Transform _bulletSpawnPoint;
    Vector3 mousePos;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateGun();
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= _lastFireTime)
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        _lastFireTime = Time.time + _gunFireCD;

        // Instanciar la bala
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);

        // Obtener la dirección en la que apunta la pistola
        Vector2 direction = (mousePos - _bulletSpawnPoint.position).normalized;

        
    }

    void RotateGun()
    {
        // Obtener la posición del mouse en el mundo
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Asegurar que está en 2D

        // Calcular la dirección entre la pistola y el ratón
        Vector3 direction = mousePos - transform.position;

        // Calcular el ángulo entre la pistola y el mouse en grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Decidir si el mouse está a la izquierda o a la derecha del jugador
        if (mousePos.x < player.position.x)
        {
            // Si el mouse está a la izquierda del jugador, apuntar a la izquierda
            if (!isLeftHand)
            {
                transform.position = rightHandPosition.position; // Cambia la posición
                transform.localScale = new Vector3(13f, -13f, 13f); // Invertir la pistola
                isLeftHand = true; // Establecer que está mirando a la izquierda
            }
        }
        else
        {
            // Si el mouse está a la derecha del jugador, apuntar a la derecha
            if (isLeftHand)
            {
                transform.position = leftHandPosition.position; // Posición normal
                transform.localScale = new Vector3(13f, 13f, 13f); // Restablecer la pistola
                isLeftHand = false; // Establecer que no está mirando a la izquierda
            }
        }

        // Aplicar la rotación de la pistola
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
