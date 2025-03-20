using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdviceDisapear : MonoBehaviour
{
    public float time=3f;
    void Start()
    {
        StartCoroutine("disapear");
    }

    IEnumerator disapear(){
        
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    } 
   
}
