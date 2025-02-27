using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemode : MonoBehaviour
{

    public void JoinScene(int index)
    {
        SceneManager.LoadScene(index);
    }



}
