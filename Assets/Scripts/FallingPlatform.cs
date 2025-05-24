using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float delayBeforeFall = 1.5f;
    [SerializeField] private float respawnTime = 2f;

    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isRespawning = false;

    void Start()
    {
        originalPosition = transform.parent.position;
        originalRotation = transform.parent.rotation;
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    IEnumerator FallAndRespawn()
    {
        yield return new WaitForSeconds(delayBeforeFall);

        rb.isKinematic = false;
        rb.useGravity = true;

        yield return new WaitForSeconds(respawnTime);

        rb.isKinematic = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.parent.position = originalPosition;
        transform.parent.rotation = originalRotation;

        Debug.Log("Plateforme téléportée à sa position initiale !");
        isRespawning = false;
    }
}
