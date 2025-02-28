using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;
	public int selectedCharacter = 0;



    private void Start()
    {
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
	}

	public void NextCharacter()
	{
		characters[selectedCharacter].SetActive(false);
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		characters[selectedCharacter].SetActive(true);
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
	}

	public void PreviousCharacter()
	{
		characters[selectedCharacter].SetActive(false);
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		characters[selectedCharacter].SetActive(true);
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
	}

	public void SaveAndLeave()
	{
		Debug.Log(PlayerPrefs.GetInt("selectedCharacter", 64));
		SceneManager.LoadScene(0);
	}
}
