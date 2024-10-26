using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorKs2 : selfOpen
{
    [SerializeField] private GameObject keystone2;
    private void Update()
    {
        if (keystone2.GetComponent<keystone2>().myself)
        {
            OpenDoor(gameObject);
        }
    } 
}
