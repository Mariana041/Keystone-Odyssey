using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkKS1 : MonoBehaviour
{

    [SerializeField] private GameObject keystone1;
    [SerializeField] private Material matGhost;
    [SerializeField] private Material matActual;
    
    void Update()
    {
        if (keystone1.GetComponent<keystone1>().myself)
        {
            gameObject.GetComponent<Renderer>().material = matActual;
        }
    }
}
