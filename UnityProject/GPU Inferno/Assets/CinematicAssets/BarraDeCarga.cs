using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDeCarga : MonoBehaviour
{
       public float duracion = 3f; // Tiempo total de la carga
    private Vector3 escalaInicial;
    private Vector3 posicionInicial;

    private void Start()
    {
        escalaInicial = transform.localScale;  // Tamaño original de la barra
        posicionInicial = transform.position; // Guarda la posición inicial
        StartCoroutine(LlenarBarra());
    }

    private System.Collections.IEnumerator LlenarBarra()
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracion;
            
            // Cambia solo la escala X y ajusta la posición para que crezca desde la izquierda
            transform.localScale = new Vector3(progreso * escalaInicial.x, escalaInicial.y, escalaInicial.z);
            transform.position = new Vector3(posicionInicial.x + (progreso * escalaInicial.x) / 2, posicionInicial.y, posicionInicial.z);

            yield return null;
        }

        transform.localScale = escalaInicial; // Asegura que termine al 100%
        transform.position = new Vector3(posicionInicial.x + escalaInicial.x / 2, posicionInicial.y, posicionInicial.z);
    }

}
