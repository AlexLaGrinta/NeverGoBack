using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class EchapMenu : MonoBehaviour
{

    public GameObject player;
    public Canvas Echap;
    public Canvas Settings;

    void Start()
    {
        Echap.enabled = false;
        Settings.enabled = false;
    }

    public void ResumeButton()
    {
        Echap.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    public void ExitButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Main Menu");
    }

    public void SettingsButton()
    {
        Settings.enabled = true;
    }


}
