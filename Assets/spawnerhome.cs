using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace for the Button

public class SpawnerHome : MonoBehaviour
{
    // Store a reference to the instantiated prefab
    private GameObject spawnedCharacter;

    private void Start()
    {
        // Load data from SaveManager
        Savemaneger.Instance.Load();

        // Ensure GameManager instance exists
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null!");
            return;
        }

        // Ensure currentCharacterID is valid
        int currentCharacterID = Savemaneger.Instance.currentCharacterID;
        if (currentCharacterID < 0 || currentCharacterID >= GameManager.instance.characters.Length)
        {
            Debug.LogError("Invalid currentCharacterID!");
            return;
        }

        // Load the current character based on the currentCharacterID
        GameManager.instance.currentCharacter = GameManager.instance.characters[currentCharacterID];

        // Ensure currentCharacter exists after loading
        if (GameManager.instance.currentCharacter == null)
        {
            Debug.LogError("CurrentCharacter is null!");
            return;
        }

        // Ensure character prefab exists
        if (GameManager.instance.currentCharacter.prefab == null)
        {
            Debug.LogError("Character prefab is null!");
            return;
        }

        // Instantiate the character and store the reference
        spawnedCharacter = Instantiate(GameManager.instance.currentCharacter.prefab, transform.position, quaternion.identity);
    }
}
