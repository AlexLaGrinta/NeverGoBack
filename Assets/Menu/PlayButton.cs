using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button PLAY;

    [SerializeField] private Canvas Gamemode;
    [SerializeField] private Canvas Main;

    private void Start()
    {
        Gamemode.enabled = false;
    }

    public void BackMain()
    {
        Gamemode.enabled = false;
        Main.enabled = true;
    }

    public void Play()
    {
        Gamemode.enabled = true;
        Main.enabled = false;
    }

    public void ChangeSkin()
    {
        SceneManager.LoadScene("SkinSelection");
    }

}
