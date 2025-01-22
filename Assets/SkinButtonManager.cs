using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinController : MonoBehaviour
{
    public Button targetButton;           // Reference to the UI Button
    public Image buttonImage;             // Image component of the button
    public Sprite newSkinImage;           // The image to set when the condition is met
    public Sprite newSkinImage2;
    private int[] characterSkinIndices = new int[12]
    {
        0, 1,  // Character ID 0 skins
        2, 3,  // Character ID 1 skins
        4, 5,  // Character ID 2 skins
        6, 7,  // Character ID 3 skins
        8, 9,  // Character ID 4 skins
        10, 11 // Character ID 5 skins
    };

    private void Start()
    {
        // Check if the confirm button has already been clicked and the prefab is set
        if (GameManager.instance != null && GameManager.instance.IsCharacterSkinSet)
        {
            // Disable the button if the confirm button was clicked
            DisableButton();
        }
        else
        {
            // Otherwise, update the button state normally
            UpdateButtonState();
        }
    }
    
    public void UpdateButtonState()
    {
        // Disable the button by default
        targetButton.interactable = false;
        
        // Get the current character ID from GameManager
        int currentCharacterID = GameManager.instance.currentCharacter.id;

        // Check the two skin indices for this character
        int skinIndex1 = characterSkinIndices[currentCharacterID * 2];
        int skinIndex2 = characterSkinIndices[currentCharacterID * 2 + 1];

        // Check if either skin is owned
        bool hasSkin1 = Savemaneger.Instance.haveskin[skinIndex1];
        bool hasSkin2 = Savemaneger.Instance.haveskin[skinIndex2];

        // If either skin is owned, activate the button and set the new image
        if (hasSkin1 || hasSkin2)
        {
            targetButton.interactable = true; // Activate the button
            if (buttonImage != null && newSkinImage != null)
            {
                buttonImage.sprite = newSkinImage; // Change the button image
            }
        }
    }

    private void DisableButton()
    {
        targetButton.interactable = false;

        // Optionally set the button image to a disabled state
        if (buttonImage != null && newSkinImage != null)
        {
            buttonImage.sprite = newSkinImage2; // Optionally use a different sprite for the disabled state
        }

        Debug.Log("Target button has been disabled because the confirm button was clicked.");
    }
}
