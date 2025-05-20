using UnityEngine;
using Photon.Pun;
using TMPro;

public class CupFinal : MonoBehaviourPun
{

    public Canvas FinalScreen;
    public TextMeshProUGUI Winner;



    [PunRPC]
    private void ShowFinal(string winner)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Winner.text = winner + " has won !";
        FinalScreen.gameObject.SetActive(true);
        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<FirstPersonController>().enabled = false;
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PhotonView playerPhotonView = other.GetComponent<PhotonView>();

        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            string nickname = other.GetComponent<PlayerSetup>().nickname;
            photonView.RPC("ShowFinal", RpcTarget.AllBuffered, nickname);
        }
    }
}
