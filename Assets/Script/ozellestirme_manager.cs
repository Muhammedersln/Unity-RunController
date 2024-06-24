using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Muhammed;
using TMPro;
using UnityEngine.SceneManagement;

using Unity.VisualScripting;

public class ozellestirme_manager : MonoBehaviour
{

    public Text PuanText;

    public GameObject[] islemPanelleri;
    public GameObject islemCanvasi;
    public GameObject[] genelPaneller;
    public Button[] islemButonlari;
    public TextMeshProUGUI satinAlmaText;

    [Header("SAPKALAR")]
    public GameObject[] Sapkalar;
    public Text SapkaText;
    public Button[] SapkaBtn;

    [Header("SOPALAR")]
    public GameObject[] Sopalar;
    public Text SopaText;
    public Button[] SopaBtn;

    [Header("MATERÝAL")]
    public Material[] Materials;
    public Material VarsayilanMaterial;
    public Button[] MaterialBtn;
    public Text MaterialText;
    public SkinnedMeshRenderer _Renderer;

    int AktifislemPaneliIndex = -1;
    int SapkaIndex = -1;
    int SopaIndex = -1;
    int MaterialIndex = -1;

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetimi = new VeriYonetimi();
    [Header("GENEL VERÝLER")]

    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();



    void Start()
    {

        _BellekYonetim.VeriKaydet_int("Puan", 1500);
        PuanText.text = _BellekYonetim.VeriOku_int("Puan").ToString();
        _VeriYonetimi.Load();
        _ItemBilgileri = _VeriYonetimi.ListeyiAktar();
        DurumKontrolEt(0, true);
        DurumKontrolEt(1, true);
        DurumKontrolEt(2, true);

    }
    void DurumKontrolEt(int Bolum, bool islem = false)
    {
        if (Bolum == 0)
        {
            #region
            if (_BellekYonetim.VeriOku_int("AktifSapka") == -1)
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = false;
                satinAlmaText.text = "SATIN AL";

                if (!islem)
                {
                    SapkaIndex = -1;
                    SapkaText.text = "Sapka Yok!";
                }

            }
            else
            {
                foreach (var item in Sapkalar) { item.SetActive(false); }
                SapkaIndex = _BellekYonetim.VeriOku_int("AktifSapka");
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
            #endregion
        }
        else if (Bolum == 1)
        {
            #region
            if (_BellekYonetim.VeriOku_int("AktifSopa") == -1)
            {
                foreach (var item in Sopalar)
                {
                    item.SetActive(false);
                }
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = false;
                satinAlmaText.text = "SATIN AL";

                if (!islem)
                {
                    SopaIndex = -1;
                    SopaText.text = "Sopa Yok!";

                }

            }
            else
            {
                foreach (var item in Sopalar) { item.SetActive(false); }

                SopaIndex = _BellekYonetim.VeriOku_int("AktifSopa");
                Sopalar[SapkaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
            #endregion
        }
        else
        {
            if (_BellekYonetim.VeriOku_int("AktifTema") == -1)
            {
                if (!islem)
                {
                    MaterialIndex = -1;
                    MaterialText.text = "Tema Yok!";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = false;
                    satinAlmaText.text = "SATIN AL";

                }

                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = VarsayilanMaterial;
                    _Renderer.materials = mats;
                }

            }
            else
            {
                MaterialIndex = _BellekYonetim.VeriOku_int("AktifTema");
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;

                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].Item_Ad;
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;
                islemButonlari[1].interactable = true;
            }
        }
    }

    void SatimAlmaSonuc(int Index)
    {
        _ItemBilgileri[Index].SatinAlmaDurumu = true;
        _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_int("Puan") - _ItemBilgileri[Index].Puan);
        satinAlmaText.text = "SATIN AL";
        islemButonlari[0].interactable = false;
        islemButonlari[1].interactable = true;
        PuanText.text = _BellekYonetim.VeriOku_int("Puan").ToString();
    }
    public void SatinAl()
    {

        if (AktifislemPaneliIndex != -1)
        {
            switch (AktifislemPaneliIndex)
            {
                case 0:
                    SatimAlmaSonuc(SapkaIndex);

                    break;
                case 1:
                    int Index = SopaIndex + 3;
                    SatimAlmaSonuc(Index);
                    break;
                case 2:
                    int Index2 = MaterialIndex + 6;
                    SatimAlmaSonuc(Index2);
                    break;
            }
        }

    }
    public void Kaydet()
    {
        if (AktifislemPaneliIndex != -1)
        {
            switch (AktifislemPaneliIndex)
            {
                case 0:
                    _BellekYonetim.VeriKaydet_int("AktifSapka", SapkaIndex);

                    break;
                case 1:
                    _BellekYonetim.VeriKaydet_int("AktifSopa", SopaIndex);

                    break;
                case 2:
                    _BellekYonetim.VeriKaydet_int("AktifTema", MaterialIndex);
                    break;
            }
        }


    }
    public void SapkaYonBtn(string islem)
    {
        if (islem == "ileri")
        {

            if (SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;

                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;

                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;
                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;

                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            //------------------------------------------------------------------
            if (SapkaIndex == Sapkalar.Length - 1)
                SapkaBtn[1].interactable = false;
            else
                SapkaBtn[1].interactable = true;

            if (SapkaIndex != -1)
                SapkaBtn[0].interactable = true;

        }
        else
        {
            if (SapkaIndex != -1)
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex--;
                if (SapkaIndex != -1)
                {
                    Sapkalar[SapkaIndex].SetActive(true);
                    SapkaBtn[0].interactable = true;
                    SapkaText.text = _ItemBilgileri[SapkaIndex].Item_Ad;
                    if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                    {
                        satinAlmaText.text = _ItemBilgileri[SapkaIndex].Puan + " - SATIN AL";
                        islemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = "SATIN AL";
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }

                }
                else
                {
                    SapkaBtn[0].interactable = false;
                    SapkaText.text = "Sapka Yok!";
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;


                }
            }
            else
            {
                SapkaBtn[0].interactable = false;
                SapkaText.text = "Sapka Yok!";
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;

            }

            if (SapkaIndex != Sapkalar.Length - 1)
                SapkaBtn[1].interactable = true;



        }
    }
    public void SopaYonBtn(string islem)
    {
        if (islem == "ileri")
        {

            if (SopaIndex == -1)
            {
                SopaIndex = 0;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex++;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }

            }
            //------------------------------------------------------------------
            if (SopaIndex == Sapkalar.Length - 1)
                SopaBtn[1].interactable = false;
            else
                SopaBtn[1].interactable = true;

            if (SopaIndex != -1)
                SopaBtn[0].interactable = true;

        }
        else
        {
            if (SopaIndex != -1)
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex--;
                if (SopaIndex != -1)
                {
                    Sopalar[SopaIndex].SetActive(true);
                    SopaBtn[0].interactable = true;
                    SopaText.text = _ItemBilgileri[SopaIndex + 3].Item_Ad;

                    if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                    {
                        satinAlmaText.text = _ItemBilgileri[SopaIndex + 3].Puan + " - SATIN AL";
                        islemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = "SATIN AL";
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }

                }
                else
                {
                    SopaBtn[0].interactable = false;
                    SopaText.text = "Sopa Yok!";
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;

                }
            }
            else
            {
                SopaBtn[0].interactable = false;
                SopaText.text = "Sopa Yok!";
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;

            }

            if (SopaIndex != Sopalar.Length - 1)
                SopaBtn[1].interactable = true;



        }
    }
    public void TemaYonBtn(string islem)
    {
        if (islem == "ileri")
        {

            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].Item_Ad;
                if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[MaterialIndex + 6].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }
            }
            else
            {
                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].Item_Ad;
                if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                {
                    satinAlmaText.text = _ItemBilgileri[MaterialIndex + 6].Puan + " - SATIN AL";
                    islemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                        islemButonlari[0].interactable = false;
                    else
                        islemButonlari[0].interactable = true;
                }
                else
                {
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;
                    islemButonlari[1].interactable = true;
                }

            }
            //------------------------------------------------------------------
            if (MaterialIndex == Materials.Length - 1)
                MaterialBtn[1].interactable = false;
            else
                MaterialBtn[1].interactable = true;

            if (MaterialIndex != -1)
                MaterialBtn[0].interactable = true;

        }
        else
        {
            if (MaterialIndex != -1)
            {

                MaterialIndex--;
                if (MaterialIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materials[MaterialIndex];
                    _Renderer.materials = mats;
                    MaterialBtn[0].interactable = true;
                    MaterialText.text = _ItemBilgileri[MaterialIndex + 6].Item_Ad;
                    if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                    {
                        satinAlmaText.text = _ItemBilgileri[MaterialIndex + 6].Puan + " - SATIN AL";
                        islemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                            islemButonlari[0].interactable = false;
                        else
                            islemButonlari[0].interactable = true;
                    }
                    else
                    {
                        satinAlmaText.text = "SATIN AL";
                        islemButonlari[0].interactable = false;
                        islemButonlari[1].interactable = true;
                    }

                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = VarsayilanMaterial;
                    _Renderer.materials = mats;
                    MaterialBtn[0].interactable = false;
                    MaterialText.text = "Material Yok!";
                    satinAlmaText.text = "SATIN AL";
                    islemButonlari[0].interactable = false;

                }
            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = VarsayilanMaterial;
                _Renderer.materials = mats;
                MaterialBtn[0].interactable = false;
                MaterialText.text = "Material Yok!";
                satinAlmaText.text = "SATIN AL";
                islemButonlari[0].interactable = false;

            }

            if (MaterialIndex != Materials.Length - 1)
                MaterialBtn[1].interactable = true;



        }
    }
    public void islemPaneliCikart(int Index)
    {
        DurumKontrolEt(Index);
        genelPaneller[0].SetActive(true);
        AktifislemPaneliIndex = Index;
        islemPanelleri[Index].SetActive(true);
        genelPaneller[1].SetActive(true);
        islemCanvasi.SetActive(false);



    }
    public void GeriDon()
    {
        genelPaneller[0].SetActive(false);
        islemCanvasi.SetActive(true);
        genelPaneller[1].SetActive(false);
        islemPanelleri[AktifislemPaneliIndex].SetActive(false);
        DurumKontrolEt(AktifislemPaneliIndex, true);
        AktifislemPaneliIndex = -1;
    }
    public void AnaMenuyeDon()
    {
        _VeriYonetimi.Save(_ItemBilgileri);
        SceneManager.LoadScene(0);
    }
}
