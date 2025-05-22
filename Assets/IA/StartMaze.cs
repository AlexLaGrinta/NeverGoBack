using UnityEngine;
using Photon.Pun;
using UnityEngine.Video;
using System.Collections;

public class StartMaze : MonoBehaviour
{
    public Transform teleportDestination;

    public VideoPlayer player;

    public Canvas videoCanva;

    public AudioSource killerAudio;

    public GameObject DirectLight;


    private bool hasPlayed = false;




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
            playerPhotonView.RPC("SyncTeleport", RpcTarget.Others, teleportDestination.position);
        }
    }

}
