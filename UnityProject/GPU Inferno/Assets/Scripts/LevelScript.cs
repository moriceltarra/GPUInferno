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
    public GameObject[] guns;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Gun "+guns[1].name);
        for (int i = 0; i < levelImages.Length; i++)
        {
            levelImages[i].enabled = false;

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
                for(int i=0;i<4;i++){
                    Debug.Log("Nivel SUBIDO "+guns[i].GetComponent<GunScript>()._gunFireCD);
                    if(guns[i].activeInHierarchy){
                        
                        guns[i].GetComponent<GunScript>().levelUp();
                        Debug.Log("Nivel SUBIDO "+guns[i].GetComponent<GunScript>()._gunFireCD);
                    }
                }
            }
            else
            {
                levelImages[barLevel - 1].enabled = true;
            }
    }
}
