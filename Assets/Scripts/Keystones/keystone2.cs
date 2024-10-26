using UnityEngine;

public class keystone2 : MonoBehaviour
{
    private GameObject porta;
    private Transform newposition;
    [SerializeField] private Material matGhost;
    [SerializeField] private Material matActual;
    public bool myself = false;
    [SerializeField] private GameObject FinalDoor; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().keystone2 = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    
    private void Update()
    {

        if (myself == false) FinalDoor.GetComponent<CheckToOpen>().ks2open = false; 
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().keystone2)
        {
            myself = true;
            FindObjectOfType<CheckToOpen>().ks2open = true;
            gameObject.GetComponent<Renderer>().material = matActual;
            collision.gameObject.GetComponent<PlayerController>().keystone2 = false;
        }
    }
}
