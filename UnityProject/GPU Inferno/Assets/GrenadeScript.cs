using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public Animator animator; // Referencia al Animator de la granada
    public float explosionDelay = 2f; // Tiempo antes de que la granada explote
    public Vector2 velocity;
    public float deceleration = 0.3f;

    void Update()
    {
        transform.position += (Vector3)(velocity * Time.deltaTime);

        // Simula fricción/desaceleración
        velocity = Vector2.Lerp(velocity, Vector2.zero, Time.deltaTime * deceleration);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Inicia la coroutine para la explosión
        StartCoroutine(Explotion());
        
    }
    
    IEnumerator Explotion()
    {
        yield return new WaitForSeconds(1f); // Espera 2 segundos antes de explotar
        animator.SetTrigger("Explode"); // Activa la animación de explosión
        yield return new WaitForSeconds(0.5f); // Espera medio segundo para que la animación termine
        Destroy(gameObject); // Destruye la granada al explotar
    }
    
}
