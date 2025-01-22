using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class characterimg2 : MonoBehaviour
{
    // Reference to the Image component or SpriteRenderer to display the character images
    public Image[] characterImages; // For UI Image components (Unity UI)
    // public SpriteRenderer[] characterImages; // Uncomment this if you're using SpriteRenderer in 2D scenes

    // Reference to the Savemaneger instance to access unlocked character data
    public Savemaneger saveManager;

    // Reference to the new images to replace the old ones
    public Sprite[] newCharacterImages; // Array to hold the new images for unlocked characters

    void Start()
    {
        // Make sure you have a reference to the SaveManager instance
        if (saveManager == null)
        {
            saveManager = Savemaneger.Instance;
        }

        UpdateCharacterImages(); // Initial image update
    }

    void Update()
    {
        // Optionally, you can update images dynamically if character unlocks change in real-time
        // For example, checking the character unlocks every frame
        // (this can be useful if you have dynamic changes, but typically you'd call this after an unlock)
        UpdateCharacterImages();
    }

    // This method updates the character images based on the `characterunlocked` array
    void UpdateCharacterImages()
    {
        if (characterImages.Length != newCharacterImages.Length)
        {
            Debug.LogError("Mismatch between number of character images and new images array!");
            return;
        }

        // Iterate through all characters and update images based on whether they are unlocked

        for (int i = 3; i < 6; i++)
        {
            if (saveManager.characterunlocked[i])
            {
                // Set the new image if the character is unlocked
                characterImages[i].sprite = newCharacterImages[i];
            }
        }
    }
}
