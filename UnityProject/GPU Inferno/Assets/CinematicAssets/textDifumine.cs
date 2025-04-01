using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class textDifumine : MonoBehaviour
{
    public TMP_Text  text; // Arrastra aquí el Text en el Inspector
    public float fadeDuration = 2f; // Tiempo que tarda en hacerse opaco

    void Start()
    {
        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        Color color = text.color;
        color.a = 0; // Comienza invisible
        text.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            text.color = color;
            yield return null;
        }

        color.a = 1;
        text.color = color; // Asegurar que quede opaco
        SceneManager.LoadScene("Gameplay");
    }
}
