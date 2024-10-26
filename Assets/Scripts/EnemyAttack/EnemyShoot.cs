using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]private GameObject bulletPrefab;
    private float bulletForce = 5f;
    void Start()
    {
        StartCoroutine("Shoot");
    }

    void Shoot()
    {
        GameObject EnemyBullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody rb = EnemyBullet.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * bulletForce, ForceMode.Impulse);
        StartCoroutine(nameof(Timer));
    }
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f); 
        Shoot();
    }
}
