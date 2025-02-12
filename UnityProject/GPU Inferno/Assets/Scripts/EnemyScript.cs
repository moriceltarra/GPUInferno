using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public enum EnemyType
{
    CPU,
    GPU
}
public class EnemyScript : MonoBehaviour
{
    public Transform target; // El objetivo al que se moverá el personaje
    public float speed = 5f; // Velocidad de movimiento
    NavMeshAgent agent; // Componente NavMeshAgent
    public EnemyType enemyType;
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
        if(enemyType == EnemyType.CPU){
            GameObject.Find("Prueba").GetComponent<Pruebas>().CPUdelay();
        }
        if(enemyType == EnemyType.GPU){
            GameObject.Find("Prueba").GetComponent<Pruebas>().GPUdelay();
        }
            Destroy(gameObject);
       } 
    }
    public void downLife()
    {
        life--;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("resetColor", 0.1f);
    }
    private void changeColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("resetColor", 0.1f);
    }
    private void resetcolor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void resetColor()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
