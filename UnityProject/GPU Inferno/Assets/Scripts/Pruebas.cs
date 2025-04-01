using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Pruebas : MonoBehaviour
{
    public Light2D light2D;
    private float deltaTime = 0.0f;
    int delay = 0;
    public int delayCPU = 0;
    public int delayGPU = 0;
    public int sumDelay = 6000;
    bool firstTime = true;
    public Font customFont; // Asigna la fuente desde el Inspector
    bool isPause = false;
    public GameObject PauseMenu;
    public GameObject lose;
    public GameObject graphic;
    public AnimatedCursor animatedCursor;
    
    void Start()
    {
        Application.targetFrameRate = 1000; // Puedes poner un valor alto o -1 para ilimitado
        QualitySettings.vSyncCount = 0; // Desactiva la sincronización vertical (VSync)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                animatedCursor.setPause(false);
                Time.timeScale = 1f;
                PauseMenu.SetActive(false);
                isPause = false;
                
            }
            else
            {
                animatedCursor.setPause(true);
                Time.timeScale = 0f;
                PauseMenu.SetActive(true);
                isPause = true;
                
            }

        }
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
        Time.timeScale = 0f;

    }

    void OnGUI()
    {
        if (!isPause)
        {
            float fps = 1.0f / deltaTime;

            GUIStyle style = new GUIStyle();
            style.fontSize = 60;
            style.font = customFont;

            float startChange = 300f; // FPS donde empieza a cambiar el color
            float endChange = 30f;   // FPS donde ya es completamente rojo

            // Normaliza el valor de FPS entre 1 y 0, asegurando que en 30 FPS ya es rojo
            float t = Mathf.InverseLerp(endChange, startChange, fps);
            float t2 = Mathf.InverseLerp(30, 90, fps);
            // Mezcla de colores de verde a rojo
            style.normal.textColor = Color.Lerp(Color.red, Color.green, t);
            light2D.color = Color.Lerp(Color.red, Color.white, t2);
            if (fps > 30)
            {
                firstTime = false;
            }

            if (fps < 30 && !firstTime)
            {
                // Mostrar mensaje de derrota en rojo
                GUIStyle loseStyle = new GUIStyle(style);
                loseStyle.normal.textColor = Color.red;
                GUI.Label(new Rect(10, 60, 300, 50), "You Lose", loseStyle);
                graphic.SetActive(false);
                lose.SetActive(true);
                animatedCursor.setPause(true);
                GUI.Label(new Rect(10, 10, 300, 50), Mathf.Ceil(30).ToString() + " FPS", style);
            }else{
                 GUI.Label(new Rect(10, 10, 300, 50), Mathf.Ceil(fps).ToString() + " FPS", style);
            }
           



            // Mostrar el número de procesos de delay en la esquina superior derecha
            GUIStyle delayStyle = new GUIStyle();
            delayStyle.fontSize = 40; // Tamaño de fuente más pequeño
            delayStyle.font = customFont;
            delayStyle.normal.textColor = Color.white; // Color blanco para el texto

            // Muestra el número de procesos de delay
            GUI.Label(new Rect(10, Screen.height - 20 - 60, 300, 50),
                      "CPU Processors: " + (int)(delayCPU * 1.3237) + "\nGPU polygons: " + (int)(delayGPU * 1.13545), delayStyle);
        }

    }
    public void returnMenu(){
        SceneManager.LoadScene("InitialMenu");


    }
    public void restart(){
        SceneManager.LoadScene("Gameplay");


    }
}
