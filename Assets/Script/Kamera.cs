using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;
    public bool SonaGeldikMi;
    public GameObject Gidecekyer;
    void Start()
    {
        target_offset = transform.position - target.position;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (!SonaGeldikMi)
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
        else
            transform.position = Vector3.Lerp(transform.position, Gidecekyer.transform.position, .010f);
    }
}
