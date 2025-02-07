using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class EnemyScript : MonoBehaviour
{
    public Transform target; // El objetivo al que se moverá el personaje
    public float speed = 5f; // Velocidad de movimiento
    NavMeshAgent agent; // Componente NavMeshAgent
    [SerializeField] int life = 1;

    void Start() {
        target = GameObject.Find("GraphicCard").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
        {
            if (target != null)
        {
            agent.SetDestination(target.position);
            agent.speed = speed;       
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
       if(other.tag == "Bullet") {
           Destroy(other.gameObject);
           downLife();
       }
       if(other.name == "GraphicCard") {
            GameObject.Find("Prueba").GetComponent<Pruebas>().downFPS();
            Destroy(gameObject);
       } 
    }
    public void downLife()
    {
        life--;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("resetColor", 0.1f);
    }
    void resetColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
