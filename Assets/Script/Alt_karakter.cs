using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_karakter : MonoBehaviour
{
    public GameObject Target;
    NavMeshAgent _Navmesh;
    public GameManager _GameManager;
    
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
        

    }


    // Update is called once per frame
    private void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("igneliKutu"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Testere"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Pervaneigne"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Dusman"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform,false);
            gameObject.SetActive(false);
        }
    }
}
