using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class selfOpen : MonoBehaviour
{
    private float delay = 2f;
    [SerializeField] private Transform oldposition;
    [SerializeField] private Transform newposition;
    
    private void Start()
    {
        oldposition = gameObject.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OpenDoor(this.gameObject);
            StartCoroutine(nameof(timer));
        }
    }

    private IEnumerator timer()
    {
        yield return new WaitForSeconds(delay);
        CloseDoor(this.gameObject);
    }

    public void CloseDoor(GameObject gameObject)
    {
        this.gameObject.transform.position = oldposition.transform.position;
    }

    public void OpenDoor(GameObject gameObject)
    {
        this.gameObject.transform.position = newposition.transform.position;
    }
    
}
