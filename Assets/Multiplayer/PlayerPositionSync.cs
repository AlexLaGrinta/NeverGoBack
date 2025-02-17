using UnityEngine;
using Photon.Pun;

public class PlayerPositionSync : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    private Vector3 networkedPosition; // Position reçue du réseau
    private Vector3 lastPositionSent; // Dernière position réellement envoyée
    private Vector3 velocity; // Vélocité calculée
    private Vector3 lastVelocitySent; // Dernière vélocité envoyée

    private float smoothingSpeed = 10f; // Vitesse pour lisser le mouvement
    private float positionThreshold = 0.05f; // Ne pas ajuster si l'erreur est inférieure à cette tolérance
    private float velocityThreshold = 0.1f; // Tolérance pour ignorer les petites fluctuations de la vélocité

    private bool isMoving; // Indique si le joueur est en mouvement

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        networkedPosition = transform.position;
        lastPositionSent = transform.position;
        velocity = Vector3.zero;
    }

    private void Update()
    {
        if (!photonView.IsMine) // Mouvements lissés pour les autres joueurs (clients distants)
        {
            SmoothNetworkMovement();
        }
        else
        {
            // Mise à jour du statut de mouvement
            isMoving = Vector3.Distance(transform.position, lastPositionSent) > positionThreshold;
        }
    }

    private void SmoothNetworkMovement()
    {
        // Ne faire un lissage que si la distance est significative
        if (Vector3.Distance(transform.position, networkedPosition) > positionThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, networkedPosition, Time.deltaTime * smoothingSpeed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // Si c'est le joueur local (envoi côté réseau)
        {
            // Calcul de la vélocité
            velocity = (transform.position - lastPositionSent) / Time.deltaTime;

            // Vérifier si une mise à jour réseau est nécessaire
            if (isMoving || Vector3.Distance(lastVelocitySent, velocity) > velocityThreshold)
            {
                // Envoie de données uniquement si le joueur bouge ou si la vélocité a changé significativement
                stream.SendNext(transform.position);
                stream.SendNext(velocity);

                // Mise à jour des derniers paramètres envoyés
                lastPositionSent = transform.position;
                lastVelocitySent = velocity;
            }
            else
            {
                // Si immobile, envoyer un signal de position fixe (sans répétition inutile)
                stream.SendNext(lastPositionSent);
                stream.SendNext(Vector3.zero);
            }
        }
        else // Réception des données réseau
        {
            // Mise à jour de la position et vélocité reçues
            networkedPosition = (Vector3)stream.ReceiveNext();
            velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
