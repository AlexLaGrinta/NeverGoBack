using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public TextMeshProUGUI FOVtext;
    public TextMeshProUGUI Sensitext;
    public Canvas Settings;

    private void Start()
    {
        Settings.enabled = false;
        PlayerPrefs.SetFloat("FOV", 80.0f);
        PlayerPrefs.SetFloat("Sensitivity", 1.0f);
    }

    public void GeneralVolume(float volume)
    {
        Debug.Log(volume);
        //audioMixer.SetFloat("volume", volume);
    }

    public void SoundEffects(float volume)
    {
        Debug.Log(volume);
    }

    public void MenuVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void MusicVolume(float volume)
    {
        Debug.Log(volume);
    }

    public void FOVChanger(float fov)
    {
        FOVtext.text = fov.ToString();
        PlayerPrefs.SetFloat("FOV", fov);
    }

    public void SensiChanger(float sensibility)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensibility);
        Sensitext.text = sensibility.ToString("F1");
    }

    public void ExitSettingsMenu()
    {
        Settings.enabled = false;
    }

    public void OpenSettingsMenu()
    {
        Settings.enabled = true;
    }



}
