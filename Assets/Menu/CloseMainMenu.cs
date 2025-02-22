using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public Button QUIT; // Assurez-vous d'assigner ce bouton dans l'inspecteur Unity

    void Start()
    {
        QUIT.onClick.AddListener(Quit);
    }

    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}