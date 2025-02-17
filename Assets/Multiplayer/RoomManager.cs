using UnityEngine;
using Photon.Pun;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;

    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.SerializationRate = 20;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("We are in the lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("We are connected in a room");

        // Instancier le joueur local et récupérer la référence PhotonView pour le contrôle
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        // Vérifier que ce joueur est bien le propriétaire du PhotonView
        PhotonView photonView = _player.GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        }
        else
        {
            _player.GetComponent<PlayerSetup>().IsNotLocalPlayer();
        }
    }

}
