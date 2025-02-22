using UnityEngine;

public class CloseSettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel; // Référence au panneau des paramètres

    public void ClosePanel()
    {
        settingsPanel.SetActive(false); // Désactive le panneau
    }
}

