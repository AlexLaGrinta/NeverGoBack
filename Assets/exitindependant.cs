using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject currentPanel;  // Le panel actuel qu'on veut cacher
    public GameObject targetPanel;   // Le panel vers lequel on veut aller

    public void SwitchPanel()
    {
        currentPanel.SetActive(false);  // Désactive l'ancien
        targetPanel.SetActive(true);    // Active le nouveau
    }
}
