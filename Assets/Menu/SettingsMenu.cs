using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    public void GeneralVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
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



}
