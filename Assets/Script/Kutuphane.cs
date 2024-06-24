using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Muhammed
{
    public class Matematiksel_islemler
    {
        public  void Carpma(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon)
        {
            int DonguSayisi = (GameManager.AnlikKarakterSayisi * GelenSayi) - GameManager.AnlikKarakterSayisi;
            int sayi = 0;
            foreach (var item in Karakterler)
            {
                if (sayi < DonguSayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi *= GelenSayi;
        }
        public  void Toplama(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon)
        {
            int sayi2 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi2 < GelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += GelenSayi;
        }
        public  void Cýkarma(int GelenSayi, List<GameObject> Karakterler)
        {
            if (GameManager.AnlikKarakterSayisi < GelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);


                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != GelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= GelenSayi;
            }
        }
        public  void Bolme(int GelenSayi, List<GameObject> Karakterler)
        {

            if (GameManager.AnlikKarakterSayisi <= 2)
            {
                foreach (var item in Karakterler)
                {
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);


                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int bolen = GameManager.AnlikKarakterSayisi / GelenSayi;
                int sayi4 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi4 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0;
                        break;
                    }
                }
                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi+=2;

                }

            }
        }
    }

    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();

        }
        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_string (string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_int(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float VeriOku_float(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void KontrolEtveTanýmla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 3);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
            }
        }
    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndex;
        public int Item_Index;
        public string Item_Ad;
        public int Puan;
        public bool SatinAlmaDurumu;

    }

    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();
        }
        public void ilkDosyaOlusturma(List<ItemBilgileri> _ItemBilgileri)
        {
            if(!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }

        }
        List<ItemBilgileri> _ItemicListe;
        public void Load()
        {

            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemicListe = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<ItemBilgileri> ListeyiAktar()
        {
            return _ItemicListe;
        }
    }

}

