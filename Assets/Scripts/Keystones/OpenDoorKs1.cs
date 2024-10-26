using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorKs1 : selfOpen
{
    [SerializeField] private GameObject keystone1;

    private void Update()
    {
        if (keystone1.GetComponent<keystone1>().myself)
        {
            OpenDoor(gameObject);
        }
    } 
}
