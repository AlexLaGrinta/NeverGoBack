using UnityEngine;

public class MoveAllerRetour : MonoBehaviour
{
    // Distance à parcourir (30 mètres)
    public float distanceToMove = 30f;

    // Vitesse du mouvement
    public float moveSpeed = 10f;

    // Position de départ et position cible
    private Vector3 startPosition;
    private Vector3 targetPosition;

    // État du mouvement (vers l'avant ou vers l'arrière)
    private bool movingForward = false;
    private bool shouldMove = false;

    void Start()
    {
        // Enregistrer la position de départ
        startPosition = transform.position;
    }

    void Update()
    {
        // Activer le mouvement lorsque l'utilisateur appuie sur la touche "Espace"
        if (Input.GetKeyDown(KeyCode.Space) && !shouldMove)
        {
            targetPosition = startPosition + transform.forward * distanceToMove;
            movingForward = true;  // Commence par bouger vers l'avant
            shouldMove = true;
        }

        // Si le mouvement est activé
        if (shouldMove)
        {
            // Déplacer l'objet
            if (movingForward)
            {
                // Vers la position cible
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Si la cible est atteinte, on change de direction (vers l'arrière)
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    movingForward = false;
                }
            }
            else
            {
                // Retourner vers la position de départ
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

                // Si la position de départ est atteinte, arrêter le mouvement
                if (Vector3.Distance(transform.position, startPosition) < 0.01f)
                {
                    shouldMove = false;
                }
            }
        }
    }
}
