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
    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;


    private string nickname = "unnamed";

    public string roomNameToJoin = "test";

    void Awake()
    {
        instance = this;
    }

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.SerializationRate = 20;
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
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

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);

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
