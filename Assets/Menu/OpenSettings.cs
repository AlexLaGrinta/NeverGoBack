using UnityEngine;

public class OpenSettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenPanel()
    {
        settingsPanel.SetActive(true);   // Afficher le panel des paramètres
    }
}

