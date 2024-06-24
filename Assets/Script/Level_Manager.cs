using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Muhammed;

public class Level_Manager : MonoBehaviour
{
    public Button[] Butonlar;
    public int Level;
    public Sprite KilitliButon;

    BellekYonetim _BellekYonetim = new BellekYonetim();
    void Start()
    {

        int mevcutLevel = _BellekYonetim.VeriOku_int("SonLevel")-2;

        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (i +1 <= mevcutLevel)
            {
                Butonlar[i].GetComponentInChildren<Text>().text = (i+1).ToString();
                int Index = i + 1;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(Index); });
            }
            else
            {
                Butonlar[i].GetComponent<Image>().sprite = KilitliButon;
                Butonlar[i].enabled = false;
            }
        }

    }
    public void SahneYukle(int Index)
    {
        SceneManager.LoadScene(Index+2);
    }

    public void GeriDon()
    {
        SceneManager.LoadScene(0);
    }
}
