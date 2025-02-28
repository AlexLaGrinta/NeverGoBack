using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject[] playerObjects;
    private int selectedPlayerIndex = 0;

    public Transform spawnPoint;

    public GameObject roomCam;
    public GameObject nameUI;
    public GameObject connectingUI;

    private string nickname = "unnamed";
    public string roomNameToJoin = "test";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectedPlayerIndex = PlayerPrefs.GetInt("selectedCharacter", 0);
        Debug.Log(selectedPlayerIndex);
    }

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void SelectPlayerObject(int index)
    {
        selectedPlayerIndex = index;
    }

    public void JoinRoomButtonPressed()
    {
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (playerObjects == null || playerObjects.Length == 0)
        {
            Debug.LogError("Player objects array is empty or not assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        GameObject _player = PhotonNetwork.Instantiate(playerObjects[selectedPlayerIndex].name, spawnPoint.position, Quaternion.identity);

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
        GameObject _player = PhotonNetwork.Instantiate(playerObjects[selectedPlayerIndex].name, spawnPoint.position, Quaternion.identity);

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
