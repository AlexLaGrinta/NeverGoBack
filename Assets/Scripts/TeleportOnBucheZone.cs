using UnityEngine;

public class TeleportOnBucheOrStatic : MonoBehaviour
{
    [SerializeField]
    private Vector3 teleportPosition;

    [SerializeField]
    private float staticTeleportDelay = 3f; // Temps de statique pour téléportation

    [SerializeField]
    private float velocityThreshold = 0.01f; // Sensibilité pour considérer l'objet comme statique

    private float staticTimer = 0f;
    private bool hasTeleported = false;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (rb != null && rb.linearVelocity.magnitude < velocityThreshold && rb.angularVelocity.magnitude < velocityThreshold)
        {
            staticTimer += Time.deltaTime;
            if (staticTimer >= staticTeleportDelay)
            {
                Teleport();
            }
        }
        else
        {
            staticTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Buche"))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        transform.position = teleportPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

