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
    private bool canDropWeapon = true;
    public GameObject WeaponToDrop;
    private GameObject player;
    public Transform target; // El objetivo al que se moverá el personaje
    public float speed = 5f; // Velocidad de movimiento
    NavMeshAgent agent; // Componente NavMeshAgent
    public EnemyType enemyType;
    [SerializeField] int life = 1;
    private Animator animator;
    private CircleCollider2D collider;
    private AudioSource audioSource;
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        player = GameObject.Find("GraphicCard");
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        transform.rotation = Quaternion.Euler(0, 0, 0);

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
            speed = 0;
            collider.enabled = false;
            animator.Play("Death");
            Invoke("DestroyEnemy", 0.5f);
        }


    }
    //Metodo Para asignarle El objeto que suelta si esque lo suelta
    public void SetWeaponToDrop(GameObject weapon)
    {
        WeaponToDrop = weapon;
    }

    public void DestroyEnemy()
    {
        if (WeaponToDrop != null && canDropWeapon)
        {
            canDropWeapon = false;
            GameObject weapon = Instantiate(WeaponToDrop, transform.position, Quaternion.identity);
            if (!weapon.CompareTag("Coin"))
            {
                GameObject arrow = player.GetComponent<GraphicMovement>().getArrow();
                arrow.SetActive(true);
                arrow.GetComponent<ArrowPointer>().ActivateArrow(weapon.transform);
            }
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Te ha golpeado "+other.name);
        if (other.CompareTag("Bullet"))
        {
            
            if (other.name.Contains("Bullet"))
            {
                Destroy(other.gameObject);
                downLife();
            }
            else if (other.name == "PowerBall")
            {
                for (int i = 0; i < 8; i++)
                {
                    downLife();
                }
            }
            else
            {
                StartCoroutine(DamageOverTime(0.2f));
            }
        }
        if (other.name == "GraphicCard")
        {
            if (enemyType == EnemyType.CPU)
            {
                GameObject.Find("Prueba").GetComponent<Pruebas>().CPUdelay(1200);
                animator.Play("Death");
                Invoke("DestroyEnemy", 0.5f);
                //cambia color de la tarjeta
                GameObject.Find("GraphicCard").GetComponent<GraphicMovement>().downLife();
            }
            if (enemyType == EnemyType.GPU)
            {
                GameObject.Find("Prueba").GetComponent<Pruebas>().GPUdelay(1100);
                animator.Play("Death");
                Invoke("DestroyEnemy", 0.5f);
                //cambia color de la tarjeta
                GameObject.Find("GraphicCard").GetComponent<GraphicMovement>().downLife();
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "LaserRay")
        {
            StopCoroutine(DamageOverTime(0f));
        }
    }
    IEnumerator DamageOverTime(float interval)
    {
        while (true)
        {
            downLife();
            audioSource.Play();
            yield return new WaitForSeconds(interval);
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
