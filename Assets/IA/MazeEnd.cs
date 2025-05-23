using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
public class MazeEnd : MonoBehaviour
{

    public SkillBasedMapGenerator generator;

    private Vector3[]  allPos;

    public GameObject lava;


    public AudioSource killerAudio;

    public GameObject DirectLight;



    private Vector3 FindGoodPlat()
    {
        allPos = generator.PosGetter();
        Vector3 lavaPos = lava.transform.position;
        float lava_y = lavaPos.y;
        int i = 0;
        while (lava_y >= allPos[i].y)
        {
            i++;
        }
        if (i > allPos.Length - 4)
        {
            return allPos[^1];
        } else
        {
            return allPos[i + 3];
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        PhotonView playerPhotonView = other.GetComponent<PhotonView>();

        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            kill playertp = other.GetComponent<kill>();
            if (playertp != null)
            {

                Vector3 newpos = FindGoodPlat();
                newpos.y += 1f;
                killerAudio.Stop();
                //DirectLight.SetActive(true);
                playertp.Teleport(newpos);
                playerPhotonView.RPC("SyncTeleport", RpcTarget.Others, newpos);
            }
        }
    }


}
