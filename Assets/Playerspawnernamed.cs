using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace for the Button

public class PlayerSpawnernamed : MonoBehaviour
{
    // Store a reference to the instantiated prefab
    private GameObject spawnedCharacter;

    // Reference to the UI Buttons
    public Button deactivateButton;
    public Button activateButton1;  // First activation button
    public Button activateButton2;  // Second activation button
    public Button nextButton;
    public Button confirmButton;
    private Image nextButtonImage;
    public Sprite newnextButtonSprite;
    private void Start()
    {
        // Make sure the nextButton is initially not interactable
        nextButton.interactable = false;
        nextButtonImage = nextButton.GetComponent<Image>();
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        if (GameManager.instance.currentCharacter == null)
        {
            Debug.LogError("CurrentCharacter is null!");
            return;
        }

        if (GameManager.instance.currentCharacter.prefab == null)
        {
            Debug.LogError("Character prefab is null!");
            return;
        }

        // Instantiate the character and store the reference
        spawnedCharacter = Instantiate(GameManager.instance.currentCharacter.prefab, transform.position, quaternion.identity);

        // Add listeners to the buttons' onClick events
        if (deactivateButton != null)
        {
            deactivateButton.onClick.AddListener(DeactivateSpawnedCharacter);
        }
        else
        {
            Debug.LogError("Deactivate Button is not assigned!");
        }

        if (activateButton1 != null)
        {
            activateButton1.onClick.AddListener(ActivateSpawnedCharacter);
        }
        else
        {
            Debug.LogError("Activate Button 1 is not assigned!");
        }

        if (activateButton2 != null)
        {
            activateButton2.onClick.AddListener(ActivateSpawnedCharacter);
        }
        else
        {
            Debug.LogError("Activate Button 2 is not assigned!");
        }

        if (confirmButton != null)
        {
            
            confirmButton.onClick.AddListener(OnConfirmButtonClick);
        }
        else
        {
            Debug.LogError("Confirm Button is not assigned!");
        }
    }

    // Function to deactivate the spawned prefab
    private void DeactivateSpawnedCharacter()
    {
        if (spawnedCharacter != null)
        {
            spawnedCharacter.SetActive(false);
        }
        else
        {
            Debug.LogError("No spawned character to deactivate!");
        }
    }

    // Function to activate the spawned prefab
    private void ActivateSpawnedCharacter()
    {
        if (spawnedCharacter != null)
        {
            spawnedCharacter.SetActive(true);
        }
        else
        {
            Debug.LogError("No spawned character to activate!");
        }
    }

    // Function to handle confirm button click
    private void OnConfirmButtonClick()
    {
        // Enable the nextButton
        if (nextButton != null)
        {
            nextButtonImage.sprite = newnextButtonSprite;
            nextButton.interactable = true;
        }
        else
        {
            Debug.LogError("Next Button is not assigned!");
        }
    }
}
