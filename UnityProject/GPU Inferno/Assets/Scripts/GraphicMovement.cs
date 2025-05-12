using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraphicMovement : MonoBehaviour
{
    public GameObject powerBall; // Referencia a la bola de poder
    int gunLvL = 1;
    public float moveSpeed = 5f;  // Velocidad normal
    public float slowSpeedMultiplier = 0.5f; // Reducción de velocidad al esquivar
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject arrow;
    public Transform bottomLeft;  // Esquina inferior izquierda del cuadrado
    public Transform topRight;    // Esquina superior derecha del cuadrado
    public GameObject grenadePrefab; // Prefab de la granada
    public float grenadeForce = 4f; // Fuerza de la granada
    public float grenadeTime = 2f; // Tiempo de vida de la granada
    public bool canGrenade = false; // Bandera para controlar el lanzamiento de granadas
    public int grenadeToThrow = 1; // Contador de granadas lanzadas
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
        if(canGrenade){
             StartCoroutine(throwGranade());
             canGrenade = false;
        }
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

        // Calcular la nueva posición
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;

        // Limitar la posición dentro del cuadrado
        newPosition.x = Mathf.Clamp(newPosition.x, bottomLeft.position.x, topRight.position.x);
        newPosition.y = Mathf.Clamp(newPosition.y, bottomLeft.position.y, topRight.position.y);

        // Aplicar la nueva posición
        rb.MovePosition(newPosition);
    }
    IEnumerator throwGranade()
    {
        while (true)
        {
            for(int i = 0; i < grenadeToThrow; i++)
            {
                GameObject grenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);
            
                Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;

                grenade.GetComponent<GrenadeScript>().velocity = dir * 50f;

            }

            yield return new WaitForSeconds(grenadeTime);
        }
    }
    public void downLife()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("resetcolor", 0.3f);
    }
    private void resetcolor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void ActivatedGun(String name)
    {
        if (name.Contains("PowerBall"))
        {
            powerBall.SetActive(true);
            return;
        }
        if (name.Contains("Grenade")){
            canGrenade = true;
            return;
        }
        if (gunLvL >= 2)
        {
            transform.Find("GunLvL" + (gunLvL - 1)).gameObject.SetActive(false);
        }
        name = name.Replace("(Clone)", "");
        transform.Find("Gun" + name).gameObject.SetActive(true);
        gunLvL++;
    }
    //Para darle la flecha al weapon que señala
    public GameObject getArrow()
    {
        return arrow;
    }
}
