using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThreeOrderClickHandler : MonoBehaviour
{
    private List<Sprite> clickOrder = new List<Sprite>(); // List to track button images
    public Image order1; 
    public Image order2; 
    public Image order3; 
    public Button ConfirmButton; 
    public Sprite cantConfirmSprite; // Sprite for "Can't Confirm"
    public Sprite confirmSprite; // Sprite for "Confirm"
     public Button CancelButton; // Reference to the Cancel button
    public Sprite cantCancelSprite; // Sprite for "Can't Cancel"
    public Sprite cancelSprite; // Sprite for "Cancel"
    public Sprite blankSprite;
    public Canvas WinCanvas;
    public Canvas LoseCanvas;
    public int maxClicksToTrack = 3; // Maximum clicks to track (3 in this case)
    private int clicks = 0; // Counter for clicks
    public int maxClicks = 3; // Maximum number of button clicks allowed
    private bool cancelUpdated = false; // Tracks if the Cancel button has been clicked
    public Sprite RiceButton, SalmonButton, SeaWeedButton, MilkButton, EggButton, HoneyButton;

    
    SFX sFX;

    
    


    private void Start()
    {
        // Initialize ConfirmButton with the "Can't Confirm" sprite
        if (ConfirmButton != null && cantConfirmSprite != null)
        {
            ConfirmButton.image.sprite = cantConfirmSprite;
        }
        if (CancelButton != null && cantCancelSprite != null)
        {
            CancelButton.image.sprite = cantCancelSprite;
        }
        // Initialize Win and Lose canvases to be hidden
        if (WinCanvas != null) WinCanvas.gameObject.SetActive(false);
        if (LoseCanvas != null) LoseCanvas.gameObject.SetActive(false);

        // Add a listener for the CancelButton and ConfirmButton
        if (CancelButton != null) CancelButton.onClick.AddListener(OnCancelButtonClicked);
        if (ConfirmButton != null) ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);

        ResetOrderImages();
        sFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFX>();

    }

    // This method will be called when any button is clicked
    public void OnButtonClicked(Button clickedButton)
    {
        
        if (clicks >= maxClicks)
            return;

        if (!cancelUpdated)
        {
            UpdateCancelButtonSprite();
        }

        // Get the Source Image (Sprite) of the clicked button
        Sprite clickedSprite = clickedButton.image.sprite;

        // Add the sprite to the click order if it's not already there
        if (!clickOrder.Contains(clickedSprite))
        {
            clickOrder.Add(clickedSprite);
            clicks++;

            // Limit the list to the maximum number of clicks
            if (clickOrder.Count > maxClicksToTrack)
            {
                clickOrder.RemoveAt(0); // Remove the first element to maintain only 3 clicks
            }
        }

        // Update the sprites based on the click order
        UpdateOrderSprites();
        if (clicks == maxClicks)
        {
            
            UpdateConfirmButtonSprite();
        }
    }

    // Method called when the ConfirmButton is clicked
    private void OnConfirmButtonClicked()
    {
        // Check if the correct order is met for the current scene
        if (IsCorrectOrder())
        {
            // Show the WinCanvas
            if (WinCanvas != null) 
            {
                WinCanvas.gameObject.SetActive(true);
                sFX.PlaySFX(sFX.Win);
            }
            
        }
        else
        {
            // Show the LoseCanvas
            if (LoseCanvas != null) 
            {
                LoseCanvas.gameObject.SetActive(true);
                sFX.PlaySFX(sFX.Lose);
            }
        }
    }

    // Method to check if the correct order is matched for the current scene
    public bool IsCorrectOrder()
{
    // Get the current scene name
    string currentScene = SceneManager.GetActiveScene().name;

    // Match conditions based on the scene
    if (currentScene == "Level1")
    {
        bool correctOrder = order1.sprite == RiceButton && order2.sprite == SalmonButton && order3.sprite == SeaWeedButton;
        
        if (correctOrder)
        {
            Savemaneger.Instance.money += 40; // Add money when correct order is matched
            Savemaneger.Instance.Save();
            
        }
        return correctOrder;
    }
    else if (currentScene == "Level2")
    {
        bool  correctOrder = order1.sprite == MilkButton && order2.sprite == EggButton && order3.sprite == HoneyButton;
        if (correctOrder)
        {
            Savemaneger.Instance.money += 50; // Add money when correct order is matched
            Savemaneger.Instance.achievementData.secondstage = true;
            Savemaneger.Instance.CheckAchievements();
            Savemaneger.Instance.Save();
        }
        return correctOrder;

    }

    // Default to false if no match
    return false;
}




    private void OnCancelButtonClicked()
    {
        // Clear all tracked clicks and UI updates
        ResetTracker();
    }

    private void UpdateOrderSprites()
    {
        // Assign sprites to order1, order2, and order3 based on the click order
        order1.sprite = clickOrder.Count > 0 ? clickOrder[0] : blankSprite;
        order2.sprite = clickOrder.Count > 1 ? clickOrder[1] : blankSprite;
        order3.sprite = clickOrder.Count > 2 ? clickOrder[2] : blankSprite;
    }
    private void UpdateConfirmButtonSprite()
    {
        if (ConfirmButton != null && confirmSprite != null)
        {
            ConfirmButton.image.sprite = confirmSprite;
        }
    }
    private void UpdateCancelButtonSprite()
    {
        if (CancelButton != null && cancelSprite != null)
        {
            CancelButton.image.sprite = cancelSprite;
            cancelUpdated = true; // Ensure this happens only once
        }
    }
    private void ResetTracker()
    {
        clickOrder.Clear();
        clicks = 0;
        cancelUpdated = false;

        // Reset ConfirmButton and CancelButton sprites
        if (ConfirmButton != null && cantConfirmSprite != null)
            ConfirmButton.image.sprite = cantConfirmSprite;

        if (CancelButton != null && cantCancelSprite != null)
            CancelButton.image.sprite = cantCancelSprite;
        
        ResetOrderImages();

         // Hide canvases
        if (WinCanvas != null)
            WinCanvas.gameObject.SetActive(false);

        if (LoseCanvas != null)
            LoseCanvas.gameObject.SetActive(false);
    }
     private void ResetOrderImages()
    {
        if (blankSprite != null)
        {
            order1.sprite = blankSprite;
            order2.sprite = blankSprite;
            order3.sprite = blankSprite;
        }
    }
    
}
