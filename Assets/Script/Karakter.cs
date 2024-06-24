using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public Kamera _Kamera;
    public bool SonaGeldikMi;
    public GameObject Gidecekyer;
    public Slider _Slider;
    public GameObject GecisNoktasi;
    private void FixedUpdate()
    {
        if (!SonaGeldikMi)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }
    private void Start()
    {
        float Fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        _Slider.maxValue = Fark;
    }

    void Update()
    {


        if (SonaGeldikMi)
        {
            transform.position = Vector3.Lerp(transform.position, Gidecekyer.transform.position, .015f);
            if(_Slider.value!=0)
                _Slider.value -= .01f;
        }
        else
        {
            float Fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
            _Slider.value = Fark;

            if (Input.GetKey(KeyCode.Mouse0)) {
            
            if(Input.GetAxis("Mouse X") < 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
            }
        }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cýkarma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _GameManager.KarakterYonetim(other.tag,sayi,other.transform);
        }
        else if (other.CompareTag("SonTetikleyici"))
        {
            _Kamera.SonaGeldikMi = true;
            _GameManager.DusmanlariTetikle();
            SonaGeldikMi = true;
        }
        else if (other.CompareTag("BosKarakter"))
        {
            _GameManager.Karakterler.Add(other.gameObject);
            other.gameObject.tag = "AltKarakter";
            GameManager.AnlikKarakterSayisi++;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk") || collision.gameObject.CompareTag("igneliKutu") || collision.gameObject.CompareTag("Pervaneigne"))
        {
            if (transform.position.x>0)
                transform.position = new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z);   
            else
                transform.position = new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z);
        }
    }
}
