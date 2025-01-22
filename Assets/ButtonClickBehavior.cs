using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene transition
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public Button rollButton; // Reference to the button
    public Image diceImage; // Reference to the image you want to show
    public Image otherImage; // Reference to the other image to hide
    public Sprite[] diceSprites; // Array of dice sprites
    public Image bannerImage; // Banner image to show after dice roll
    private int firstRollValue = -1; // Store the first roll value
    private int secondRollValue = -1; // Store the second roll value
    private bool isFirstRoll = true; // Flag to track if it's the first roll

    void Start()
    {
        // Ensure the dice image and banner image are hidden initially
        diceImage.gameObject.SetActive(false); // Hide the dice image
        bannerImage.gameObject.SetActive(false); // Hide the banner initially

        // Ensure the other image is hidden initially (you can remove this if it's not required)
        if (otherImage != null) 
        {
            otherImage.gameObject.SetActive(false); // Hide the other image initially
        }

        // Add the Button click listener
        rollButton.onClick.AddListener(OnButtonClick);

        // Validate diceSprites array
        if (diceSprites.Length != 6)
        {
            Debug.LogError("Please assign exactly 6 dice images to the diceSprites array.");
        }

        // Validate dice image
        if (diceImage == null)
        {
            Debug.LogError("The diceImage field is not assigned. Please assign a UI Image component.");
        }

        // Validate roll button
        if (rollButton == null)
        {
            Debug.LogError("The rollButton field is not assigned. Please assign a Button component.");
        }
    }

    void OnButtonClick()
    {
        // If it's the first roll
        if (isFirstRoll)
        {
            // Hide the button immediately
            rollButton.gameObject.SetActive(false);

            // Start the dice roll process
            StartCoroutine(RollDiceAfterDelay());
        }
        else // Second roll
        {
            // Start the second roll process
            StartCoroutine(RollDiceAfterDelay());
        }
    }

    IEnumerator RollDiceAfterDelay()
    {
        // Wait for 0.5 seconds before rolling
        yield return new WaitForSeconds(0.5f);

        // Start the dice roll process
        RollDice();
    }

    public void RollDice()
    {
        // Log to check if this method is being called
        Debug.Log("RollDice method called");

        // Start the dice display coroutine
        StartCoroutine(ShowRandomDiceWithDelay());
    }

    private IEnumerator ShowRandomDiceWithDelay()
    {
        // Wait for 0.5 seconds before showing the random dice
        yield return new WaitForSeconds(0.5f);

        // Hide the other image
        if (otherImage != null)
        {
            otherImage.gameObject.SetActive(false); // Hide the other image
        }

        diceImage.gameObject.SetActive(true); // Show the dice image

        // Ensure diceSprites is not empty
        if (diceSprites.Length > 0 && diceImage != null)
        {
            // Get a random index (0 to 5)
            int randomIndex = Random.Range(0, diceSprites.Length);

            // Log the random index to ensure it's being generated correctly
            Debug.Log("Random index: " + randomIndex);

            // Set the dice image to the selected sprite
            diceImage.sprite = diceSprites[randomIndex];

            // Show the dice image (already enabled by SetActive)
            diceImage.enabled = true;

            // Log the dice value (1 to 6)
            Debug.Log("Rolled a dice: " + (randomIndex + 1));

            // Store the value of the first or second roll
            if (isFirstRoll)
            {
                firstRollValue = randomIndex + 1; // Store the first value (1 to 6)
                Debug.Log("First roll: " + firstRollValue);

                // Wait for 2 seconds before hiding the dice and showing the banner
                yield return new WaitForSeconds(2f);

                // Hide the dice image
                diceImage.gameObject.SetActive(false);

                // Show the banner image
                bannerImage.gameObject.SetActive(true);

                // Re-enable the roll button for the second roll
                rollButton.gameObject.SetActive(true);

                // Switch to second roll flag
                isFirstRoll = false;
            }
            else // If it's the second roll, store the value and compare
            {
                secondRollValue = randomIndex + 1; // Store the second value (1 to 6)
                Debug.Log("Second roll: " + secondRollValue);

                // Wait for 2 seconds before hiding the dice and showing the banner
                yield return new WaitForSeconds(2f);

                // Hide the dice image
                diceImage.gameObject.SetActive(false);

                // Show the banner image
                bannerImage.gameObject.SetActive(true);

                // Disable the roll button permanently after comparison
                rollButton.gameObject.SetActive(false);

                // Compare the rolls after both are completed
                CompareRolls();

                // Store the winner in PlayerPrefs
                if (firstRollValue > secondRollValue)
                {
                    PlayerPrefs.SetString("winner", "First Roll");
                }
                else if (secondRollValue > firstRollValue)
                {
                    PlayerPrefs.SetString("winner", "Second Roll");
                }
                else
                {
                    PlayerPrefs.SetString("winner", "Tie");
                }

                // Transition to the next scene (e.g., "NextScene" is the name of the scene you want to load)
                SceneManager.LoadScene("chess play 2");
            }
        }
        else
        {
            Debug.LogError("diceSprites array is empty or diceImage is not assigned.");
        }
    }

    // Compare the first and second roll values and log which one is greater
    private void CompareRolls()
    {
        if (firstRollValue > secondRollValue)
        {
            Debug.Log("First roll was higher: " + firstRollValue);
        }
        else if (secondRollValue > firstRollValue)
        {
            Debug.Log("Second roll was higher: " + secondRollValue);
        }
        else
        {
            Debug.Log("Both rolls were equal.");
        }
    }
}
