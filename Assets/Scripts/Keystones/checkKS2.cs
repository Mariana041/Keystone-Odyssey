using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkKS2 : MonoBehaviour
{

    [SerializeField] private GameObject keystone2;
    [SerializeField] private Material matGhost;
    [SerializeField] private Material matActual;
    
    void Update()
    {
        if (keystone2.GetComponent<keystone2>().myself)
        {
            gameObject.GetComponent<Renderer>().material = matActual;
        }
    }
}
