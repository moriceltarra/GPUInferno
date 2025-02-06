using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VirusMovement : MonoBehaviour
{
   public Transform target; // El objetivo al que se moverá el personaje
    public float speed = 5f; // Velocidad de movimiento
    NavMeshAgent agent; // Componente NavMeshAgent

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
    }
    
}
