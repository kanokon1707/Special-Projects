using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacterScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public Button[] characterButtons; // Array to hold the character buttons
    public Sprite[] buttonImages; // Array to hold the images for each button

    [Header("Confirm/Cancel Buttons")]
    public Button confirmButton; // The confirm button
    public Button cancelButton; // The cancel button
    public Sprite confirmDefaultImage; // Default image for confirm button
    public Sprite confirmActiveImage; // Active image for confirm button
    public Sprite cancelDefaultImage; // Default image for cancel button
    public Sprite cancelActiveImage; // Active image for cancel button

    private int selectedCharacterID = -1; // Tracks the currently selected character
    private int previousCharacterID = -1; // Tracks the character ID before changing
    private bool isCharacterConfirmed = false; // Flag to check if the character is confirmed

    private void Start()
    {
        UpdateCharacterButtons();

        // Assign listeners to Confirm and Cancel buttons
        confirmButton.onClick.AddListener(OnConfirmClick);
        cancelButton.onClick.AddListener(OnCancelClick);

        UpdateConfirmCancelButtons(); // Initialize the button images
    }

    private void UpdateCharacterButtons()
    {
        // Loop through all buttons and update their image based on unlock status
        for (int i = 0; i < characterButtons.Length; i++)
        {
            Image buttonImage = characterButtons[i].GetComponent<Image>();

            // Check if the character is unlocked
            if (Savemaneger.Instance.IsCharacterUnlocked(i))
            {
                buttonImage.sprite = buttonImages[i]; // Set the unlocked image
                characterButtons[i].interactable = true;

                // Assign click event to immediately spawn the character
                int characterID = i; // Store index for the closure
                characterButtons[i].onClick.RemoveAllListeners();
                characterButtons[i].onClick.AddListener(() => OnCharacterButtonClick(characterID));
            }
            else
            {
                buttonImage.sprite = buttonImages[i]; // Use a specific locked image if necessary
                characterButtons[i].interactable = false; // Disable the button
            }
        }
    }

    private void OnCharacterButtonClick(int characterID)
    {
        // Store the previous character ID to revert later if needed
        previousCharacterID = GameManager.instance.currentCharacter.id;

        // Change the spawned character immediately for preview
        ChangeSpawnedCharacter(characterID);

        // Set the selected character ID for potential confirmation
        selectedCharacterID = characterID;

        // Update Confirm and Cancel button states
        UpdateConfirmCancelButtons();

        // Flag the character as previewed but not confirmed
        isCharacterConfirmed = false;

        Debug.Log($"Character {characterID} selected and spawned immediately.");
    }

    private void UpdateConfirmCancelButtons()
    {
        // Update the Confirm button image
        Image confirmImage = confirmButton.GetComponent<Image>();
        confirmImage.sprite = selectedCharacterID >= 0 ? confirmActiveImage : confirmDefaultImage;

        // Update the Cancel button image
        Image cancelImage = cancelButton.GetComponent<Image>();
        cancelImage.sprite = selectedCharacterID >= 0 ? cancelActiveImage : cancelDefaultImage;
    }

    private void OnConfirmClick()
    {
        if (selectedCharacterID < 0)
        {
            Debug.Log("No character selected. Cannot confirm.");
            return;
        }

        Debug.Log($"Confirmed character selection: {selectedCharacterID}");

        // Set the character using GameManager
        Character selectedCharacter = GameManager.instance.characters[selectedCharacterID];
        GameManager.instance.SetCharacterchange(selectedCharacter); // Update GameManager with the selected character

        // Mark the character as confirmed
        isCharacterConfirmed = true;

        // Reset selection (does not affect unlocked characters)
        selectedCharacterID = -1;

        // Update button states
        UpdateConfirmCancelButtons();
    }

    private void OnCancelClick()
    {
        Debug.Log("Cancelled character selection.");

        // Revert to the previous character if any change was made
        if (selectedCharacterID >= 0)
        {
            // Restore the previous character
            selectedCharacterID = previousCharacterID;

            // Change the spawned character to the previous character
            ChangeSpawnedCharacter(previousCharacterID);

            Debug.Log($"Character reverted to the previous character: {selectedCharacterID}");
        }

        // If no character was confirmed, do not change the GameManager's character
        if (!isCharacterConfirmed)
        {
            // Revert to the previous character if selection wasn't confirmed
            GameManager.instance.SetCharacterchange(GameManager.instance.characters[previousCharacterID]);
        }

        // Update button states to default (confirm and cancel buttons)
        UpdateConfirmCancelButtons();
    }

    private void ChangeSpawnedCharacter(int characterID)
    {
        // Fetch the new character's prefab from GameManager
        GameObject newCharacterPrefab = GameManager.instance.characters[characterID].prefab;

        if (newCharacterPrefab == null)
        {
            Debug.LogError("Character prefab is null! Ensure prefab is assigned in GameManager.");
            return;
        }

        // Find the PlayerSpawner in the scene
        PlayerSpawner playerSpawner = FindObjectOfType<PlayerSpawner>();
        if (playerSpawner == null)
        {
            Debug.LogError("PlayerSpawner not found in the scene!");
            return;
        }

        // Destroy the currently spawned character and spawn the new one
        if (playerSpawner.spawnedCharacter != null)
        {
            Destroy(playerSpawner.spawnedCharacter);
        }

        // Spawn the new character at the spawner's position
        playerSpawner.spawnedCharacter = Instantiate(newCharacterPrefab, playerSpawner.transform.position, Quaternion.identity);

        Debug.Log($"Character with ID {characterID} spawned successfully.");
    }
}
