using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject spawnedCharacter; // Reference to the spawned character

    private void Start()
    {
        // Check if GameManager exists and is properly initialized
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null! Make sure GameManager is loaded first.");
            return;
        }

        // Check if the current character is valid
        if (GameManager.instance.currentCharacter == null)
        {
            Debug.LogError("Current character is null! Ensure the character is set in GameManager.");
            return;
        }

        // Check if the current character has an associated prefab
        GameObject characterPrefab = GameManager.instance.currentCharacter.prefab;
        if (characterPrefab == null)
        {
            Debug.LogError("Character prefab is null! Assign a valid prefab in the Character settings.");
            return;
        }

        // Instantiate the saved character's prefab at the spawner's position
        spawnedCharacter = Instantiate(characterPrefab, transform.position, quaternion.identity);
        Debug.Log("Spawned character successfully.");

    }
    
}
