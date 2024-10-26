using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EnemyHealth : MonoBehaviour
{
    //enemy health system
    public float health;
    public Animator animator;
    public AudioClip hit;
    public AudioClip die;
    public AudioSource audiosource;
    
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    //enemy death
    [SerializeField]private GameObject deathEffect; //colocar no inspetor o vfx de morte do inimigo
    public void TakeDamage(float damage)
    {
        health -= damage;
        PlaySound();
        Anim();
        if (health <= 0)
        {
            StartCoroutine(nameof(Die));
        }
    }
    IEnumerator Die()
    {
        Anim();
        PlaySound();
        yield return new WaitForSeconds(1.4f);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Anim()
    {
        if(health <= 0) animator.Play("die");
        if(health > 0) animator.Play("gethit");
    }

    void PlaySound()
    {
        if (health <= 0) audiosource.PlayOneShot(die);

        if (health > 0) audiosource.PlayOneShot(hit);
    }
}
