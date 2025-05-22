using UnityEngine;

public class TeleportOnBucheZone : MonoBehaviour
{
    [SerializeField]
    private Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buche"))
        {
            transform.position = teleportPosition;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}


