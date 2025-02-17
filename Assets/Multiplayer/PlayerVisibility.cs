using UnityEngine;
using Photon.Pun;

public class PlayerVisibilityController : MonoBehaviour
{
    public Renderer[] renderersToHide; // Les renderers du corps du joueur à cacher

    void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();

        // Si c'est le joueur local, désactiver les renderers
        if (photonView.IsMine)
        {
            foreach (Renderer renderer in renderersToHide)
            {
                renderer.enabled = false;
            }
        }
    }
}
