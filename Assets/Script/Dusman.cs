using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Dusman : MonoBehaviour
{
    public GameObject Saldiri_Hedefi;
    NavMeshAgent _NavMesh;
    bool Saldiri_Basladimi;
    void Start()
    {
        _NavMesh = GetComponent<NavMeshAgent>();
    }
    public void AnimasyonTetikle()
    {
        GetComponent<Animator>().SetBool("Saldir", true);
        Saldiri_Basladimi = true;
    }
    
    void LateUpdate()
    {
        if(Saldiri_Basladimi)
        _NavMesh.SetDestination(Saldiri_Hedefi.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakter") || other.CompareTag("BosKarakter"))
        {

            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(transform,true);
            gameObject.SetActive(false);
        }
    }
}
