using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    public Button PLAY; 

    public void Play()
    {
        SceneManager.LoadScene("MapScene");

    }
}
