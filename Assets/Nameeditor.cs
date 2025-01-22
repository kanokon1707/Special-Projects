using UnityEngine;
using UnityEngine.UI;

public class NameEditor : MonoBehaviour
{
    public InputField nameInputField;  // Reference to the InputField
    public Text displayText;           // Reference to the Text UI to display the updated input
    public Button submitButton;        // Reference to the Submit Button

    void Start()
    {
        // Load player data
        Savemaneger.Instance.Load();

        // Initialize the InputField with the loaded name
        nameInputField.text = Savemaneger.Instance.playerName;

        // Display the loaded name in the Text UI
        displayText.text = Savemaneger.Instance.playerName;

        // Add listener to call OnSubmit when the button is clicked
        submitButton.onClick.AddListener(OnSubmit);
    }

    // Called when the Submit button is clicked
    public void OnSubmit()
    {
        // Get the input from the InputField
        string inputName = nameInputField.text;

        // Check if the input is empty
        if (string.IsNullOrEmpty(inputName))
        {
            Debug.LogWarning("Name input is empty!");
            return;
        }

        // Save the input name to Savemaneger
        Savemaneger.Instance.SetPlayerName(inputName);
        Savemaneger.Instance.Save();

        // Update the display text with the new name
        displayText.text = inputName;

        // Optionally, log the updated name to the console
        Debug.Log("Name updated to: " + inputName);
    }
}
