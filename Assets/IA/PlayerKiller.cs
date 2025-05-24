using UnityEngine;
using Photon.Pun;

public class PlayerKiller : MonoBehaviour
{

    [SerializeField] private AudioSource audioKiler;


    private void OnTriggerEnter(Collider other)
    {

        PhotonView playerPhotonView = other.GetComponent<PhotonView>();

        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            SpectatorMode specComp = playerPhotonView.gameObject.GetComponent<SpectatorMode>();
            if (specComp != null)
            {
                audioKiler.Stop();
                specComp.MazeFinal(other);
            }
        }
    }
}
