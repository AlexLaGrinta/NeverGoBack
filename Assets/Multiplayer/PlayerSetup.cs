using UnityEngine;

public class PlayerSetup : MonoBehaviour
{

    public PlayerController movement;
    //public InputManager inputManager;
    public GameObject camera;
    public GameObject cine;
    public GameObject Player;

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

}

