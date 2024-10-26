using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkChest : MonoBehaviour
{
    [SerializeField]private Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(nameof(GotoMenu));
        }
    }

    IEnumerator GotoMenu()
    {
        animator.Play("ChestOpen");
        yield return new WaitForSeconds(2f);
        animator.Play("open");
        yield return new WaitForSeconds(1.3f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
