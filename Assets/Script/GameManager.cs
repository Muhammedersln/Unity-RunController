using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muhammed;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{



    public static int AnlikKarakterSayisi = 1;
    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;

    [Header("Level Verileri")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject _AnaKarakter;
    public bool OyunBittiMi;
    bool SonaGeldikMi;
    [Header("SAPKALAR")]
    public GameObject[] Sapkalar;
    [Header("SOPALAR")]
    public GameObject[] Sopalar;
    [Header("MATERÝAL")]
    public Material[] Materials;
    public SkinnedMeshRenderer _Renderer;
    public Material VarsayilanMaterial;
    public GameObject[] islemPanelleri;



    Matematiksel_islemler _Matematiksel_islemler = new Matematiksel_islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();

    Scene _Scene;

    private void Awake()
    {
        ItemleriKontrolEt();
    }
    void Start()
    {
        DusmanlariOlustur();
        _Scene = SceneManager.GetActiveScene();

    }
    public void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void DusmanlariTetikle()
    {
        foreach (var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikMi = true;
        SavasDurumu();

    }
    void Update()
    {
    }

    void SavasDurumu()
    {

        if (SonaGeldikMi)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittiMi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                _AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);
                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    islemPanelleri[0].SetActive(true);
                }
                else
                {
                    if (AnlikKarakterSayisi > 5)
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_int("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_int("Puan") + 600);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_int("SonLevel") + 1);
                        }

                    }
                    else
                    {
                        if (_Scene.buildIndex == _BellekYonetim.VeriOku_int("SonLevel"))
                        {
                            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_int("Puan") + 200);
                            _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_int("SonLevel") + 1);
                        }

                    }

                    islemPanelleri[1].SetActive(true);
                }
            }
        }


    }
    public void KarakterYonetim(string islemturu, int GelenSayi, Transform Pozisyon)
    {
        switch (islemturu)
        {
            case "Carpma":
                _Matematiksel_islemler.Carpma(GelenSayi, Karakterler, Pozisyon);
                break;

            case "Toplama":
                _Matematiksel_islemler.Toplama(GelenSayi, Karakterler, Pozisyon);
                break;
            case "Cýkarma":
                _Matematiksel_islemler.Cýkarma(GelenSayi, Karakterler);
                break;
            case "Bolme":
                _Matematiksel_islemler.Bolme(GelenSayi, Karakterler);
                break;


        }
    }

    public void YokOlmaEfektiOlustur(Transform Pozisyon, bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon.position;
                item.GetComponent<ParticleSystem>().Play();

                if (!Durum)
                    AnlikKarakterSayisi--;
                else
                    KacDusmanOlsun--;
                break;

            }
        }
        if (!OyunBittiMi)
            SavasDurumu();
    }

    public void ItemleriKontrolEt()
    {
        if (_BellekYonetim.VeriOku_int("AktifSapka") != -1)
            Sapkalar[_BellekYonetim.VeriOku_int("AktifSapka")].SetActive(true);
        if (_BellekYonetim.VeriOku_int("AktifSopa") != -1)
            Sopalar[_BellekYonetim.VeriOku_int("AktifSopa")].SetActive(true);

        if (_BellekYonetim.VeriOku_int("AktifTema") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materials[_BellekYonetim.VeriOku_int("AktifTema")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = VarsayilanMaterial;
            _Renderer.materials = mats;
        }
    }

    public void Cisik()
    {
        SceneManager.LoadScene(0);
    }
    public void TekrarOyna()
    {
        SceneManager.LoadScene(_Scene.buildIndex);
    }
    public void sonrakiLevel()
    {
        SceneManager.LoadScene(_Scene.buildIndex + 1);
    }
}
