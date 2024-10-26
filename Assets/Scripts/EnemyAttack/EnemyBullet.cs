using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject); -- not 1 hit anymore
            other.gameObject.GetComponent<PlayerController>().TakeDamage(other.gameObject.GetComponent<PlayerController>().damage);  // takes as much damage as a normal tick
            Destroy(gameObject);
        }

        Destroy(gameObject, 1.2f);
    }
}
