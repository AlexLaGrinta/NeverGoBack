using UnityEngine;
using Photon.Pun;

public class CheckPoint : MonoBehaviourPun
{
    private GameObject spawnPoint;
    private CharacterController controller;

    void Start()
    {
        spawnPoint = GameObject.Find("CP");
        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint introuvable ! Assurez-vous qu'il est bien dans la scène.");
        }

        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("Pas de CharacterController attaché au joueur !");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        if (other.CompareTag("Final"))
        {

            if (spawnPoint != null && controller != null)
            {
                Teleport(spawnPoint.transform.position);

                photonView.RPC("SyncTeleport", RpcTarget.Others, spawnPoint.transform.position);
            }
        }
    }

    public void Teleport(Vector3 newPosition)
    {
        controller.enabled = false;
        transform.position = newPosition; 
        controller.enabled = true;

        Debug.Log($"Le joueur local {gameObject.name} a été téléporté au SpawnPoint ({newPosition}).");
    }


    [PunRPC]
    public void SyncTeleport(Vector3 newPosition)
    {
        if (!photonView.IsMine)
        {
            controller.enabled = false;
            transform.position = newPosition;
            controller.enabled = true;
        }

        Debug.Log($"Player téléporté via réseau à la position ({newPosition}).");
    }


}