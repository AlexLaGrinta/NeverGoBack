using UnityEngine;
using Photon.Pun;

public class EjectionY : MonoBehaviourPun
{
    [SerializeField] private float ejectForce = 15f; // Force d'éjection vers le haut
    [SerializeField] private float ejectDuration = 2f; // Durée pendant laquelle le joueur est éjecté
    private CharacterController controller;
    private Vector3 currentEjectVelocity = Vector3.zero; // Vélocité en cours d'éjection
    private float ejectTimer = 0f; // Chronométrage de l'éjection active

    private void Start()
    {
        // Obtenez le `CharacterController` du joueur
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError($"{gameObject.name} : Aucun CharacterController trouvé. Ajoutez un CharacterController au joueur !");
        }
    }

    private void Update()
    {
        // Si le joueur est en cours d'éjection
        if (ejectTimer > 0)
        {
            // Continue à appliquer la force d'éjection uniquement sur l'axe Y
            controller.Move(currentEjectVelocity * Time.deltaTime);

            // Réduit la durée d'éjection
            ejectTimer -= Time.deltaTime;

            // Décroissance de la vitesse (simulation gravité légère) - tu peux commenter si non nécessaire
            if (controller.isGrounded) 
            {
                currentEjectVelocity = Vector3.zero;
            }
        }
    }
}