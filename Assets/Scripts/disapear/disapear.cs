using Photon.Pun;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Playables;

public enum Map
{
    Montagne,
    HellTower
}


public class BoxDisabler : MonoBehaviourPunCallbacks
{
    public int requiredPlayers = 4;
    public float countdownTime = 15f;
    [SerializeField] private GameObject boxSpawn = null;

    private GameObject playerCanvas;
    private TMP_Text countdownText;
    private bool gameStarted = false;

    public Map map;
    public LavaFlow flow = null;


    public SkillBasedMapGenerator generator = null;


    public AudioSource tickStart;


    // Cinématiques
    [SerializeField]
    private PlayableDirector playableMontagne = null;

    [SerializeField]
    private PlayableDirector playableHellTower = null;

    [SerializeField]
    private GameObject CinematicCamera;


    public override void OnJoinedRoom()
    {
        // Attendre que le PlayerObject soit instanci�
        StartCoroutine(WaitForPlayerObject());
    }



    [PunRPC]
    void StartCountdown()
    {
        if (gameStarted) return;
        gameStarted = true;

        CinematicCamera.SetActive(true);

        if (map == Map.Montagne)
        {
            playableMontagne.Play();
        } else if (map == Map.HellTower)
        {
            generator.SendPlatToAll();
            playableHellTower.Play();
        }


        if (playerCanvas != null)
        {
            playerCanvas.SetActive(true);
            StartCoroutine(CountdownCoroutine());
        }
    }



    private IEnumerator WaitForPlayerObject()
    {
        while (GameObject.FindWithTag("CanvaCount") == null)
        {
            yield return null;
        }

        playerCanvas = GameObject.FindWithTag("CanvaCount");
        if (playerCanvas != null)
        {
            countdownText = playerCanvas.GetComponentInChildren<TMP_Text>();
            //playerCanvas.SetActive(false);
            countdownText.text = "";
        }
        else
        {
            Debug.LogError("Canvas du joueur introuvable !");
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !gameStarted && Input.GetKeyDown(KeyCode.End))
        {
            photonView.RPC("StartCountdown", RpcTarget.AllBuffered);
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        CheckPlayers();
    }

    [PunRPC]
    void ActualizePlayers(int players)
    {
        GameObject[] counters = GameObject.FindGameObjectsWithTag("CountPlayer");
        
        foreach (var counter in counters)
        {
            TextMeshProUGUI textt = counter.GetComponent<TextMeshProUGUI>();
            textt.text = $"{players}/{requiredPlayers}";
        }
    }

    [PunRPC]
    void DesactivateCanvas()
    {
        GameObject[] canvas = GameObject.FindGameObjectsWithTag("CanvaCount");
        foreach (var canva in canvas)
        {
            canva.gameObject.SetActive(false);
        }
    }


    void CheckPlayers()
    {
        int currentPlayers = PhotonNetwork.PlayerList.Length;
        Debug.Log($"Joueurs connectés : {currentPlayers}/{requiredPlayers}");

        photonView.RPC("ActualizePlayers", RpcTarget.AllBuffered, currentPlayers);
        if (currentPlayers >= requiredPlayers)
        {
            photonView.RPC("StartCountdown", RpcTarget.AllBuffered);
        }
    }


    IEnumerator CountdownCoroutine()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = $"Game starts in {Mathf.Ceil(timeLeft)}s";
            }

            if (timeLeft == 4) tickStart.Play();

            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        if (countdownText != null) countdownText.text = "";
        countdownText.gameObject.SetActive(false);


        if (map == Map.Montagne)
        {
            photonView.RPC("DisableBox", RpcTarget.All);
        }
        else if (map == Map.HellTower)
        {
            photonView.RPC("ActivateLava",RpcTarget.All);
        }
    }


    [PunRPC]
    void ActivateLava()
    {
        flow.enabled = true;
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(false);
        }
        CinematicCamera.SetActive(false);

        photonView.RPC("DesactivateCanvas", RpcTarget.AllBuffered);
    }




    [PunRPC]
    void DisableBox()
    {
        if (boxSpawn != null)
        {
            boxSpawn.SetActive(false);
        }
        if (playerCanvas != null)
        {
            playerCanvas.SetActive(false);
        }
        CinematicCamera.SetActive(false);
        photonView.RPC("DesactivateCanvas", RpcTarget.AllBuffered);
    }
}
