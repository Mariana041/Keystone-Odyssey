using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{

    #region Variables

    [SerializeField]private float coolDown = 3f;
    public float damage, lenght;
    public GameObject bulletPrefab1;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public Transform firePoint;

    public Animator animator;
    public Animation[] animations;

    [Range(1, 25)]
    public float bulletForce = 10f;
    
    [Range(1, 15)]
    [SerializeField] private float speed;
    private Vector2 move, mouseLook, joystickLook;
    private Vector3 rotationTarget;

    public bool pc; //boolean to later chose between controller and mouse on settings menu

    private enum state { idle, walking, running, shooting, dead }; // for later use on animations

    
    public float health = 50;
    public bool canMelee;
    public bool canShoot1;
    public bool canShoot2;
    public bool canShoot3;
    
    public bool keystone1 = false;
    public bool keystone2 = false;
    public bool keystone3 = false;

    [SerializeField] private VisualEffect n1, p1, p2, p3;
    #endregion


    #region Movement and Aim
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        animator.Play("RunForwardBattle");
        
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        joystickLook = context.ReadValue<Vector2>();
    }
    #endregion

    
    #region Attacks
    public void OnFire(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if (canMelee) Melee();
        };
    }
    
    public void OnOne(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if(keystone1 && canShoot1) Keystone1();
        };
    }
    
    public void OnTwo(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if(keystone2 && canShoot2) Keystone2();
        };
    }
    
    public void OnThree(InputAction.CallbackContext context)
    { // call Shoot method when X or LMB is pressed
        context.action.performed += ctx =>
        {
            if(keystone3 && canShoot3) Keystone3();
        };
    }
    #endregion

    void Start()
    {
        pc = true;
        canMelee = true;
        canShoot1 = true;
        canShoot2 = true;
        canShoot3 = true;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward*lenght, Color.green);
        if (pc)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            mouseMovement();
        }
        else
        {
            if (joystickLook.x == 0 && joystickLook.y == 0)
            {
                movement();
            }
            else
            {
                mouseMovement();
            }
        }
    }

    #region Methods

    #region cooldowns
    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.3f);
        canMelee = true;
    }
    
    IEnumerator cooldown1()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot1 = true;
    }
    
    IEnumerator cooldown2()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot2 = true;
    }
    
    IEnumerator cooldown3()
    {
        yield return new WaitForSeconds(coolDown);
        canShoot3 = true;
    }
    #endregion
    
    
    #region Movement
    public void movement()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        /*if statement to fix the player looking up after release of the controls*/
        if (movement != Vector3.zero) transform.Translate(movement * speed * Time.deltaTime, Space.World);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    }

    public void mouseMovement()
    {
        if (pc)
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDirection = new Vector3(joystickLook.x, 0f, joystickLook.y);
            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDirection), 0.15f);
            }
        }
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * (speed * Time.deltaTime), Space.World);
    }
    
    #endregion
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            ReloadScene();
        }
    }

    public void TakeDamage(float f)
    {
        health -= f;
        if (health <= 0)
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01");
    }
    

    #endregion

    
    #region ApplyDamage

    public void Melee()
    {
        //Debug.DrawRay(transform.position, transform.forward*lenght, Color.green);
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, transform.forward, out hit2, lenght))
        {
            if (hit2.collider.gameObject.CompareTag("inimigo"))
            {
                hit2.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                //Debug.Log("Hit" + hit2.collider.gameObject.name);
                canMelee = false;
                StartCoroutine("cooldown");
                if (n1 != null) n1.Play();
            }
        }
        //animator.Play("Attack01");
    }

    public void Keystone1()
    {
        GameObject bullet1 = Instantiate(bulletPrefab1, firePoint.position, gameObject.transform.rotation);
        Rigidbody rb = bullet1.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        canShoot1 = false;
        StartCoroutine("cooldown1");
        //animator.Play("Attack02");
    }

    public void Keystone2()
    {
        GameObject bullet2 = Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet2.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        canShoot2 = false;
        StartCoroutine("cooldown2");
        if (n1 != null) p2.Play();
    }

    public void Keystone3()
    {
        GameObject bullet3 = Instantiate(bulletPrefab3, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet3.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        canShoot3 = false;
        StartCoroutine("cooldown3");
        if (n1 != null) p3.Play();
    }

    #endregion


}