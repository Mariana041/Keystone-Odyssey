using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraTriggerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    Rigidbody _rb;
    [SerializeField] Rigidbody _player;
    [SerializeField] GameObject SalaA;
    [SerializeField] GameObject SalaB;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        _rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == _player)
        {
            //Debug.Log("im detecting the player!");
            if (CameraSwitcher.ActiveCamera != cam)
            {
                //! SalaA.GetComponent<Renderer>().enabled = false;
                CameraSwitcher.SwitchCamera(cam);
            }
        }
    }
}
