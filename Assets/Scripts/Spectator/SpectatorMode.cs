using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;


public enum SpectateMap
{
    Montagne,
    HellTower
}


public class SpectatorMode : MonoBehaviourPun
{
    [SerializeField] private Camera spectatorCamera; // Caméra autonome pour le mode spectateur
    [SerializeField] private Camera playerCamera; // Caméra du joueur local
    [SerializeField] private Camera ownSpectatorCam; // Caméra propre au joueur local pour spectateur
    [SerializeField] private TextMeshProUGUI Ranking; // Pour afficher les classements

    [SerializeField] private Canvas EndScreen;
    [SerializeField] private TextMeshProUGUI FinalRanking;


    private SpectateMap Map;

    private List<GameObject> players; // Liste des joueurs observables
    private int currentPlayerIndex = 0; // Index du joueur actuellement observé
    public bool _isSpectatorModeActive; // Active le mode spectateur
    private List<string> winners = new List<string>(); // Liste des gagnants
    private string nickname; // Pseudo du joueur local
    private string top = "";

    private bool collided = false;

    void Start()
    {

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "MapScene")
        {
            Map = SpectateMap.Montagne;
        } else if (sceneName == "OnlyUp")
        {
            Map = SpectateMap.HellTower;
        }

        if (photonView.IsMine)
        {
            nickname = gameObject.GetComponent<PlayerSetup>().nickname;
        }

        if (ownSpectatorCam != null)
        {
            ownSpectatorCam.enabled = false;
        }
        else
        {
            Debug.LogError("`OwnSpectatorCam` n'est pas assignée. Assurez-vous d'assigner une caméra propre au joueur !");
        }

        if (!photonView.IsMine && spectatorCamera != null)
        {
            spectatorCamera.enabled = false;
        }

        UpdatePlayerList();
        _isSpectatorModeActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // Empêcher un joueur déjà spectateur de redevenir spectateur
        if (other.CompareTag("Spectateur") && !_isSpectatorModeActive)
        {
            if (collided) return;
            photonView.RPC("AddPlayerRank", RpcTarget.AllBuffered, nickname);
            ActivateSpectatorMode();
            collided = true;
        }
    }

    public void MazeFinal(Collider other)
    {
        if (!_isSpectatorModeActive)
        {
            if (collided) return;
            //photonView.RPC("AddPlayerRank", RpcTarget.AllBuffered, nickname);
            ActivateSpectatorMode();
            collided = true;
        }
    }

    [PunRPC]
    //public void AddPlayerRank(string _nickname)
    //{
    //    if (!winners.Contains(_nickname))
    //    {
    //        winners.Add(_nickname);
    //        Debug.Log($"[AddPlayerRank] {PhotonNetwork.NickName} ajoute {_nickname} au classement.");

    //        string newRankingText = "";
    //        for (int i = 0; i < winners.Count; i++)
    //        {
    //            newRankingText += (i + 1) + ". " + winners[i] + "\n";
    //        }

    //        Debug.Log($"[AddPlayerRank] Envoi du classement mis à jour :\n{newRankingText}");

    //        // Envoi à tous les joueurs pour mettre à jour leur UI (Canvas de tous les joueurs)
    //        photonView.RPC("SyncRankingForAllPlayers", RpcTarget.AllBuffered, newRankingText);
    //    }
    //}

    public void AddPlayerRank(string _nickname)
    {
        if (winners.Contains(_nickname)) return;

        winners.Add(_nickname);

        //top += winners.Count + ". " + _nickname + "\n";

        photonView.RPC("SyncRankingForAllPlayers", RpcTarget.AllBuffered, _nickname);

    }


    private static int CountNewLines(string input)
    {
        return input.Split('\n').Length - 1;
    }



    [PunRPC]
    private void SyncRankingForAllPlayers(string rankingText)
    {
        if (Map == SpectateMap.HellTower) return;

        // Trouve tous les objets avec le tag "Player"
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        // Parcours chaque joueur
        foreach (var player in allPlayers)
        {
            Debug.Log($"[SyncRankingForAllPlayers] Vérification du joueur {player.name}");

            // Recherche l'enfant "Camera"
            Transform cameraTransform = player.transform.Find("Camera");

            if (cameraTransform != null)
            {
                // Recherche l'enfant "Leaderboard" sous "Camera"
                Transform leaderboardTransform = cameraTransform.Find("Leaderboard");

                if (leaderboardTransform != null)
                {
                    // Recherche l'enfant "Finishers" sous "Leaderboard"
                    TextMeshProUGUI finishersText = leaderboardTransform.Find("Finishers")?.GetComponent<TextMeshProUGUI>();

                    if (finishersText != null)
                    {
                        if (finishersText.text.Contains(rankingText)) return;
                        finishersText.text += (CountNewLines(finishersText.text)+1).ToString() + ". " + rankingText + "\n";
                    }
                }
            }
        }
    }






    [PunRPC]
    private void SetVisibility(bool isVisible)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = isVisible;
        }

        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = isVisible;
        }

        Debug.Log($"[SetVisibility] Rendu du joueur {gameObject.name} réglé sur : {isVisible}");
    }


    private bool IsCh15RendererEnabled(GameObject obj)
    {
        Transform parent = obj.transform.parent;
        if (parent != null)
        {
            var ch15Renderer = parent.Find("character/Ch15")?.GetComponent<SkinnedMeshRenderer>();
            return ch15Renderer != null && ch15Renderer.enabled;
        }
        return false;
    }


    private void UpdatePlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Spectateur")
        .Where(player => IsCh15RendererEnabled(player))
        //{
        //    // Récupère le script SpectatorMode pour vérifier si le joueur est déjà spectateur
        //    var spectatorMode = player.GetComponent<SpectatorMode>();
        //    if (spectatorMode != null && spectatorMode._isSpectatorModeActive)
        //    {
        //        Debug.Log($"[UpdatePlayerList] Exclu {player.name} car il est déjà spectateur.");
        //        return false; // Exclure les spectateurs
        //    }

        //    PhotonView view = player.GetComponent<PhotonView>();

        //    // Exclure le joueur local
        //    return view == null || !view.IsMine;
        //})
        .ToList();

        Debug.Log($"[UpdatePlayerList] Joueurs disponibles pour spectateur : {players.Count}");
    }



    [PunRPC]
    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FinalRanking.text = Ranking.text;
        EndScreen.gameObject.SetActive(true);

        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (var player in allPlayers)
        {

            Transform cameraTransform = player.transform.Find("Camera");

            if (cameraTransform != null)
            {
                Transform EndScreenTransform = cameraTransform.Find("EndScreen");
                EndScreenTransform.parent.gameObject.SetActive(true);

                if (Map != SpectateMap.HellTower)
                {

                    if (EndScreenTransform != null)
                    {
                        TextMeshProUGUI finishersText = EndScreenTransform.Find("ranking")?.GetComponent<TextMeshProUGUI>();

                        if (finishersText != null)
                        {
                            finishersText.text = Ranking.text;
                        }
                    }
                }
            }
        }

        GameObject[] canvas = GameObject.FindGameObjectsWithTag("CanvaCount");
        foreach (var canva in canvas)
        {
            canva.gameObject.SetActive(false);
        }


    }



    private void ActivateSpectatorMode()
    {
        if (!photonView.IsMine) return;


        Debug.Log("[ActivateSpectatorMode] Activation du mode spectateur...");

        UpdatePlayerList();

        if (players.Count == 0)
        {
            Debug.LogWarning("[ActivateSpectatorMode] MDRRRRRRRRR");
            photonView.RPC("EndGame", RpcTarget.AllBuffered); // Rendre invisible pour tout le monde

            //EndGame();
            return;
        }


        kill playertp = gameObject.GetComponent<kill>();
        playertp.Teleport(new Vector3(100,100,100));
        photonView.RPC("SyncTeleport", RpcTarget.AllBuffered, new Vector3(100,100,100));

        if (playerCamera != null) playerCamera.enabled = false;
        if (ownSpectatorCam != null) ownSpectatorCam.enabled = false;

        photonView.RPC("SetVisibility", RpcTarget.AllBuffered, false); // Rendre invisible pour tout le monde

        UpdatePlayerList();

        if (players.Count == 0)
        {
            Debug.LogWarning("[ActivateSpectatorMode] Aucun joueur disponible pour spectateur.");
            //DisableSpectatorMode();
            return;
        }

        if (spectatorCamera != null) spectatorCamera.enabled = true;

        _isSpectatorModeActive = true;
        SwitchToNextPlayer();
        StartCoroutine(WorkerCoroutine());
    }

    private void DisableSpectatorMode()
    {
        return;
        Debug.Log("[DisableSpectatorMode] Désactivation du mode spectateur...");

        if (playerCamera != null) playerCamera.enabled = true;
        if (ownSpectatorCam != null) ownSpectatorCam.enabled = false;

        photonView.RPC("SetVisibility", RpcTarget.AllBuffered, true); // Rendre visible à nouveau pour tout le monde

        if (spectatorCamera != null) spectatorCamera.enabled = false;

        _isSpectatorModeActive = false;
    }

    private System.Collections.IEnumerator WorkerCoroutine()
    {
        Debug.Log("[WorkerCoroutine] Début du mode spectateur");

        while (_isSpectatorModeActive)
        {
            UpdatePlayerList();

            if (players.Count == 0)
            {
                photonView.RPC("EndGame", RpcTarget.AllBuffered);
                Debug.LogWarning("[WorkerCoroutine] Aucun joueur à observer. Fin du mode spectateur.");
                DisableSpectatorMode();
                yield break;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("[WorkerCoroutine] Changement de joueur (précédent).");
                SwitchToPreviousPlayer();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Debug.Log("[WorkerCoroutine] Changement de joueur (suivant).");
                SwitchToNextPlayer();
            }

            yield return null;
        }

        Debug.Log("[WorkerCoroutine] Fin du mode spectateur.");
    }

    private void SwitchToNextPlayer()
    {
        UpdatePlayerList();

        if (players.Count == 0)
        {
            Debug.LogWarning("[SwitchToNextPlayer] Aucun joueur disponible pour observer.");
            return;
        }

        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        Debug.Log($"[SwitchToNextPlayer] Passage à {players[currentPlayerIndex].name}");
        ObservePlayer(players[currentPlayerIndex]);
    }

    private void SwitchToPreviousPlayer()
    {
        UpdatePlayerList();

        if (players.Count == 0)
        {
            Debug.LogWarning("[SwitchToPreviousPlayer] Aucun joueur disponible pour observer.");
            return;
        }

        currentPlayerIndex = (currentPlayerIndex - 1 + players.Count) % players.Count;
        Debug.Log($"[SwitchToPreviousPlayer] Passage à {players[currentPlayerIndex].name}");
        ObservePlayer(players[currentPlayerIndex]);
    }

    private void ObservePlayer(GameObject player)
    {
        Debug.Log($"[ObservePlayer] Tentative d'observer {player.name}");

        var spectatorMode = player.GetComponentInParent<SpectatorMode>();
        // Vérifie que le joueur observé n'est pas un spectateur
        if (spectatorMode != null && spectatorMode._isSpectatorModeActive)
        {
            Debug.LogWarning($"[ObservePlayer] Impossible d'observer {player.name}, il est déjà spectateur !");
            return;
        }

        // Désactive les caméras des autres joueurs
        foreach (GameObject p in players)
        {
            Camera cam = p.GetComponent<Camera>();
            if (cam != null) cam.enabled = false;
        }

        // Active la caméra du joueur ciblé
        Camera targetCam = player.GetComponent<Camera>();
        if (targetCam != null)
        {
            targetCam.enabled = true;
            Debug.Log($"[ObservePlayer] Caméra activée pour {player.name}");
        }
        else
        {
            Debug.LogWarning($"[ObservePlayer] Aucun composant caméra trouvé pour {player.name}");
        }
    }
}