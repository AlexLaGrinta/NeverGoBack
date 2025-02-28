using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    public Button PLAY;

    public Canvas Gamemode;
    public Canvas Main;

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
