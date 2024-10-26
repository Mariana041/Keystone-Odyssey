using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class keystone1 : MonoBehaviour
{
    private GameObject porta;
    private Transform newposition;
    [SerializeField] private Material matGhost;
    [SerializeField] private Material matActual;
    public bool myself = false;

    [SerializeField] private GameObject FinalDoor; 

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {    
            if (!myself)
            {
                other.gameObject.GetComponent<PlayerController>().keystone1 = true;
                gameObject.GetComponent<MeshRenderer>().enabled = false;   
            }
        }
    }

    private void Update()
    {
        if (myself == false) FinalDoor.GetComponent<CheckToOpen>().ks1open = false; 
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().keystone1)
        {
            myself = true;
            FindObjectOfType<CheckToOpen>().ks1open = true;
            gameObject.GetComponent<Renderer>().material = matActual;
            collision.gameObject.GetComponent<PlayerController>().keystone1 = false;
        }
    }
}
