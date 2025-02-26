using UnityEngine;
using Photon.Pun; // Nécessaire pour Photon PUN

public class PlatformDisappearsAndReappears : MonoBehaviourPunCallbacks
{
    private bool isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        // Si le joueur touche la plateforme et que cet événement n'a pas encore été déclenché
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // Appel du RPC pour gérer la plateforme sur tous les clients
            photonView.RPC("MakePlatformDisappear", RpcTarget.All);
        }
    }

    [PunRPC] // Synchroniser cette méthode sur tous les clients
    void MakePlatformDisappear()
    {
        // Désactive la plateforme après un délai d'1 seconde sur tous les clients
        Invoke(nameof(HidePlatform), 1f);
    }

    void HidePlatform()
    {
        gameObject.SetActive(false); // Désactive la plateforme localement
        photonView.RPC("MakePlatformReappear", RpcTarget.All, 3f); // Appelle le RPC de réapparition
    }

    [PunRPC]
    void MakePlatformReappear(float waitTime)
    {
        // Réapparaît après le délai donné
        Invoke(nameof(ShowPlatform), waitTime);
    }

    void ShowPlatform()
    {
        gameObject.SetActive(true); // Réactive la plateforme localement
        isTriggered = false;        // Permet à nouveau l'interaction
    }
}
