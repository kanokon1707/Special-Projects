using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Savemaneger : MonoBehaviour
{
    public static Savemaneger Instance { get; private set; }
    public bool[] characterskinselected = new bool[6];
    public string playerName;
     public bool[] haveskin = new bool[12];
    public int currentCharacterID = 0; // Tracks the currently selected character
    public bool isConfirmButtonDisabled = false; // Tracks the confirm button's state
    public int gameManagerCurrentCharacterID;
    public int money; // Tracks the player's currency
    public bool[] characterunlocked = new bool[6]; // Tracks unlocked characters
    public bool IsDataLoaded { get; private set; }
    public AchievementData achievementData = new AchievementData();
    public bool[] achievementButtonStates =new bool[6];
    public GameObject currentSkinPrefab;
public void ResetCharacterSkins()
{
    for (int i = 0; i < characterskinselected.Length; i++)
    {
        characterskinselected[i] = false;
    }

    Save();
    Debug.Log("All character skins have been reset.");
}
    private void Start()
    {
    Load(); // Simulate loading
    IsDataLoaded = true; // Set to true after loading
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }

        if (characterunlocked == null || characterunlocked.Length == 0)
        {
            characterunlocked = new bool[6];
        }
        if (achievementButtonStates == null || achievementButtonStates.Length != 6)
        {
        achievementButtonStates = new bool[6]; // Reinitialize to correct size
        }
        if (achievementButtonStates == null || achievementButtonStates.Length != 6)
    {
        achievementButtonStates = new bool[6]; // Reinitialize to correct size
    }
    }
    private void Update()
    {
        // Listen for key presses to trigger achievement updates
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseCrocWins();
            
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetAchievementData();
            ResetAchievementButtonStates();
            ResetSkinOwnership();
            ResetCharacterSkins();
            ResetGameData();
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            Increasewinrows();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Increasewindiagonal();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            IncreaseCrocLosses();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            passmatch();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Savemaneger.Instance.money+=100;
        }
        
        
    }
    public void passmatch()
    {
        achievementData.fourthstage = true;
        achievementData.secondstage = true;
        CheckAchievements();
    }
    public void IncreaseCrocWins()
    {
        achievementData.crocWins++;
        Debug.Log("Croc Wins increased: " + achievementData.crocWins);
        CheckAchievements(); // Check if any new achievements should be unlocked
    }

    public void IncreaseCrocLosses()
    {
        achievementData.crocLosses++;
        Debug.Log("Croc Losses increased: " + achievementData.crocLosses);
        CheckAchievements(); // Check if any new achievements should be unlocked
    }
    public void Increasewinrows()
    {
        achievementData.eggyRowWins++;
        CheckAchievements(); // Check if any new achievements should be unlocked
    }
    public void Increasewindiagonal()
    {
        achievementData.eggyDiagonalWins++;
        CheckAchievements(); // Check if any new achievements should be unlocked
    }
    public void ResetSkinOwnership()
    {
        // Reset all skins to 'false' (not owned)
        Array.Clear(haveskin, 0, haveskin.Length); // Clear the array

        // Save after resetting to ensure the change is persistent
        Save();

        Debug.Log("Skin ownerships have been reset.");
    }
    public void ResetGameData()
{
    // Reset all game data to its default values
    playerName = "ใส่ชื่อตรงนี้"; // Set default player name
    currentCharacterID = 0; // Set default character
    money = 0; // Set default money value
    Array.Clear(characterunlocked, 0, characterunlocked.Length); // Reset character unlocks

    // Reset achievement data
    achievementData = new AchievementData(); // Set default achievement data

    // Save the reset data
    Save();

    Debug.Log("Game data reset to default values.");
}

    public void ResetAchievementData()
    {
        achievementData.crocWins = 0;
        achievementData.crocLosses = 0;
        achievementData.eggyDiagonalWins = 0;
        achievementData.eggyRowWins = 0;
        achievementData.fourthstage = false;
        achievementData.secondstage = false;

        // Reset achievement statuses
        for (int i = 0; i < achievementData.achievementsUnlocked.Length; i++)
        {
            achievementData.achievementsUnlocked[i] = false;
        }

        Save(); // Save after resetting
        Debug.Log("Achievement data reset to default values.");
    }
    public void CheckAchievements()
    {
        var data = achievementData;
        Debug.Log("Checking achievements...");
        // Unlock achievement for pass second stage of match game
        if (data.secondstage == true && !data.achievementsUnlocked[0])
        {
            data.achievementsUnlocked[0] = true;
            Debug.Log("Unlocked Achievement: pass second stage of match game");
        }
        // Unlock achievement for pass fourth stage of match game
        if (data.fourthstage == true && !data.achievementsUnlocked[1])
        {
            data.achievementsUnlocked[1] = true;
            Debug.Log("Unlocked Achievement: pass fourth stage of match game");
        }
        // Unlock achievement for 4 win rows eggy chess
        if (data.eggyRowWins >= 4 && !data.achievementsUnlocked[4])
        {
            data.achievementsUnlocked[4] = true;
            Debug.Log("Unlocked Achievement: 4 win rows eggy chess");
        }
        // Unlock achievement for 4 win diagonal eggy chess
        if (data.eggyDiagonalWins >= 4 && !data.achievementsUnlocked[5])
        {
            data.achievementsUnlocked[5] = true;
            Debug.Log("Unlocked Achievement: 4 win diagonal eggy chess");
        }
        // Unlock achievement for 7 croc wins
        if (data.crocWins >= 7 && !data.achievementsUnlocked[2])
        {
            data.achievementsUnlocked[2] = true;
            Debug.Log("Unlocked Achievement: Croc Wins 7 Times");
        }

        // Unlock achievement for 7 croc losses
        if (data.crocLosses >= 7 && !data.achievementsUnlocked[3])
        {
            data.achievementsUnlocked[3] = true;
            Debug.Log("Unlocked Achievement: Croc Losses 7 Times");
        }

        // Save updated achievement state
        Save();
    }
    public void SetPlayerName(string name)
    {
        playerName = name;
        Save(); // Save immediately after updating the name
    }
   public void Load()
{
    if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        currentCharacterID = PlayerPrefs.GetInt("CurrentCharacterID", 0);
        string skinPrefabName = PlayerPrefs.GetString("CurrentSkinPrefab", "");
        if (!string.IsNullOrEmpty(skinPrefabName))
        {
            GameObject skinPrefab = Resources.Load<GameObject>(skinPrefabName);
            if (skinPrefab != null)
            {
                currentSkinPrefab = skinPrefab;
            }
        }
        playerData_Storage data = (playerData_Storage)bf.Deserialize(file);
        file.Close();

        currentCharacterID = data.currentCharacterID;
        money = data.money;
        characterunlocked = data.characterunlocked;
        achievementData = data.achievementData;
        isConfirmButtonDisabled = data.isConfirmButtonDisabled;
        playerName = data.playerName;
         if (data.haveskin != null && data.haveskin.Length == haveskin.Length){
            characterskinselected = data.characterskinselected;
         }
        else
        {
            characterskinselected = new bool[6]; // Or any default value you prefer
        }
        // Load skin ownership
        if (data.haveskin != null && data.haveskin.Length == haveskin.Length)
        {
            haveskin = data.haveskin;
        }
        else
        {
            haveskin = new bool[12]; // Or any default value you prefer
        }

        // Load achievement button states
        if (data.achievementButtonStates != null && data.achievementButtonStates.Length == achievementData.achievementsUnlocked.Length)
        {
            achievementButtonStates = data.achievementButtonStates;
        }
        else
        {
            achievementButtonStates = new bool[achievementData.achievementsUnlocked.Length]; // Default to all buttons enabled
        }
    }
    else
    {
        Debug.Log("No saved data found. Using default values.");
    }
}



    public void Save()
{
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
    PlayerPrefs.SetInt("CurrentCharacterID", currentCharacterID);
    if (currentSkinPrefab != null)
        {
            PlayerPrefs.SetString("CurrentSkinPrefab", currentSkinPrefab.name);
        }
        PlayerPrefs.Save();
    playerData_Storage data = new playerData_Storage
    {
        currentCharacterID = currentCharacterID,
        
        characterskinselected = characterskinselected,
        money = money,
        characterunlocked = characterunlocked,
        achievementData = achievementData,
        playerName = playerName,
        haveskin = haveskin,
        isConfirmButtonDisabled = isConfirmButtonDisabled,
        achievementButtonStates = achievementButtonStates // Save the button states
    };

    bf.Serialize(file, data);
    file.Close();
}


    /// <summary>
    /// Unlocks a character in the shop. This does not affect other characters' unlock status.
    /// </summary>
    public void UnlockCharacterInShop(int characterID)
    {
        if (characterID >= 0 && characterID < characterunlocked.Length)
        {
            characterunlocked[characterID] = true; // Unlock this character
            Save(); // Save after unlocking
            Debug.Log($"Character {characterID} unlocked in shop!");
        }
        else
        {
            Debug.LogError("Invalid character ID passed to UnlockCharacterInShop!");
        }
    }

    /// <summary>
    /// Unlocks a character in the selection screen. Resets all others to locked.
    /// </summary>
    public void UnlockCharacter(int characterID)
{
    if (characterID >= 0 && characterID < characterunlocked.Length)
    {
        // Unlock only the selected character and reset others
        for (int i = 0; i < characterunlocked.Length; i++)
        {
            characterunlocked[i] = false;  // Lock all characters first
        }

        characterunlocked[characterID] = true;  // Unlock the selected character
        
        Save(); // Save after unlocking
    }
}

public void SelectCharacter(int characterID)
{
    if (characterID >= 0 && characterID < characterunlocked.Length)
    {
        if (characterunlocked[characterID]) // Ensure the character is unlocked before selecting
        {
            currentCharacterID = characterID; // Update the selected character ID
            Save(); // Save the current selection
            Debug.Log($"Character {characterID} selected!");
        }
        else
        {
            Debug.LogError($"Character {characterID} is not unlocked! Selection failed.");
        }
    }
    else
    {
        Debug.LogError("Invalid character ID passed to SelectCharacter!");
    }
}
    public void UnlockCharacterInSelectionScreen(int characterID)
    {
        if (characterID >= 0 && characterID < characterunlocked.Length)
        {
            // Lock all characters first
            for (int i = 0; i < characterunlocked.Length; i++)
            {
                characterunlocked[i] = false;
            }

            characterunlocked[characterID] = true; // Unlock only the selected character
            currentCharacterID = characterID; // Update the currently selected character
            Save(); // Save the state
            Debug.Log($"Character {characterID} selected!");
        }
        else
        {
            Debug.LogError("Invalid character ID passed to UnlockCharacterInSelectionScreen!");
        }
    }

    public bool IsCharacterUnlocked(int characterID)
    {
        return characterID >= 0 && characterID < characterunlocked.Length && characterunlocked[characterID];
    }

    public void ResetCharacterUnlocked()
    {
        // Reset all characters to locked
        for (int i = 0; i < characterunlocked.Length; i++)
        {
            characterunlocked[i] = false;
        }
        Save(); // Save after resetting
    }
    public void SetSkinOwnership(int skinIndex, bool isOwned)
{
    if (skinIndex >= 0 && skinIndex < haveskin.Length)
    {
        haveskin[skinIndex] = isOwned;
        Save(); // Save after updating the ownership
        Debug.Log($"Skin at index {skinIndex} set to {isOwned}");
    }
    else
    {
        Debug.LogError("Invalid skin index!");
    }
}
public void ResetAchievementButtonStates()
{
    // Set all achievement button states to true (enabled)
    for (int i = 0; i < achievementButtonStates.Length; i++)
    {
        achievementButtonStates[i] = true;
    }

    // Save the updated button states
    Save();

    Debug.Log("Achievement button states have been reset to true.");
}


}

[Serializable]
class playerData_Storage
{
    public bool[] characterskinselected = new bool[6];
    public int currentCharacterID;
    public bool[] achievementButtonStates = new bool[6];
    public int money;
    public bool[] haveskin = new bool[12];
    public bool[] characterunlocked;
    public AchievementData achievementData;
    public bool isMusicMuted; // Add music mute state
    public float musicVolume = 1f;
    public string playerName;
    public bool isConfirmButtonDisabled = false; // Tracks the confirm button's state

}

[Serializable]
public class AchievementData
{
    public int crocWins = 0;
    public int crocLosses = 0;
    public int eggyDiagonalWins = 0;
    public int eggyRowWins = 0;
    public bool secondstage = false;
    public bool fourthstage = false;
    public bool[] achievementsUnlocked = new bool[6]; // Tracks achievement statuses
}