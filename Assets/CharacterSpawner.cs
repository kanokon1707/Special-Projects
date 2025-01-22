using UnityEngine;
using System.Collections;

public class ReplaceGameObjectsWithPrefabs : MonoBehaviour
{
    //public GameObject gameObject2; // The object to show the selected skin prefab temporarily

    private GameManager gm;
    private Savemaneger saveManager;

    private void Start()
    {
        // Find the GameManager and SaveManager in the scene
        gm = GameManager.instance;
        saveManager = Savemaneger.Instance;

        if (gm == null || saveManager == null)
        {
            Debug.LogError("GameManager or SaveManager is missing!");
            return;
        }

        // Start waiting for SaveManager data
        StartCoroutine(WaitForSaveManager());
    }

    private IEnumerator WaitForSaveManager()
    {
        Debug.Log("Waiting for SaveManager to load data...");
        while (!saveManager.IsDataLoaded)
        {
            yield return null;
        }

        Debug.Log("SaveManager data loaded. Proceeding to replace objects.");

        // Check if currentCharacter and prefab are properly assigned
        if (gm.currentCharacter == null || gm.currentCharacter.prefab == null)
        {
            Debug.LogError("GameManager's currentCharacter or prefab is null!");
            yield break;
        }

        // Initially, do nothing with the prefab until the skin is selected
    }

    

    // Method to apply the selected skin when confirmed
    public void ConfirmSkinSelection()
    {
        GameObject selectedSkinPrefab = gm.currentCharacter.characterskinselected;
        if (selectedSkinPrefab != null)
        {
            // Set the character skin in GameManager
            gm.SetCharacterSkin(selectedSkinPrefab);
            
            saveManager.Save();
            Debug.Log("Character skin confirmed and applied.");
        }
        else
        {
            Debug.LogWarning("No valid skin prefab to apply.");
        }
    }

    public GameObject GetCharacterSkinPrefab()
    {
        int characterID = saveManager.currentCharacterID;
        int skinIndex1 = characterID * 2;
        int skinIndex2 = characterID * 2 + 1;

        if (skinIndex1 < saveManager.haveskin.Length && saveManager.haveskin[skinIndex1])
        {
            return gm.currentCharacter.characterskin1;
        }
        else if (skinIndex2 < saveManager.haveskin.Length && saveManager.haveskin[skinIndex2])
        {
            return gm.currentCharacter.characterskin2;
        }

        return null;
    }
}
