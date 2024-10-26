using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CheckToOpen : selfOpen
{
    public bool ks1open = false;
    public bool ks2open = false;
    public bool ks3open = false;
    
    private void Update()
    {
        if (ks1open && ks2open && ks3open)
        {
            OpenDoor(gameObject);          
        }
    }
}
