using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Character[] characters;       // Array of available characters
    public Character currentCharacter;   // Currently selected character
    private Character previousCharacter;
    
    private void Update()
{
    // Check if the "O" key is pressed to reset the character
    if (Input.GetKeyDown(KeyCode.O))
    {
        ResetCharacterToPrevious();
    }
}
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager objects
            return;
        }

        InitializeCharacters(); // Initialize the character IDs
        LoadCharacter();
    }

    private void InitializeCharacters()
    {
        // Assign unique IDs to each character in the array
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].id = i; // Assign the array index as the character ID
        }
    }

    private void LoadCharacter()
{
    // Load the saved character ID
    int savedCharacterID = Savemaneger.Instance.currentCharacterID; 
    if (savedCharacterID >= 0 && savedCharacterID < characters.Length)
    {
        currentCharacter = characters[savedCharacterID];
    }
    else
    {
        currentCharacter = characters[0]; // Default to the first character if no valid character is saved
    }

    // Load the saved skin prefab
    if (Savemaneger.Instance.currentSkinPrefab != null)
    {
        SetCharacterSkin(Savemaneger.Instance.currentSkinPrefab);
    }
    else
    {
        // If no skin is saved, use default character skin
        SetCharacterSkin(currentCharacter.prefab);
    }
}
    public bool IsCharacterSkinSet { get; private set; } = false;

public void SetCharacterSkin(GameObject skinPrefab)
{
    if (currentCharacter != null && skinPrefab != null)
    {
        currentCharacter.prefab = skinPrefab;
        IsCharacterSkinSet = true; // Set the flag here
        Debug.Log($"Character skin updated: {skinPrefab.name}");
        ReplaceCurrentCharacterGameObject(skinPrefab);
    }
    else
    {
        Debug.LogError("SkinPrefab or currentCharacter is null!");
    }
}

private void ReplaceCurrentCharacterGameObject(GameObject skinPrefab)
{
    // Find the currently instantiated character GameObject
    GameObject currentCharacterGameObject = GameObject.Find(currentCharacter.prefab.name);
    
    if (currentCharacterGameObject != null)
    {
        // Store the position and rotation of the existing character
        Vector3 position = currentCharacterGameObject.transform.position;
        Quaternion rotation = currentCharacterGameObject.transform.rotation;

        // Destroy the existing character GameObject
        Destroy(currentCharacterGameObject);

        // Instantiate the new prefab at the same position and rotation
        GameObject newCharacterGameObject = Instantiate(skinPrefab, position, rotation);
        newCharacterGameObject.name = skinPrefab.name; // Ensure the name matches the prefab
        Debug.Log($"Instantiated new character prefab: {skinPrefab.name}");
    }
    else
    {
        Debug.LogWarning("No current character GameObject found to replace.");
    }
}

    public void SetCharacter(Character character)
    {
        // Store the current character before changing
        previousCharacter = currentCharacter;
        
        currentCharacter = character;

        // Save the selected character's ID
        Savemaneger.Instance.currentCharacterID = character.id;
        Savemaneger.Instance.UnlockCharacter(character.id); // Unlock the character
        Savemaneger.Instance.Save();
    }

    // Method to reset the character back to the previous one
    public void ResetCharacterToPrevious()
    {
        if (previousCharacter != null)
        {
            currentCharacter = previousCharacter;
            Debug.Log($"Character reset to previous: {currentCharacter.id}");
            
            // Optionally, save the previous character ID
            Savemaneger.Instance.currentCharacterID = previousCharacter.id;
            Savemaneger.Instance.Save();
        }
        else
        {
            Debug.LogWarning("No previous character found.");
        }
    }
    public void SetCharacterchange(Character character){
        currentCharacter = character;
        Savemaneger.Instance.currentCharacterID = character.id;
        Savemaneger.Instance.Save();
    }
    public Sprite GetCurrentCharacterSadImage()
    {
        if (currentCharacter != null)
        {
            return currentCharacter.sadImage;
        }
        return null; // Return null if currentCharacter is not set
    }

    public GameObject GetCurrentCharacterDicePrefab()
    {
        if (currentCharacter != null)
        {
            return currentCharacter.dicePrefab;
        }
        return null; // Return null if currentCharacter is not set
    }
    
    public GameObject InstantiateCurrentCharacterPrefab(Vector3 position, Quaternion rotation)
    {
        if (currentCharacter != null)
        {
            return Instantiate(currentCharacter.prefab, position, rotation);
        }
        return null; // Return null if currentCharacter is not set
    }
    public void GoToNextScreen(string sceneName)
{
    if (!string.IsNullOrEmpty(sceneName))
    {
        SceneManager.LoadScene(sceneName); // Load the scene by name
    }
    else
    {
        Debug.LogError("Scene name is null or empty. Please provide a valid scene name.");
    }
}

}
