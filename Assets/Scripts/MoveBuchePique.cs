using UnityEngine;

public class MoveBuchePick : MonoBehaviour
{
    private float distanceToMove = 15f;
    private float moveSpeed = 10f;
    private float radius = 0.5f; // Rayon de l'objet pour la rotation réaliste (exemple : une roue)

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool movingForward = false;
    private bool shouldMove = false;

    void Start()
    {
        startPosition = transform.position; // Position initiale
    }

    void Update()
    {
        if (!shouldMove)
        {
            targetPosition = startPosition + new Vector3(-distanceToMove, 0f, 0f); // Déplacement sur l'axe X
            movingForward = true;
            shouldMove = true;
        }

        if (shouldMove)
        {
            Vector3 previousPosition = transform.position; // Sauvegarde la position précédente

            if (movingForward)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Si l'objet atteint la cible, il commence à revenir
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    movingForward = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

                // Si l'objet revient à la position de départ, le mouvement s'arrête
                if (Vector3.Distance(transform.position, startPosition) < 0.01f)
                {
                    shouldMove = false;
                }
            }

            // Calcul de la distance parcourue
            Vector3 movement = transform.position - previousPosition;
            float distanceMoved = movement.magnitude;

            // Calcul de l'angle de rotation (basé sur le rayon et la distance parcourue)
            float rotationAngle = (distanceMoved / (2 * Mathf.PI * radius)) * 360f;

            // Appliquer la rotation sur l'axe Z (comme un roulement sur l'axe X)
            transform.Rotate(new Vector3(0f, -rotationAngle, 0f));
        }
    }
}
