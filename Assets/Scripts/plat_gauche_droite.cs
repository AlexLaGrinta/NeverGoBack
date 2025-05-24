using UnityEngine;

public class Plat_g_d : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;       // Vitesse de déplacement
    [SerializeField] private float moveDistance = 3f;    // Distance de déplacement sur l'axe Z

    private Vector3 startPosition;
    private bool movingForward = true;
    private Transform playerTransform; // Stocke le joueur s'il est sur la plateforme
    private Vector3 lastPosition;      // Stocke la dernière position de la plateforme

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position; // Initialise la position précédente
    }

    void Update()
    {
        // Calcul du déplacement
        float moveDirection = movingForward ? 1 : -1;
        transform.position += Vector3.forward * moveDirection * moveSpeed * Time.deltaTime;

        // Si la plateforme atteint sa distance max, elle inverse la direction
        if (Vector3.Distance(transform.position, startPosition) >= moveDistance)
        {
            movingForward = !movingForward;
        }

        // ?? Déplace le joueur avec la plateforme (s'il est dessus)
        if (playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPosition;
            playerTransform.position += platformMovement;
        }

        // Met à jour la dernière position de la plateforme
        lastPosition = transform.position;
    }

    // ? Détecte quand le joueur monte sur la plateforme
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
        }
    }

    // ? Détecte quand le joueur descend
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
}