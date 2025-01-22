using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Shopspawn : MonoBehaviour
{
    private GameObject spawnedCharacter;
    private int temporaryCharacterID; // Temporary character ID for preview

    // Array of buttons for selecting different characters
    public Button[] characterButtons;

    // Array of sprites for unlocked images for each character
    public Sprite[] unlockedSprites;
    public Image[] priceimg;
    public Sprite unlockedprice;

    // UI Elements
    public GameObject buyButton; // Reference to the "Buy" button
    public GameObject purchaseSuccessMessage; // Reference to the success GameObject
    public GameObject purchaseFailMessage; // Reference to the fail GameObject
    //public Text buyButtonText; // Text to display on the "Buy" button

    private const int CHARACTER_PRICE = 200; // Set the price for characters

    SFX sFX;

    private void Start()
    {
        if (GameManager.instance == null || GameManager.instance.currentCharacter == null)
        {
            Debug.LogError("GameManager or current character is null!");
            return;
        }

        // Set temporaryCharacterID to the currently selected character ID
        temporaryCharacterID = Savemaneger.Instance.currentCharacterID;

        // Spawn the currently saved character for display
        SpawnCharacter(GameManager.instance.characters[temporaryCharacterID].prefab);

        // Initialize each character button
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i; // Capture the current index for the button

            // Check if character is unlocked
            if (Savemaneger.Instance.IsCharacterUnlocked(i))
            {
                // Update button appearance for unlocked characters
                UpdateButtonAppearance(characterButtons[i], priceimg[i], unlockedSprites[i], unlockedprice);

                // Disable button functionality for unlocked characters
                characterButtons[i].interactable = false;
            }
            else
            {
                // Enable button functionality for locked characters
                characterButtons[i].interactable = true;

                // Assign the button click function
                characterButtons[i].onClick.AddListener(() => OnLockedCharacterButtonClick(index));
            }
        }

        // Hide the "Buy" button and messages by default
        buyButton.SetActive(false);
        purchaseSuccessMessage.SetActive(false);
        purchaseFailMessage.SetActive(false);

        sFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFX>();
    }

   private void UpdateButtonAppearance(Button button, Image priceImage, Sprite unlockedSprite, Sprite unlockedPriceSprite)
{
    if (unlockedSprite != null)
    {
        var buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Debug.Log($"Setting button image to unlocked sprite: {unlockedSprite.name}");
            buttonImage.sprite = unlockedSprite;
        }
    }

    if (priceImage != null && unlockedPriceSprite != null)
    {
        Debug.Log($"Setting price image to unlocked price sprite: {unlockedPriceSprite.name}");
        priceImage.sprite = unlockedPriceSprite;
    }
}

    private void OnLockedCharacterButtonClick(int index)
    {
        if (index >= 0 && index < GameManager.instance.characters.Length)
        {
            // Update the temporary character ID for preview
            temporaryCharacterID = index;

            // Spawn the preview character
            SpawnCharacter(GameManager.instance.characters[temporaryCharacterID].prefab);

            // Show the "Buy" button
            buyButton.SetActive(true);

            // Update the buy button text
            

            // Assign purchase functionality to the buy button
            Button buyButtonComponent = buyButton.GetComponent<Button>();
            if (buyButtonComponent != null)
            {
                buyButtonComponent.onClick.RemoveAllListeners(); // Clear previous listeners
                buyButtonComponent.onClick.AddListener(() => AttemptPurchase(index));
            }
        }
    }

    private void AttemptPurchase(int characterID)
{
    if (Savemaneger.Instance.money >= CHARACTER_PRICE)
    {
        Savemaneger.Instance.money -= CHARACTER_PRICE;
        Savemaneger.Instance.UnlockCharacterInShop(characterID);
        Savemaneger.Instance.Save();

        ShowSuccessMessage();

        // Refresh character buttons to update UI immediately
        RefreshCharacterButtons();

        // Optionally hide the "Buy" button after purchase
        buyButton.SetActive(false);
    }
    else
    {
        Debug.Log("Not enough money!");
        ShowFailureMessage();
    }
}


    private void ShowMessage(GameObject message)
{
    // Ensure all messages are hidden first
    if (purchaseSuccessMessage != null)
        purchaseSuccessMessage.SetActive(false);

    if (purchaseFailMessage != null)
        purchaseFailMessage.SetActive(false);

    // Show the intended message
    if (message != null)
        message.SetActive(true);
}

private IEnumerator HideMessageAfterDelay(GameObject message, float delay)
{
    yield return new WaitForSeconds(delay);

    if (message != null)
        message.SetActive(false);
}


    private void SpawnCharacter(GameObject prefab)
    {
        if (spawnedCharacter != null)
        {
            Destroy(spawnedCharacter); // Destroy the previously spawned character
        }

        // Instantiate the new character at the current position
        spawnedCharacter = Instantiate(prefab, transform.position, quaternion.identity);
    }
    private void ShowSuccessMessage()
{
    Debug.Log("Purchase successful! Showing success message...");
    if (purchaseFailMessage != null)
        purchaseFailMessage.SetActive(false);

    if (purchaseSuccessMessage != null)
    {
        purchaseSuccessMessage.SetActive(true);
        sFX.PlaySFX(sFX.buy);
    }
    // Optionally hide after a delay
    //StartCoroutine(HideMessageAfterDelay(purchaseSuccessMessage, 2f));
}

public void AttemptToBuyCharacter(int characterID)
{
    if (Savemaneger.Instance.IsCharacterUnlocked(characterID))
    {
        Debug.Log("Character already unlocked!");
        return;
    }

    // Check if the player has enough money
    if (Savemaneger.Instance.money >= 200)
    {
        // Deduct money
        Savemaneger.Instance.money -= 200;

        // Unlock the character
        Savemaneger.Instance.UnlockCharacter(characterID);

        // Show success message (via a GameObject in Unity, for example)
        ShowSuccessMessage();
    }
    else
    {
        // Show failure message (via a GameObject in Unity)
        ShowFailureMessage();
    }
}

private void ShowFailureMessage()
{
    // Activate the failure message GameObject
    purchaseFailMessage.SetActive(true);
}
private void RefreshCharacterButtons()
{
    for (int i = 0; i < characterButtons.Length; i++)
    {
        if (Savemaneger.Instance.IsCharacterUnlocked(i))
        {
            // Update button appearance for unlocked characters
            UpdateButtonAppearance(characterButtons[i], priceimg[i], unlockedSprites[i], unlockedprice);

            // Disable button functionality for unlocked characters
            characterButtons[i].interactable = false;
        }
        else
        {
            // Enable button functionality for locked characters
            characterButtons[i].interactable = true;
        }

        // Force the canvas to update after making changes to the UI
        Canvas.ForceUpdateCanvases();
        // Force button layout to rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(characterButtons[i].GetComponent<RectTransform>());
    }
}

}
