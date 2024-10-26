using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keystone3 : MonoBehaviour
{
    public GameObject porta;
    public Transform newposition;
    [SerializeField] private Material matGhost;
    [SerializeField] private Material matActual;
    public bool myself = false;
    [SerializeField] private GameObject FinalDoor; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().keystone3 = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            porta.gameObject.transform.position = newposition.position;
        }
    }
    private void Update()
    {

        if (myself == false) FinalDoor.GetComponent<CheckToOpen>().ks3open = false; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().keystone3)
        {
            myself = true;
            FindObjectOfType<CheckToOpen>().ks3open = true;
            gameObject.GetComponent<Renderer>().material = matActual;
            collision.gameObject.GetComponent<PlayerController>().keystone3 = false;
        }
    }
}
