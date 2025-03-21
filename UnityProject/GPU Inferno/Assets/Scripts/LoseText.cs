using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Referencia al texto en pantalla
    public float typingSpeed = 0.05f;      // Velocidad de escritura

    private string fullText;               // Texto completo que se va a mostrar

    void Start()
    {
        string gpuName = SystemInfo.graphicsDeviceName;
        fullText = "Your FPS have dropped too much… Your "+gpuName+" is done.";   // Guardamos el texto original
        textComponent.text = "";         // Lo borramos para empezar de cero
        StartCoroutine(TypeText());      // Iniciamos la animación de escritura
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText) // Recorre cada letra del texto
        {
            textComponent.text += letter;  // Agrega la letra al texto mostrado
            yield return new WaitForSeconds(typingSpeed); // Espera antes de la siguiente letra
        }
    }
}
