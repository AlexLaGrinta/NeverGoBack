using UnityEngine;
using Photon.Pun;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        PhotonView playerPhotonView = other.GetComponent<PhotonView>();

        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            SpectatorMode specComp = playerPhotonView.gameObject.GetComponent<SpectatorMode>();
            if (specComp != null)
            {
                specComp.MazeFinal(other);
            }
        }
    }
}
