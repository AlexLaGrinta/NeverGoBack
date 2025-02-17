using UnityEngine;
using Photon.Pun;

public class PlayerAnimationSync : MonoBehaviour, IPunObservable
{
    private Animator anim;     // Référence à l'Animator
    private PhotonView photonView;

    // Variables locales pour stocker les données du réseau
    private float speed;
    private bool isRunning;
    private bool isJumping;
    private bool onFloor;
    private bool isFalling;

    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // Référence à l'Animator enfant
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            // Si ce n'est pas le joueur local, appliquer les valeurs synchronisées
            anim.SetFloat("Speed", speed);
            anim.SetBool("isRunning", isRunning);
            anim.SetBool("isJumping", isJumping);
            anim.SetBool("onFloor", onFloor);
            anim.SetBool("isFalling", isFalling);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // Le joueur local envoie des données
        {
            if (photonView.IsMine)
            {
                // Prendre les valeurs de l'Animator local et les envoyer
                stream.SendNext(anim.GetFloat("Speed"));
                stream.SendNext(anim.GetBool("isRunning"));
                stream.SendNext(anim.GetBool("isJumping"));
                stream.SendNext(anim.GetBool("onFloor"));
                stream.SendNext(anim.GetBool("isFalling"));
            }
        }
        else // Les autres joueurs reçoivent ces données
        {
            // Recevoir et stocker les valeurs des autres joueurs
            speed = (float)stream.ReceiveNext();
            isRunning = (bool)stream.ReceiveNext();
            isJumping = (bool)stream.ReceiveNext();
            onFloor = (bool)stream.ReceiveNext();
            isFalling = (bool)stream.ReceiveNext();
        }
    }
}
