using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotate : MonoBehaviour
{
  
    void Update()
    {
        //Rotate the shuriken
        Debug.Log(1000 * Time.deltaTime);
        Debug.Log(Time.deltaTime+"Time.deltaTime");
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
    }
}
