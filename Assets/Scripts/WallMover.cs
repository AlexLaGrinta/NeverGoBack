using UnityEngine;
using System.Collections;

public class WallMover : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.forward; // Direction du mouvement
    [SerializeField] private float speed = 2f;                    // Vitesse du déplacement
    [SerializeField] private float lifeTime = 5f;                 // Temps avant disparition du mur

    void Start()
    {
        // Lance la destruction du mur après lifeTime secondes
        StartCoroutine(DestroyAfterDelay());
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}