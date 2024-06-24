using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Muhammed;

public class AnaMenu_Manager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    void Start()
    {
        _BellekYonetim.KontrolEtveTanýmla();
        _VeriYonetimi.ilkDosyaOlusturma(_ItemBilgileri);
    }
    
    public void SahneYukle(int Index)
    {
        SceneManager.LoadScene(Index);
    }
    public void Oyna()
    {
        SceneManager.LoadScene(_BellekYonetim.VeriOku_int("SonLevel"));
    }
    public void Cikis()
    {
        Application.Quit();
    }

}
