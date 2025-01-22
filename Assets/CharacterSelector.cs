/*using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour {
    public GameObject optionPrefab;
    private int currentpet;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    private int charactersToShow = 2; // Limit to show only 2 characters
    public Button confirmButton; // Reference to the confirm button
    public Sprite newConfirmButtonSprite; // Sprite to change the confirm button to when a character is selected
    public Button backButton;
    private int selectedCharacterID = -1; // Variable to store the selected character ID
    private Image confirmButtonImage; // Reference to the confirm button's Image component

    private void Start() {
        // Get the Image component of the confirm button
        confirmButtonImage = confirmButton.GetComponent<Image>();

        // Disable the confirm button initially
        confirmButton.interactable = false;
        backButton.interactable=false;
        // Reset character unlocked data when entering this screen
        Savemaneger.Instance.ResetCharacterUnlocked();
        Savemaneger.Instance.money = 0;

        // Only show the first two characters
        for (int i = 0; i < charactersToShow && i < GameManager.instance.characters.Length; i++) {
            Character c = GameManager.instance.characters[i]; // Get the character data
            GameObject option = Instantiate(optionPrefab, transform); // Create a character selection button
            Button button = option.GetComponent<Button>();

            // Add listener to handle character selection
            button.onClick.AddListener(() => {
                if (selectedCharacter != option.transform) { // Check if the selected character is different
                    GameManager.instance.SetCharacter(c); // Set the selected character in GameManager
                    if (selectedCharacter != null) {
                        prevCharacter = selectedCharacter; // Store previous selection
                    }
                    selectedCharacter = option.transform; // Update the new selected character
                    selectedCharacterID = i; // Store the selected character's ID based on the index
                    // Enable the confirm button and update its sprite
                    if (confirmButtonImage != null && newConfirmButtonSprite != null) {
                        confirmButtonImage.sprite = newConfirmButtonSprite; // Update the confirm button's sprite
                    }
                    confirmButton.interactable = true; // Enable the confirm button
                }
            });

            // Set the character icon in the UI
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.icon;
        }
    }

    private void Update() {
        // Scale up the selected character smoothly
        if (selectedCharacter != null) {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        // Scale down the previous character smoothly
        if (prevCharacter != null) {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}*/
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour {
    public GameObject[] characterOptions; // Reference to two predefined GameObjects
    private int currentpet;
    public Transform prevCharacter;
    public Transform selectedCharacter;
    public Button confirmButton; // Reference to the confirm button
    public Sprite newConfirmButtonSprite; // Sprite to change the confirm button to when a character is selected
    public Button backButton;
    private int selectedCharacterID = -1; // Variable to store the selected character ID
    private Image confirmButtonImage; // Reference to the confirm button's Image component

    private void Start() {
        // Get the Image component of the confirm button
        confirmButtonImage = confirmButton.GetComponent<Image>();

        // Disable the confirm button initially
        confirmButton.interactable = false;
        backButton.interactable = false;

        // Reset character unlocked data when entering this screen
        Savemaneger.Instance.ResetCharacterUnlocked();
        Savemaneger.Instance.money = 0;

        // Ensure there are only 2 character options
        if (characterOptions.Length != 2) {
            Debug.LogError("Character options array must contain exactly 2 elements.");
            return;
        }

        // Assign the first two characters to the predefined GameObjects
        for (int i = 0; i < characterOptions.Length && i < GameManager.instance.characters.Length; i++) {
            Character c = GameManager.instance.characters[i]; // Get the character data
            GameObject option = characterOptions[i]; // Use the predefined GameObject
            Button button = option.GetComponent<Button>();

            // Add listener to handle character selection
            int index = i; // Capture the loop variable
            button.onClick.AddListener(() => {
                if (selectedCharacter != option.transform) { // Check if the selected character is different
                    GameManager.instance.SetCharacter(c); // Set the selected character in GameManager
                    if (selectedCharacter != null) {
                        prevCharacter = selectedCharacter; // Store previous selection
                    }
                    selectedCharacter = option.transform; // Update the new selected character
                    selectedCharacterID = index; // Store the selected character's ID based on the index
                    // Enable the confirm button and update its sprite
                    if (confirmButtonImage != null && newConfirmButtonSprite != null) {
                        confirmButtonImage.sprite = newConfirmButtonSprite; // Update the confirm button's sprite
                    }
                    confirmButton.interactable = true; // Enable the confirm button
                }
            });

            // Set the character icon in the UI
            Image image = option.GetComponentInChildren<Image>();
            image.sprite = c.icon;
        }
    }

    private void Update() {
        // Scale up the selected character smoothly
        if (selectedCharacter != null) {
            selectedCharacter.localScale = Vector3.Lerp(selectedCharacter.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * 10);
        }

        // Scale down the previous character smoothly
        if (prevCharacter != null) {
            prevCharacter.localScale = Vector3.Lerp(prevCharacter.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10);
        }
    }
}
