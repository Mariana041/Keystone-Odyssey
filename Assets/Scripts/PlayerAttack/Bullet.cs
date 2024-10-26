 using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            //Destroy(collision.gameObject); -- not 1 hit anymore
            collision.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(FindObjectOfType<PlayerController>().damage);  // takes as much damage as a normal tick
            Destroy(gameObject);
        }

        Destroy(gameObject, 2f);
    }
}
