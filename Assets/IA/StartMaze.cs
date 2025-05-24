using UnityEngine;
using Photon.Pun;
using UnityEngine.Video;
using System.Collections;

public class StartMaze : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform teleportDestination;

    [SerializeField] private VideoPlayer player;

    [SerializeField] private Canvas videoCanva;

    [SerializeField] private AudioSource killerAudio;

    [SerializeField] private GameObject DirectLight;

    [SerializeField] private GameObject monster;


    private bool hasPlayed = false;


    private void Start()
    {
        monster.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {


        PhotonView playerPhotonView = other.GetComponent<PhotonView>();

        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            if (!hasPlayed)
            {
                hasPlayed = true;
                StartCoroutine(PlayAndTeleportCoroutine(playerPhotonView));
            } else
            {
                SpectatorMode specComp = playerPhotonView.gameObject.GetComponent<SpectatorMode>();
                if (specComp != null)
                {
                    specComp.MazeFinal(other);
                }
            }
        }
    }



    private IEnumerator SpawnMonster()
    {
        yield return new WaitForSeconds(2f);
        monster.SetActive(true);
    }


    [PunRPC]
    private void StartRoutineMonster()
    {
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator PlayAndTeleportCoroutine(PhotonView playerPhotonView)
    {
        videoCanva.gameObject.SetActive(true);
        player.Play();

        bool isFinished = false;
        player.loopPointReached += (VideoPlayer vp) => { isFinished = true; };
        while (!isFinished)
            yield return null;

        videoCanva.gameObject.SetActive(false);

        kill playertp = playerPhotonView.gameObject.GetComponent<kill>();
        if (playertp != null)
        {
            killerAudio.Play();
            DirectLight.SetActive(false);
            playertp.Teleport(teleportDestination.position);
            if (!monster.activeSelf)
            {
                photonView.RPC("StartRoutineMonster", RpcTarget.AllBuffered);
            }
            playerPhotonView.RPC("SyncTeleport", RpcTarget.Others, teleportDestination.position);
        }
    }

}
