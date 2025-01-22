using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ObjectClickHandler : MonoBehaviour
{
    public Button[] objectButtons;          // Array of buttons representing teeth
    public Button confirmButton;           // Confirm button
    public Button cancelButton;            // Cancel button
    public GameObject playerOneIndicator;  // Player 1 turn indicator
    public GameObject playerTwoIndicator;  // Player 2 turn indicator
    public GameObject victoryIndicator;    // Victory indicator (all incorrect teeth clicked)
    public GameObject defeatIndicator;     // Defeat indicator (correct tooth clicked)
    
    public Sprite replacedImage;           // New image to replace the clicked button's image

    private string clickedObjectName;      // Store the name of the clicked tooth
    private int currentPlayer = 1;         // Current player (1 or 2)
    private int incorrectClickCount = 0;   // Number of incorrect teeth clicked
    private HashSet<string> clickedTeeth = new HashSet<string>(); // Track clicked teeth
    public Timers gameTimer;  // Reference to the Timer script

    SFX sFX;

    void Start()
    {
        //oz
        sFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFX>();
        //oz

        currentPlayer = 1; // Start with Player 1
        UpdatePlayerIndicator(); // Display Player 1's indicator at the beginning
        
        // Initialize game state
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        victoryIndicator.SetActive(false);
        defeatIndicator.SetActive(false);

        // Add click listeners to each tooth button
        foreach (Button button in objectButtons)
        {
            button.onClick.AddListener(() => OnObjectClicked(button.name));
        }

        // Add listeners for confirm and cancel buttons
        confirmButton.onClick.AddListener(OnConfirmClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void OnObjectClicked(string objectName)
    {
        // Prevent re-clicking the same tooth
        if (clickedTeeth.Contains(objectName))
        {
            Debug.Log("This tooth has already been clicked!");
            return;
        }

        clickedObjectName = objectName; // Store the clicked tooth
        confirmButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        Debug.Log($"Tooth clicked: {objectName}");
    }

    private void OnConfirmClicked()
    {
        Debug.Log("Confirm button clicked.");
        Debug.Log($"Clicked tooth: {clickedObjectName}");
        Debug.Log($"Incorrect click count: {incorrectClickCount}");

        if (string.IsNullOrEmpty(clickedObjectName))
        {
            Debug.LogWarning("No tooth was selected!");
            return;
        }

        // Check if the clicked tooth is correct
        if (RandomObjectActivator.selectedObjectNames.Contains(clickedObjectName))
        {
            Debug.Log($"Player {currentPlayer} clicked the correct tooth. Proceeding to the next screen!");
            
            EndGame();
            return; // Stop further execution because the game advances to the next scene
        }
        else
        {
            Debug.Log($"Player {currentPlayer} clicked an incorrect tooth.");

            // Disable the clicked button and replace its image
            foreach (Button button in objectButtons)
            {
                if (button.name == clickedObjectName)
                {
                    Image buttonImage = button.GetComponent<Image>();
                    if (buttonImage != null)
                    {
                        buttonImage.sprite = replacedImage; // Replace with the new image
                    }
                    button.interactable = false; // Disable the button
                    break;
                }
            }

            clickedTeeth.Add(clickedObjectName); // Add to the set of clicked teeth
            incorrectClickCount++;

            // Check if all incorrect teeth are clicked
            if (incorrectClickCount == objectButtons.Length - RandomObjectActivator.selectedObjectNames.Count)
            {
                Debug.Log("All incorrect teeth clicked. Players win!");
                victoryIndicator.SetActive(true); // Show victory object

                // Reset the timer to 00
                if (gameTimer != null)
                {
                    gameTimer.remainingTime = 0f; // Set the remaining time to 0
                }

                EndGame(); // End the game
                return; // End execution after victory
            }
        }

        // Reset clicked object name and hide confirm/cancel buttons
        clickedObjectName = null;
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        // Switch to the next player's turn
        TogglePlayerTurn();
    }

    private void LoadNextScreen()
    {
        // Replace "NextSceneName" with the actual name of your next scene
        SceneManager.LoadScene("play croc 2");
    }

    private void OnCancelClicked()
    {
        Debug.Log("Cancel button clicked.");
        clickedObjectName = null;
        confirmButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    private void TogglePlayerTurn()
    {
        // Alternate player turn
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        UpdatePlayerIndicator();
    }

    private void UpdatePlayerIndicator()
{
    // Update player indicators based on the current player
    playerOneIndicator.SetActive(currentPlayer == 1);
    playerTwoIndicator.SetActive(currentPlayer == 2);

    // Debugging
    Debug.Log($"Player {currentPlayer}'s turn. Indicators updated.");
    Debug.Log($"Player 1 Indicator Active: {playerOneIndicator.activeSelf}");
    Debug.Log($"Player 2 Indicator Active: {playerTwoIndicator.activeSelf}");

    // Additional debug to check hierarchy and visibility
    if (!playerOneIndicator.activeSelf && currentPlayer == 1)
    {
        Debug.LogWarning("Player 1 Indicator should be active but is not.");
        Debug.Log($"Is Player 1 Indicator GameObject active in hierarchy? {playerOneIndicator.activeInHierarchy}");
    }

    if (!playerTwoIndicator.activeSelf && currentPlayer == 2)
    {
        Debug.LogWarning("Player 2 Indicator should be active but is not.");
        Debug.Log($"Is Player 2 Indicator GameObject active in hierarchy? {playerTwoIndicator.activeInHierarchy}");
    }
}


    private void EndGame()
{
    Debug.Log("Game has ended.");

    foreach (Button button in objectButtons)
    {
        button.interactable = false;
    }

    confirmButton.gameObject.SetActive(false);
    cancelButton.gameObject.SetActive(false);

    // Debugging
    Debug.Log("Victory Indicator Active: " + victoryIndicator.activeSelf);
    Debug.Log("Defeat Indicator Active: " + defeatIndicator.activeSelf);

    if (victoryIndicator.activeSelf)
    {
        //oz
        
        Debug.Log("Victory condition met!");
        
        // Play the Win sound effect
        if (sFX != null)
        {
            sFX.PlaySFX(sFX.Win);
        }
        
        //oz

        Savemaneger.Instance.achievementData.crocWins++;
        Savemaneger.Instance.money +=50;
        Savemaneger.Instance.Save();
        Debug.Log("Croc Wins incremented: " + Savemaneger.Instance.achievementData.crocWins);
    }
    else
    {
        Debug.Log("Defeat condition met.");
        if (sFX != null)
        {
            Debug.Log("Playing Lose sound...");
            sFX.PlaySFX(sFX.bite);
        }
        Savemaneger.Instance.achievementData.crocLosses++;
        Debug.Log("Croc Losses incremented: " + Savemaneger.Instance.achievementData.crocLosses);
        LoadNextScreen();
        
    }

    // Ensure changes are saved
    Savemaneger.Instance.Save();
    Debug.Log("Data saved.");

    // Check if achievements need to be updated
    Savemaneger.Instance.CheckAchievements();
    //Invoke("LoadNextScreen", 0.5f);
}



}
