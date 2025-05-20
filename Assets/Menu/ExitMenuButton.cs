using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ExitMenuButton : MonoBehaviour
{

    public void ExitMenuAction()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("Main Menu");
    }
}
