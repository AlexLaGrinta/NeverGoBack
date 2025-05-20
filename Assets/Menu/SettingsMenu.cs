using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public TextMeshProUGUI FOVtext;
    public TextMeshProUGUI Sensitext;
    public Canvas Settings;
    public AudioSource music;

    public AudioMixer mixer;

    private void Start()
    {
        Settings.enabled = false;
        PlayerPrefs.SetFloat("FOV", 80.0f);
        PlayerPrefs.SetFloat("Sensitivity", 1.0f);
        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
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

    public void VolumeChanger(float volume)
    {
        music.volume = volume;
    }

    public void MasterVolumeChanger(float volume)
    {
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20;
        bool found = mixer.SetFloat("VolumeMDR", dB);
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
