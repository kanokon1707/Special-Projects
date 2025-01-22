using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class confirmname : MonoBehaviour
{
    public InputField inputField;
    public Text displayText;
    
    // This method should be triggered when the confirm button is clicked
    void Start()
    {
        // Load player data
        Savemaneger.Instance.Load();
        
        // Display the player's name in the Text UI
        UpdateDisplay();
    }
    public void OnSubmit()
    {
        // Check if the input is empty
        if (string.IsNullOrEmpty(inputField.text))
        {
            Debug.LogWarning("Player name is empty!");
            return;  // Exit if there's no name entered
        }

        // Save the input field text to the playerName in SaveManager
        Savemaneger.Instance.SetPlayerName(inputField.text);  // Set player name in SaveManager

        // Log the input to the console
        Debug.Log("User Input: " + inputField.text);
        
        // Save data
        Savemaneger.Instance.Save();
        UpdateDisplay();
        // Optionally, load a new scene or proceed to the next step
        // SceneManager.LoadScene("Home"); // Uncomment this line if you want to load the "Home" scene
    }
    public void UpdateDisplay()
    {
        displayText.text = Savemaneger.Instance.playerName;
    }
}

