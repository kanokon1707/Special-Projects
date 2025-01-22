using UnityEngine;
using UnityEngine.UI;

public class SadImageUpdater : MonoBehaviour
{
    public GameObject imageContainer; // Container to hold the new Image (parent)
    private Image newImage; // The new Image object that will be updated
    //public Vector2 newPosition = new Vector2(0, 0); // Desired position for the sad image (relative to imageContainer)

    void Start()
    {
        // Instantiate a new Image object (you can create this dynamically or via the inspector)
        newImage = new GameObject("SadImage", typeof(Image)).GetComponent<Image>();
        newImage.transform.SetParent(imageContainer.transform); // Set the parent of the new image

        // Update the new image with the sad image from GameManager
        UpdateSadImage();
    }

    public void UpdateSadImage()
    {
        // Access the sadImage from GameManager and assign it to the new image
        Sprite sadImage = GameManager.instance.GetCurrentCharacterSadImage();
        if (sadImage != null && newImage != null)
        {
            newImage.sprite = sadImage; // Update the sprite of the new image

            // Get the size of the sprite and set the RectTransform size
            RectTransform rectTransform = newImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(sadImage.rect.width, sadImage.rect.height); // Set size to match sprite's dimensions

            // Reset the scale to (1, 1, 1) to avoid any unwanted scaling
            newImage.transform.localScale = Vector3.one;

            // Set the new position of the image relative to its parent
            //rectTransform.localPosition = newPosition; // Position relative to the parent (imageContainer)
            Debug.Log("Sad image updated, size set, scale reset to (1, 1, 1), and position set.");
        }
        else
        {
            Debug.LogWarning("Sad image or Image component is not set.");
        }
    }

    // This method updates the parent position, you can call it from somewhere else when you need to move the parent
    public void UpdateParentPosition(Vector3 newPosition)
    {
        imageContainer.transform.position = newPosition;  // Move the parent object
        // After moving the parent, ensure the sadImage's local position stays correct
        UpdateSadImage();
    }
}
