using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonRouteManager : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    void Update()
    {
        // Listen for the "=" key press
        if (Input.GetKeyDown(KeyCode.Equals)) // The Equals key "="
        {
            ResetUsageFlag();
        }
    }

    public void OnButtonClick()
{
    if (PlayerPrefs.GetInt("HasUsedGameBefore", 0) == 1)
    {
        SceneManager.LoadScene("Home");
    }
    else
    {
        // Reset the game data when the player has not used the game before
        SceneManager.LoadScene("choose character");
        PlayerPrefs.SetInt("HasUsedGameBefore", 1);
        PlayerPrefs.Save();

        // Call the reset method in SaveManager to reset all data to default
        Savemaneger.Instance.ResetGameData();
    }
}


    // Method to reset the PlayerPrefs flag
    private void ResetUsageFlag()
    {
        PlayerPrefs.SetInt("HasUsedGameBefore", 0); // Reset to "unused"
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs 'HasUsedGameBefore' reset to 0");
    }
}
