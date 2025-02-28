using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
     public Transform player;       // Referencia al jugador
    public Transform rightHandPosition; // Posición de la pistola en la mano derecha
    public Transform leftHandPosition;  // Posición de la pistola en la mano izquierda
    private bool isLeftHand = false; // Para verificar si está mirando a la izquierda
    [SerializeField] private float _gunFireCD = .1f;
    [SerializeField] private GameObject _bulletPrefab;
    private float _lastFireTime = 0f;
    public Transform _bulletSpawnPoint;
    Vector3 mousePos;

    private LineRenderer lineRenderer;

    void Start()
    {
        // Agregar LineRenderer si no está en el objeto
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configurar propiedades del LineRenderer
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Material transparente
        lineRenderer.startColor = new Color(1f, 0f, 0f, 0.5f); // Rojo semi-transparente
        lineRenderer.endColor = new Color(1f, 0f, 0f, 0f);    // Se desvanece
        lineRenderer.positionCount = 2; // Dos puntos (inicio y fin)
    }

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
            if (!isLeftHand)
            {
                transform.position = rightHandPosition.position; 
                transform.localScale = new Vector3(13f, -13f, 13f); 
                isLeftHand = true;
            }
        }
        else
        {
            if (isLeftHand)
            {
                transform.position = leftHandPosition.position; 
                transform.localScale = new Vector3(13f, 13f, 13f);
                isLeftHand = false;
            }
        }

        // Aplicar la rotación de la pistola
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Dibujar la línea apuntando
        Vector3 startPos = _bulletSpawnPoint.position;
        Vector3 endPos = startPos + direction * 5f; // 5 unidades de longitud
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
