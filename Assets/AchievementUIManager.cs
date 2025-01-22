using UnityEngine;
using UnityEngine.UI;

public class AchievementUIManager : MonoBehaviour
{
    [SerializeField] private Text achievementText; // Reference to the Text component
    private Savemaneger saveManager; // Reference to Savemaneger for achievement data

    private void Start()
    {
        // Get the instance of the Savemaneger
        saveManager = Savemaneger.Instance;

        // Update the text at the start
        UpdateAchievementText();
    }

    private void Update()
    {
        // Continuously update the achievement text in case any achievement is unlocked during gameplay
        UpdateAchievementText();
    }

    /// <summary>
    /// Updates the achievement text showing unlocked/total achievements.
    /// </summary>
    private void UpdateAchievementText()
    {
        int unlockedAchievements = 0;

        // Count the number of unlocked achievements
        foreach (bool isUnlocked in saveManager.achievementData.achievementsUnlocked)
        {
            if (isUnlocked)
            {
                unlockedAchievements++;
            }
        }

        // Set the text to display unlocked/total achievements (e.g., 0/6)
        achievementText.text = $"{unlockedAchievements}/6";
    }
}
