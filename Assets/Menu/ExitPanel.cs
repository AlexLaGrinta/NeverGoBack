using UnityEngine;

public class CloseSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel; // Référence au panneau des paramètres

    public void ClosePanel()
    {
        settingsPanel.SetActive(false); // Désactive le panneau
    }
}

