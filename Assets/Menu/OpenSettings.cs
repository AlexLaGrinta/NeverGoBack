using UnityEngine;

public class OpenSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainMenuPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenPanel()
    {
        settingsPanel.SetActive(true);   // Afficher le panel des paramètres
    }
}

