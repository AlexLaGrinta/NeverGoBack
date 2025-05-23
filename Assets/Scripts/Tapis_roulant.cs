using UnityEngine;

public class MovingFloorTrap : MonoBehaviour
{
    public float moveSpeed = 1.0f;         // Vitesse du déplacement
    public Vector3 moveDirection = Vector3.back; // Direction (par défaut: recule en Z)
    private bool shouldMove = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldMove = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldMove = false;
        }
    }

    void Update()
    {
        if (shouldMove)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
    }
}
