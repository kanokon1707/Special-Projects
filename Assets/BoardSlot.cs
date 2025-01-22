using UnityEngine;
using UnityEngine.UI;

public class BoardSlot : MonoBehaviour
{
    private Image placedEggImage; // The image of the egg placed in this slot
    private bool isOccupied = false; // Whether the slot is occupied
    
    /// <summary>
    /// Checks if the slot is occupied.
    /// </summary>
    public bool IsOccupied()
    {
        return isOccupied;
    }

    /// <summary>
    /// Get the image of the egg that was placed in the slot.
    /// </summary>
    public Image GetPlacedEggImage()
    {
        return placedEggImage;
    }

    /// <summary>
    /// Places the egg in the slot.
    /// </summary>
    public void PlaceEgg(GameObject egg)
    {
        Image newEggImage = egg.GetComponent<Image>();

        if (!isOccupied)
        {
            // Place the new egg if the slot is empty
            isOccupied = true;
            placedEggImage = newEggImage;

            SnapEggToSlot(egg);
        }
        else
        {
            // Replace the old egg with the new one
            ReplaceEgg(newEggImage);
        }
    }

    private void ReplaceEgg(Image newEggImage)
    {
        if (placedEggImage != null)
        {
            // Replace the old egg's sprite, color, and size with the new egg's properties
            placedEggImage.sprite = newEggImage.sprite;
            placedEggImage.color = newEggImage.color;

            // Copy the new egg's size to the existing placed egg
            RectTransform placedEggRect = placedEggImage.GetComponent<RectTransform>();
            RectTransform newEggRect = newEggImage.GetComponent<RectTransform>();
            placedEggRect.sizeDelta = newEggRect.sizeDelta;

            // Hide the new egg GameObject
            newEggImage.gameObject.SetActive(false);
        }

        // Ensure the replaced egg is properly centered in the slot
        CenterEggInSlot();
    }

    private void SnapEggToSlot(GameObject egg)
    {
        // Snap the egg to the center of the slot
        egg.transform.SetParent(transform); // Set egg's parent to this slot
        egg.transform.localPosition = Vector3.zero; // Reset local position to slot center

        // Optional: Reset the egg's scale and rotation to match the slot
        egg.transform.localScale = Vector3.one; // Ensure the egg is scaled correctly
        egg.transform.localRotation = Quaternion.identity; // Reset rotation
    }

    private void CenterEggInSlot()
    {
        if (placedEggImage != null)
        {
            RectTransform placedEggRect = placedEggImage.GetComponent<RectTransform>();
            placedEggRect.SetParent(transform); // Set the egg image's parent to the slot
            placedEggRect.localPosition = Vector3.zero; // Center the egg image within the slot
            placedEggRect.localScale = Vector3.one; // Ensure proper scaling
            placedEggRect.localRotation = Quaternion.identity; // Ensure proper rotation
        }
    }
}
