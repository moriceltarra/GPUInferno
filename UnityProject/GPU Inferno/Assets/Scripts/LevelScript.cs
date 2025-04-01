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
    public float downSize=0.0001f;
    public Pruebas pruebas;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Gun " + guns[1].name);
        for (int i = 0; i < levelImages.Length; i++)
        {
            levelImages[i].enabled = false;

        }
    }


    public void PickCoin()
    {
        barLevel++;
        if (barLevel > levelImages.Length)
        {
            LvlUp();
        }
        else
        {
            levelImages[barLevel - 1].enabled = true;
        }
    }
    public void LvlUp()
    {
        
        for (int i = 0; i < levelImages.Length; i++)
            {
                levelImages[i].enabled = false;

            }
            barLevel = 0;
            level++;
            levelText.text = "LVL " + level;
        panel.SetActive(true);
        Time.timeScale = 0f;    
         for (int i = 0; i < 4; i++)
            {       
                
                if (guns[i].activeInHierarchy)
                {
                    guns[i].GetComponent<GunScript>().isPause = true;
                }
            }
    }
    public void DownSize(){
        transform.localScale = new Vector3(transform.localScale.x - downSize, transform.localScale.y - downSize, transform.localScale.z - downSize);
        panel.SetActive(false);
        Time.timeScale = 1f;
        for (int i = 0; i < 4; i++)
            {       
                
                if (guns[i].activeInHierarchy)
                {
                    guns[i].GetComponent<GunScript>().isPause = false;
                }
            }
    }
    public void UpCD(){
        for (int i = 0; i < 4; i++)
            {       
                Debug.Log("Nivel SUBIDO " + guns[i].GetComponent<GunScript>()._gunFireCD);
                if (guns[i].activeInHierarchy)
                {
                    guns[i].GetComponent<GunScript>().levelUp();
                    Debug.Log("Nivel SUBIDO " + guns[i].GetComponent<GunScript>()._gunFireCD);
                }
            }
            panel.SetActive(false);
            Time.timeScale = 1f;
            for (int i = 0; i < 4; i++)
            {       
                
                if (guns[i].activeInHierarchy)
                {
                    guns[i].GetComponent<GunScript>().isPause = false;
                }
            }
    }
    public void UpFPS(){
        pruebas.CPUdelay(-1000);
        pruebas.GPUdelay(-400);
        panel.SetActive(false);
        Time.timeScale = 1f;
        for (int i = 0; i < 4; i++)
            {       
                
                if (guns[i].activeInHierarchy)
                {
                    guns[i].GetComponent<GunScript>().isPause = false;
                }
            }
    }
}
