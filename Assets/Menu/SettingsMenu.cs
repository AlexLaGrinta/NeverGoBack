using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{

    public TextMeshProUGUI FOVtext;
    public TextMeshProUGUI Sensitext;
    public Canvas Settings;
    public AudioSource music;

    public AudioMixer mixer;

    // Résolution
    public TMP_Dropdown resolutionDropdown;
    public Button windowedButton;
    public TMP_Text windowedButtonText;

    private Resolution[] resolutions;
    private bool isFullScreen;

    private void Start()
    {
        Settings.enabled = false;
        PlayerPrefs.SetFloat("FOV", 80.0f);
        PlayerPrefs.SetFloat("Sensitivity", 1.0f);
        PlayerPrefs.SetFloat("MasterVolume", 1.0f);


        isFullScreen = Screen.fullScreen;
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height} @{resolutions[i].refreshRate}Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        windowedButton.onClick.AddListener(ToggleFullScreen);

        UpdateWindowedButtonText();

    }

    void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen, resolution.refreshRate);
    }

    void ToggleFullScreen()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        UpdateWindowedButtonText();
    }

    void UpdateWindowedButtonText()
    {
        windowedButtonText.text = isFullScreen ? "Full Screen" : "Windowed";
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
