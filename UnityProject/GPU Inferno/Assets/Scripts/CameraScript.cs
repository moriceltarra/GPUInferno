using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.x <= -38.11999 || target.position.x >= 37.54374)
        {
            if (target.position.y <= -41.28207 || target.position.y >= 41.28207)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
        }
        else if (target.position.y <= -41.28207 || target.position.y >= 41.28207)
        {
            if (target.position.x <= -38.11999 || target.position.x >= 37.54374)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        

        
    }
}
