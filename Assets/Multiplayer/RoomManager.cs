using UnityEngine;
using Photon.Pun;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject player;
    [Space]
    public Transform spawnPoint;
    [Space]
    public GameObject roomCam;


    void Awake()
    {
        instance = this;
    }

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

        roomCam.SetActive(false);

        // Instancier le joueur local et r�cup�rer la r�f�rence PhotonView pour le contr�le
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        // V�rifier que ce joueur est bien le propri�taire du PhotonView
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

    public void RespawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

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
