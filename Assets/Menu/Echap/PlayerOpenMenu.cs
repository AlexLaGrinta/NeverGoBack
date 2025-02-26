using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerOpenMenu : MonoBehaviourPun
{
    public Canvas Echap;
    public Canvas Settings;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Echap.enabled)
            {
                if (Settings.enabled)
                {
                    FirstPersonController FirstPerson = gameObject.GetComponent<FirstPersonController>();
                    FirstPerson.mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f); ;
                    gameObject.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = PlayerPrefs.GetFloat("FOV", 80.0f);
                    Settings.enabled = false;
                }
                Echap.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameObject.GetComponent<FirstPersonController>().enabled = true;
            }
            else
            {
                Echap.enabled = true;
                Cursor.lockState = CursorLockMode.None; 
                Cursor.visible = true;
                gameObject.GetComponent<FirstPersonController>().enabled = false; 
            }
        }
    }
}
