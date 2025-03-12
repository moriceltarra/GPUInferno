using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CinematicScript : MonoBehaviour
{
    public GameObject[] VirusAdvice;
    public GameObject FirstPart; // Primer objeto activo
    public GameObject secondPart;    // Objeto que se activa después
    public Transform targetPosition; // Posición final del objeto a mover
    public float moveSpeed = 1f;     // Velocidad del movimiento
    public GameObject cursor;
    public GameObject barDownload;
    public GameObject thirdPart;
    public GameObject graphicCard;
    public Transform GraphicPoint;
    public Light2D l2d;

    void Start()
    {
        StartCoroutine(Inicio());
    }
    private IEnumerator Inicio(){
        yield return new WaitForSeconds(1f);
        StartCoroutine(SwitchAndMove());
    }
    private IEnumerator SwitchAndMove()
    {
        // 1. Desactivar objeto inicial y activar SecondPart
        FirstPart.SetActive(false);
        secondPart.SetActive(true);

        // 2. Mover el objeto
        yield return StartCoroutine(MoveCursor(cursor, targetPosition.position));

        // 3. Esperar 2 segundos
        barDownload.SetActive(true);
        yield return new WaitForSeconds(1f);
        //Tercera parte
        secondPart.SetActive(false);
        thirdPart.SetActive(true);
        //reutilizo el codigo de lo del cursor para mover la grafica 
        yield return StartCoroutine(MoveCursor(graphicCard, GraphicPoint.position));
        
        yield return StartCoroutine(SpawnWarning());
        yield return new WaitForSeconds(1.5f);
        // 4. Desactivar SecondPart y volver a activar el objeto inicial
        thirdPart.SetActive(false);
        FirstPart.SetActive(true);
        GameObject.Find("cinematicfreak").GetComponent<Animator>().SetTrigger("Angry");
        
    }

    private IEnumerator MoveCursor(GameObject cursor, Vector3 target)
    {
        while (Vector3.Distance(cursor.transform.position, target) > 0.4f)
        {
            cursor.transform.position = Vector3.MoveTowards(cursor.transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    private IEnumerator SpawnWarning(){
        l2d.color=new Color(1f, 0.1556604f, 0.1556604f);
        for(int i=0;i<VirusAdvice.Length;i++){
            VirusAdvice[i].SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return null;
    }
}
