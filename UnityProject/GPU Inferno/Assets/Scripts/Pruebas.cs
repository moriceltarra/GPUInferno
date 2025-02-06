using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebas : MonoBehaviour
{
    private float deltaTime = 0.0f;
    int delay=0;
    public int sumDelay=6000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
           delay+=sumDelay;
            GameObject obj = new GameObject("TempObject");
            Destroy(obj);
        }
        for(int i=0; i<delay; i++)
        {
            Debug.Log("Prueba");
        }
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        
        
    }
     void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 150, 50), Mathf.Ceil(fps).ToString() + " FPS", style);
    }
}
