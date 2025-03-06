using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebas : MonoBehaviour
{
      private float deltaTime = 0.0f;
    int delay = 0;
    public int delayCPU = 0;
    public int delayGPU = 0;
    public int sumDelay = 6000;
    bool firstTime = true;
    public Font customFont; // Asigna la fuente desde el Inspector

    void Start()
    {
        Application.targetFrameRate = 500; // Puedes poner un valor alto o -1 para ilimitado
        QualitySettings.vSyncCount = 0; // Desactiva la sincronización vertical (VSync)
    }

    void Update()
    {
        // Debug.Log("Tarjeta gráfica: " + SystemInfo.graphicsDeviceName);
        for (int i = 0; i < delayCPU; i++)
        {
            float x = Mathf.Sqrt(i) * Mathf.Sin(i) * Mathf.Cos(i); // Operaciones matemáticas inútiles
        }

        // Crear muchos objetos en la escena (Carga en GPU)
        for (int i = 0; i < delayGPU / 100; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(-1000, -1000, -1000);
            cube.AddComponent<Rigidbody>(); // Añadir física también ayuda a consumir CPU
            Destroy(cube, 0.5f);
        }
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void CPUdelay()
    {
        Debug.Log("FPS down");
        delayCPU += 1000;
    }

    public void GPUdelay()
    {
        Debug.Log("FPS down");
        delayGPU += 1000;
    }

    void pause()
    {
        Debug.Break();
    }

    void OnGUI()
    {
        float fps = 1.0f / deltaTime;

        GUIStyle style = new GUIStyle();
        style.fontSize = 60;
        style.font = customFont;

        float startChange = 300f; // FPS donde empieza a cambiar el color
        float endChange = 30f;   // FPS donde ya es completamente rojo

        // Normaliza el valor de FPS entre 1 y 0, asegurando que en 30 FPS ya es rojo
        float t = Mathf.InverseLerp(endChange, startChange, fps);

        // Mezcla de colores de verde a rojo
        style.normal.textColor = Color.Lerp(Color.red, Color.green, t);

        GUI.Label(new Rect(10, 10, 300, 50), Mathf.Ceil(fps).ToString() + " FPS", style);

        if (fps > 15)
        {
            firstTime = false;
        }

        if (fps < 15 && !firstTime)
        {
            // Mostrar mensaje de derrota en rojo
            GUIStyle loseStyle = new GUIStyle(style);
            loseStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(10, 60, 300, 50), "You Lose", loseStyle);
            pause();
        }

        // Mostrar el número de procesos de delay en la esquina superior derecha
        GUIStyle delayStyle = new GUIStyle();
        delayStyle.fontSize = 40; // Tamaño de fuente más pequeño
        delayStyle.font = customFont;
        delayStyle.normal.textColor = Color.white; // Color blanco para el texto

        // Muestra el número de procesos de delay
        GUI.Label(new Rect(10, Screen.height-20 - 60, 300, 50),
                  "CPU Processors: " + (int)(delayCPU*1.3237) + "\nGPU polygons: " + (int)(delayGPU*1.13545), delayStyle);
    }

}
