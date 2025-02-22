using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string modeToLoad;
    public void StartGame()
    {
        //SceneManager.LoadScene(modeToLoad);
        Debug.Log("selemmm");
    }

    public void SettingsButton()
    {
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StoreButton()
    {
        
    }




}
