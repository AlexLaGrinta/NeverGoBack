using UnityEngine;
using Photon.Pun;

public class Ejection : MonoBehaviourPun
{
    private float ejectForce = 15f; // Force d'éjection
    private float ejectDuration = 1f; // Durée pendant laquelle le joueur est éjecté
    private CharacterController controller;
    private Vector3 currentEjectDirection = Vector3.zero; // Direction en cours d'éjection
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
            // Continue à appliquer le mouvement selon la direction et le temps restant
            controller.Move(currentEjectDirection * ejectForce * Time.deltaTime);

            // Réduit le temps de l'éjection
            ejectTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return; // Ne traite que l'objet local

        // Si l'objet touché a le tag "EjectPlatform"
        if (other.CompareTag("EjectPlatform"))
        {
            // Calcule la direction d'éjection
            Vector3 ejectDirection = (transform.position - other.transform.position).normalized;

            // Applique l'éjection
            StartEjection(ejectDirection);

            // Synchronise avec les autres joueurs
            photonView.RPC("SyncEjection", RpcTarget.Others, ejectDirection);
        }
    }

    private void StartEjection(Vector3 direction)
    {
        // Configure la direction et démarre le chronométrage pour l'éjection
        currentEjectDirection = direction;
        ejectTimer = ejectDuration;

        Debug.Log($"Joueur éjecté ! Direction : {direction}, Force : {ejectForce}, Durée : {ejectDuration}s.");
    }

    [PunRPC]
    private void SyncEjection(Vector3 direction)
    {
        if (photonView.IsMine)
        {
            StartEjection(direction);
            Debug.Log("(Synchronisation réseau) Éjection appliquée !");
        }
    }
}
