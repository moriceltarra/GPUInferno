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
    public float downSize = 0.0001f;
    public Pruebas pruebas;
    public GameObject panel;
    public GameObject[] ButtonLevels;
    private int coinCount = 0; // Contador de monedas recogidas


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
       coinCount++; // Aumenta el contador de monedas recogidas
    
        int requiredCoins = (barLevel / 3) + 1; // Cada 3 niveles, aumentamos la cantidad requerida

        if (coinCount >= requiredCoins) 
        {
            coinCount = 0; // Reinicia el contador
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
        for (int i = 0; i < ButtonLevels.Length; i++)
        {
            ButtonLevels[i].SetActive(false);
        }
        ButtonLevels[Random.Range(0, ButtonLevels.Length)].SetActive(true);
        pruebas.changeTime();
        Time.timeScale = 0f;
        for (int i = 0; i < 4; i++)
        {

            if (guns[i].activeInHierarchy)
            {
                guns[i].GetComponent<GunScript>().isPause = true;
            }
        }
        
    }
    public void DownSize()
    {
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
        pruebas.changeTime();
    }
    public void UpCD()
    {
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
        pruebas.changeTime();
    }
    public void UpFPS()
    {
        pruebas.CPUdelay(-3000);
        pruebas.GPUdelay(-3000);
        panel.SetActive(false);
        Time.timeScale = 1f;
        for (int i = 0; i < 4; i++)
        {

            if (guns[i].activeInHierarchy)
            {
                guns[i].GetComponent<GunScript>().isPause = false;
            }
        }
        pruebas.changeTime();
    }


}
