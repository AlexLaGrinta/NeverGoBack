using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{

    public PlayerController movement;
    //public InputManager inputManager;
    public GameObject camera;
    public GameObject cine;
    public GameObject Player;


    public string nickname;

    public TextMeshPro nicknameText;

    public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
        cine.SetActive(true);
        if (Player != null)
        {
            FirstPersonController FirstPerson = Player.GetComponent<FirstPersonController>();
            FirstPerson.enabled = true;

        }
        nicknameText.GetComponent<MeshRenderer>().enabled = false;
        //inputManager.enabled = true;
    }

    public void IsNotLocalPlayer()
    {
        movement.enabled = false;  // Désactive les contrôles du joueur pour les autres clients
        camera.SetActive(false);   // Désactive la caméra pour l'autre joueur
        cine.SetActive(false);
        if (Player != null)
        {
            FirstPersonController FirstPerson = Player.GetComponent<FirstPersonController>();
            FirstPerson.enabled = false;

        }
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;

        nicknameText.text = nickname;
    }


}

