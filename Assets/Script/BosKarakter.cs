using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _SkinnedMeshRenderer;
    public Material atanacakMaterial;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    bool TemasVar;
    public GameManager _GameManager;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (TemasVar)
        {
            _Navmesh.SetDestination(Target.transform.position);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AltKarakter") || other.CompareTag("Player"))
        {
            MaterialDegistirAnimasyonTetikle();
            TemasVar = true;
        }

        else if (other.CompareTag("igneliKutu"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Testere"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Pervaneigne"))
        {

            _GameManager.YokOlmaEfektiOlustur(transform);
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfektiOlustur(transform, false);
            gameObject.SetActive(false);
        }
    }

    void MaterialDegistirAnimasyonTetikle()
    {
        Material[] mats = _SkinnedMeshRenderer.materials;
        mats[0] = atanacakMaterial;
        _SkinnedMeshRenderer.materials = mats;
        _Animator.SetBool("Saldir", true);
        
    }

}
