using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public Image[] levelImages;
    public TextMeshProUGUI levelText;
    private int level = 0;
    private int barLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levelImages.Length; i++)
        {
            levelImages[i].enabled = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsas espacio sube de nivel
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("Subir de nivel");
            Debug.Log("Nivel: " + level);
            
            Debug.Log("Barra de nivel: " + barLevel);
            barLevel++;
            if (barLevel > levelImages.Length)
            {
                for (int i = 0; i < levelImages.Length; i++)
                {
                    levelImages[i].enabled = false;

                }
                barLevel = 0;
                level++;
            }
            else
            {
                levelImages[barLevel - 1].enabled = true;
            }

        }
    }
    public void PickCoin()
    {
        Debug.Log("Moneda recogida");
        Debug.Log("Subir de nivel");
            Debug.Log("Nivel: " + level);
            Debug.Log("Barra de nivel: " + barLevel);
            
            barLevel++;
            if (barLevel > levelImages.Length)
            {
                for (int i = 0; i < levelImages.Length; i++)
                {
                    levelImages[i].enabled = false;

                }
                barLevel = 0;
                level++;
                levelText.text = "LVL " + level;
            }
            else
            {
                levelImages[barLevel - 1].enabled = true;
            }
    }
}
