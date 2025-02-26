using System.Collections;
using UnityEngine;

public class MovePiqueY : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f;  // Distance parcourue sur l'axe X
    [SerializeField] private float moveSpeed = 2f;     // Vitesse de déplacement
    [SerializeField] private float delay = 1f;         // Délai avant que la plateforme commence à bouger

    private Vector3 originalPosition;  // Position initiale de la plateforme
    private bool isMoving = false;     // Indique si la plateforme est en train de bouger
    private bool isReturning = false;  // Indique si la plateforme est en train de revenir
    private bool isTriggered = false; // Empêche plusieurs déclenchements

    void Start()
    {
        // Stocke la position initiale de la plateforme
        originalPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si le joueur entre dans le trigger et que la plateforme n'est pas déjà en mouvement
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(PlatformMovementRoutine());
        }
    }

    IEnumerator PlatformMovementRoutine()
    {
        yield return new WaitForSeconds(delay);  // Attend le délai défini

        // Déplace la plateforme vers sa nouvelle position
        isMoving = true;
        Vector3 targetPosition = originalPosition + new Vector3(0f, moveDistance, 0f);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Assure que la plateforme est exactement sur la position cible
        transform.position = targetPosition;

        // Marque le début du retour
        isMoving = false;
        isReturning = true;

        yield return new WaitForSeconds(3f);  // Optionnel : délai avant le retour

        // Assure que la plateforme est exactement sur sa position d'origine
        transform.position = originalPosition;
        isReturning = false;

        // Réinitialise le déclencheur
        isTriggered = false;
    }
}