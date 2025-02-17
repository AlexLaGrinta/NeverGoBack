using UnityEngine;
using Photon.Pun;

public class PlayerRotationSync : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    private Quaternion networkRotation; // La rotation reçue depuis le réseau
    public Transform transformToSync; // Transform (généralement "joueur") à synchroniser
    public float smoothingSpeed = 10f; // Vitesse de synchronisation pour lisser les mouvements

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (transformToSync == null)
        {
            Debug.LogError("Veuillez assigner un transform à synchroniser dans PlayerRotationSync.");
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            // Interpolation pour rendre la rotation fluide (pas de saut brusque)
            transformToSync.rotation = Quaternion.Lerp(transformToSync.rotation, networkRotation, Time.deltaTime * smoothingSpeed);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // Si c'est le joueur local, envoyer la rotation
        {
            stream.SendNext(transformToSync.rotation);
        }
        else // Si c'est un autre joueur, recevoir la rotation
        {
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
