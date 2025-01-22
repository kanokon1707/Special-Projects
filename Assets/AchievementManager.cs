using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // Achievement image array for locked (default state) and unlocked achievements
    public Image[] achievementImages;

    // Unlocked images for each achievement
    public Sprite[] unlockedSprites;

    // GameObjects to show when achievement is clicked (only shown for unlocked achievements)
    public GameObject[] unlockedAchievementObjects;

    private void Start()
    {
        // Initialize achievements to display the correct state on start
        UpdateAchievements();
    }
    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else
    {
        Destroy(gameObject); // Prevent duplicate instances
    }
}
    void Update()
    {
        // Listen for key presses to increase or decrease crocWins and crocLosses
    }

    /// <summary>
    /// Updates the UI for all achievements.
    /// Each achievement will show the default locked image initially.
    /// When unlocked, it will display the unlocked sprite.
    /// </summary>
    public void UpdateAchievements()
{
    var data = Savemaneger.Instance.achievementData;

    for (int i = 0; i < achievementImages.Length; i++)
    {
        bool isUnlocked = i < data.achievementsUnlocked.Length && data.achievementsUnlocked[i];
        bool isButtonInteractable = i < Savemaneger.Instance.achievementButtonStates.Length && Savemaneger.Instance.achievementButtonStates[i];

        // Update the sprite based on unlocked status
        if (isUnlocked)
        {
            achievementImages[i].sprite = unlockedSprites[i];
        }
        else
        {
            achievementImages[i].sprite = achievementImages[i].sprite; // Optionally, set a default locked sprite
        }

        // Enable or disable button interaction based on saved state
        var button = achievementImages[i].GetComponent<Button>();
        if (button != null)
        {
            button.interactable = isButtonInteractable; // Use saved state for interactability
            if (isUnlocked)
            {
                int index = i; // Local variable for capturing index in the lambda
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnAchievementClick(index));
            }
        }
    }
}


    /// <summary>
    /// Handles clicks on achievement images.
    /// Displays the associated GameObject if the achievement is unlocked.
    /// </summary>
    /// <param name="index">Index of the clicked achievement</param>
    public void OnAchievementClick(int index)
    {
        if (index >= 0 && index < unlockedAchievementObjects.Length)
        {
            // Only show the achievement object if the achievement is unlocked
            if (unlockedAchievementObjects[index] != null)
            {
                unlockedAchievementObjects[index].SetActive(true);
            }
            else
            {
                Debug.LogWarning($"No GameObject assigned for achievement at index {index}");
            }
        }
    }
    public void DisableAchievementButton(int index)
{
    if (index >= 0 && index < achievementImages.Length)
    {
        // Check if this achievement is unlocked
        if (Savemaneger.Instance.achievementData.achievementsUnlocked[index])
        {
            var button = achievementImages[index].GetComponent<Button>();
            if (button != null)
            {
                button.interactable = false; // Disable the button
                Debug.Log($"Button for achievement {index} disabled.");

                // Save the button's disabled state
                Savemaneger.Instance.achievementButtonStates[index] = false; // Store button state as disabled
                Savemaneger.Instance.Save(); // Make sure to save the state
            }
        }
        else
        {
            // If the achievement is not unlocked, ensure button is interactable
            var button = achievementImages[index].GetComponent<Button>();
            if (button != null)
            {
                button.interactable = true; // Ensure button is enabled if the achievement isn't unlocked
                Debug.Log($"Button for achievement {index} enabled.");

                // Save the button's enabled state
                Savemaneger.Instance.achievementButtonStates[index] = true; // Store button state as enabled
                Savemaneger.Instance.Save(); // Make sure to save the state
            }
        }
    }
}

}
