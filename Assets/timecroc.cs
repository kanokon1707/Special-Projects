using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class timecroc : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    [SerializeField] string nextSceneName; // Scene name to load after time is up
    [SerializeField] GameObject checkGameObject; // GameObject to check if it's active
    [SerializeField] GameObject victoryIndicator; // Victory indicator (set by ObjectClickHandler)

    private bool gameEnded = false; // Flag to prevent loading the next scene after victory

    void Update()
    {
        if (gameEnded)
        {
            // If the game has ended (e.g., victory achieved), ensure timer stays at 0
            remainingTime = 0;
            timerText.text = "0";
            return;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int second = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0}", second);

        // Check if time has run out
        if (remainingTime == 0)
        {
            // Only load the next scene if the GameObject is NOT active and the game hasn't ended
            if (checkGameObject != null && !checkGameObject.activeInHierarchy)
            {
                LoadNextScene();
            }
            else
            {
                Debug.Log("The GameObject is active, not loading the next scene.");
            }
        }
    }

    public void EndGame()
    {
        // Called by ObjectClickHandler to indicate the game has ended (victory)
        gameEnded = true;
        Debug.Log("Game ended! Timer stopped.");
    }

    void LoadNextScene()
    {
        // Make sure a valid scene name is provided before trying to load it
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }
}
