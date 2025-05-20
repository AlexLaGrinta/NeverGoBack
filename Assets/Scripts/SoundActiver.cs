using UnityEngine;
using Photon.Pun;

public class SoundActiver : MonoBehaviourPun
{

    private AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.enabled = false;
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool found = false;
        foreach (var player in players)
        {
            var comp = player.GetComponent<PhotonView>();
            if (comp != null)
            {
                if (comp.IsMine)
                {
                    found = true;
                    break;
                }
            }
        }
        if (found)
        {
            audioS.enabled = true;
            this.enabled = false;
        }
    }
}
