using UnityEngine;
using UnityEngine.UI;

public class ConfirmButtonHandler : MonoBehaviour
{
    public Button confirmButton;             // The confirm button
    public Button targetButton;              // The button to disable
    public Sprite disabledButtonSprite;      // The sprite to use when the button is disabled
    private ReplaceGameObjectsWithPrefabs replaceGameObjectsWithPrefabs;

    private void Start()
{
    replaceGameObjectsWithPrefabs = FindObjectOfType<ReplaceGameObjectsWithPrefabs>();

    if (confirmButton != null)
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);

        // Restore TargetButton state from SaveManager
        if (Savemaneger.Instance != null)
        {
            if (Savemaneger.Instance.isConfirmButtonDisabled)
            {
                DisableTargetButton(); // Keep it disabled if confirmed
            }
            else
            {
                EnableTargetButton(); // Ensure it starts enabled if not confirmed
            }
        }
    }
    else
    {
        Debug.LogError("Confirm button is not assigned!");
    }
}

private void EnableTargetButton()
{
    if (targetButton != null)
    {
        targetButton.interactable = true;

        // Optionally reset the button sprite (if needed)
        Image buttonImage = targetButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = null; // Reset to the default sprite if applicable
        }

        Debug.Log("Target button enabled.");
    }
    else
    {
        Debug.LogError("Target button is not assigned.");
    }
}


    private void OnConfirmButtonClicked()
{
    if (replaceGameObjectsWithPrefabs != null)
    {
        replaceGameObjectsWithPrefabs.ConfirmSkinSelection(); // Apply the selected skin

        // Immediately disable the button
        DisableTargetButton();
    }
    else
    {
        Debug.LogError("ReplaceGameObjectsWithPrefabs not found.");
    }
}


    private void DisableTargetButton()
{
    if (targetButton != null)
    {
        targetButton.interactable = false;

        // Change the button's image if a disabled sprite is provided
        Image buttonImage = targetButton.GetComponent<Image>();
        if (buttonImage != null && disabledButtonSprite != null)
        {
            buttonImage.sprite = disabledButtonSprite;
        }

        // Save the button's state
        Savemaneger.Instance.isConfirmButtonDisabled = true;
        Savemaneger.Instance.Save();

        Debug.Log("Confirm button disabled and state saved.");
    }
    else
    {
        Debug.LogError("Target button is not assigned.");
    }
}


}
