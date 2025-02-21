using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenRotate : MonoBehaviour
{
  
    void Update()
    {
        //Rotate the shuriken
        
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
    }
}
