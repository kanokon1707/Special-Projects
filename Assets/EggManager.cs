using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class EggManager : MonoBehaviour
{
    [SerializeField] private GameObject player1WinObject;
    [SerializeField] private GameObject player2WinObject;
    [SerializeField] private Image[] player1SmallEggs;
    [SerializeField] private Image[] player1MediumEggs;
    [SerializeField] private Image[] player1LargeEggs;
    [SerializeField] private Image[] player2SmallEggs;
    [SerializeField] private Image[] player2MediumEggs;
    [SerializeField] private Image[] player2LargeEggs;

    [SerializeField] private Sprite player1SmallEggGrey;
    [SerializeField] private Sprite player1MediumEggGrey;
    [SerializeField] private Sprite player1LargeEggGrey;
    [SerializeField] private Sprite player2SmallEggGrey;
    [SerializeField] private Sprite player2MediumEggGrey;
    [SerializeField] private Sprite player2LargeEggGrey;

    private bool isPlayer1Turn = true;

    private Dictionary<Image, Sprite> originalSprites = new Dictionary<Image, Sprite>();
    private HashSet<Image> placedEggs = new HashSet<Image>();

    private BoardSlot[,] boardSlots = new BoardSlot[3, 3];

    //oz
    SFX sFX;
    //oz

    void Start()
    {
        
        // Initialize the board slots and other setups
        boardSlots[0, 0] = GameObject.Find("t1").GetComponent<BoardSlot>();
        boardSlots[0, 1] = GameObject.Find("t2").GetComponent<BoardSlot>();
        boardSlots[0, 2] = GameObject.Find("t3").GetComponent<BoardSlot>();
        boardSlots[1, 0] = GameObject.Find("m1").GetComponent<BoardSlot>();
        boardSlots[1, 1] = GameObject.Find("m2").GetComponent<BoardSlot>();
        boardSlots[1, 2] = GameObject.Find("m3").GetComponent<BoardSlot>();
        boardSlots[2, 0] = GameObject.Find("b1").GetComponent<BoardSlot>();
        boardSlots[2, 1] = GameObject.Find("b2").GetComponent<BoardSlot>();
        boardSlots[2, 2] = GameObject.Find("b3").GetComponent<BoardSlot>();

        StoreOriginalSprites(player1SmallEggs);
        StoreOriginalSprites(player1MediumEggs);
        StoreOriginalSprites(player1LargeEggs);
        StoreOriginalSprites(player2SmallEggs);
        StoreOriginalSprites(player2MediumEggs);
        StoreOriginalSprites(player2LargeEggs);

        // Get the player who should go first
        string firstPlayer = PlayerPrefs.GetString("firstPlayer", "Player 1");
        isPlayer1Turn = (firstPlayer == "Player 1");

        UpdateEggVisuals();

        //oz
        sFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<SFX>();
        //oz
    }

    // Toggle turn between players
    public void ToggleTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
        UpdateEggVisuals();
    }

    private void UpdateEggVisuals()
    {
        UpdateEggArray(player1SmallEggs, isPlayer1Turn, player1SmallEggGrey);
        UpdateEggArray(player1MediumEggs, isPlayer1Turn, player1MediumEggGrey);
        UpdateEggArray(player1LargeEggs, isPlayer1Turn, player1LargeEggGrey);

        UpdateEggArray(player2SmallEggs, !isPlayer1Turn, player2SmallEggGrey);
        UpdateEggArray(player2MediumEggs, !isPlayer1Turn, player2MediumEggGrey);
        UpdateEggArray(player2LargeEggs, !isPlayer1Turn, player2LargeEggGrey);
    }

    private void UpdateEggArray(Image[] eggArray, bool isActive, Sprite greySprite)
    {
        foreach (var egg in eggArray)
        {
            if (placedEggs.Contains(egg)) continue;

            egg.sprite = isActive ? originalSprites[egg] : greySprite;
            egg.raycastTarget = isActive;
        }
    }

    private void StoreOriginalSprites(Image[] eggArray)
    {
        foreach (var egg in eggArray)
        {
            if (egg != null && !originalSprites.ContainsKey(egg))
            {
                originalSprites.Add(egg, egg.sprite);
            }
        }
    }

    public void MarkEggAsPlaced(Image egg)
    {
        if (!placedEggs.Contains(egg))
        {
            placedEggs.Add(egg);
        }
    }

    public void TryPlaceEggOnBoard(BoardSlot slot, Image eggImage)
{
    if (slot.IsOccupied())
    {
        Image placedEgg = slot.GetPlacedEggImage();

        if (CanReplaceEgg(placedEgg, eggImage))
        {
            ReplaceEgg(slot, eggImage);
        }
        else
        {
            Debug.Log("Cannot replace with a smaller egg!");
            return;
        }
    }
    else
    {
        PlaceEgg(slot, eggImage);
    }

    // Re-check win conditions after the egg is placed or replaced
    if (CheckForWin())
    {
        GameOver();
    }
}


    public bool CanReplaceEgg(Image placedEgg, Image newEgg)
{
    string placedEggType = GetEggType(placedEgg);
    string newEggType = GetEggType(newEgg);

    // Only allow replacing smaller eggs with larger ones
    if (placedEggType == "Small" && (newEggType == "Medium" || newEggType == "Large"))
    {
        return true;
    }
    else if (placedEggType == "Medium" && newEggType == "Large")
    {
        return true;
    }

    // Large eggs cannot be replaced
    return false;
}


    private void ReplaceEgg(BoardSlot slot, Image newEgg)
{
    Image oldEgg = slot.GetPlacedEggImage();
    if (oldEgg != null)
    {
        placedEggs.Remove(oldEgg); // Remove the old egg from placed eggs
        Destroy(oldEgg.gameObject); // Optionally destroy the old egg's GameObject
    }

    // Place the new egg and update the slot's state
    PlaceEgg(slot, newEgg);
}


    private void PlaceEgg(BoardSlot slot, Image eggImage)
    {
        placedEggs.Add(eggImage); // Mark the egg as placed
        slot.PlaceEgg(eggImage.gameObject); // Assuming a method in BoardSlot to place an egg
    }

    private string GetEggType(Image egg)
    {
        if (Array.Exists(player1SmallEggs, x => x == egg) || Array.Exists(player2SmallEggs, x => x == egg)) return "Small";
        if (Array.Exists(player1MediumEggs, x => x == egg) || Array.Exists(player2MediumEggs, x => x == egg)) return "Medium";
        if (Array.Exists(player1LargeEggs, x => x == egg) || Array.Exists(player2LargeEggs, x => x == egg)) return "Large";
        return "Unknown";
    }

    public bool CheckForWin()
{
    for (int i = 0; i < 3; i++)
    {
        // Check rows and columns for a win
        if (CheckLine(boardSlots[i, 0], boardSlots[i, 1], boardSlots[i, 2]))  // Row
              
        {
            Savemaneger.Instance.achievementData.eggyRowWins++;
            Savemaneger.Instance.CheckAchievements();
            Savemaneger.Instance.money +=80;
            Savemaneger.Instance.Save();
            return true;
        }
        else if(CheckLine(boardSlots[0, i], boardSlots[1, i], boardSlots[2, i])){ // Column
            Savemaneger.Instance.money +=80;
            Savemaneger.Instance.Save();
            return true;
            
        }
    }

    // Check diagonals for a win
    if (CheckLine(boardSlots[0, 0], boardSlots[1, 1], boardSlots[2, 2]) || // Main diagonal
        CheckLine(boardSlots[0, 2], boardSlots[1, 1], boardSlots[2, 0]))   // Anti-diagonal
    {
        Savemaneger.Instance.achievementData.eggyDiagonalWins++;
        Savemaneger.Instance.CheckAchievements();
        Savemaneger.Instance.money +=80;
        Savemaneger.Instance.Save();
        return true;
    }

    return false;
}


private bool CheckLine(BoardSlot slot1, BoardSlot slot2, BoardSlot slot3)
{
    // Ensure all slots are occupied
    if (slot1.IsOccupied() && slot2.IsOccupied() && slot3.IsOccupied())
    {
        // Get the eggs placed in the slots
        Image egg1 = slot1.GetPlacedEggImage();
        Image egg2 = slot2.GetPlacedEggImage();
        Image egg3 = slot3.GetPlacedEggImage();

        // Check if all eggs belong to the current player
        if (AreAllEggsOwnedByCurrentPlayer(egg1, egg2, egg3))
        {
            return true;
        }
    }

    return false;
}

private bool AreAllEggsOwnedByCurrentPlayer(Image egg1, Image egg2, Image egg3)
{
    // For Player 1's turn, ensure all eggs belong to Player 1
    if (isPlayer1Turn)
    {
        return IsPlayer1Egg(egg1) && IsPlayer1Egg(egg2) && IsPlayer1Egg(egg3);
    }
    // For Player 2's turn, ensure all eggs belong to Player 2
    else
    {
        return IsPlayer2Egg(egg1) && IsPlayer2Egg(egg2) && IsPlayer2Egg(egg3);
    }
}





private bool IsPlayer1Egg(Image egg)
{
    return egg.sprite == player1SmallEggs[0].sprite ||
           egg.sprite == player1MediumEggs[0].sprite ||
           egg.sprite == player1LargeEggs[0].sprite;
}

private bool IsPlayer2Egg(Image egg)
{
    return egg.sprite == player2SmallEggs[0].sprite ||
           egg.sprite == player2MediumEggs[0].sprite ||
           egg.sprite == player2LargeEggs[0].sprite;
}

    public void GameOver()
{
    if (isPlayer1Turn)
    {
        Debug.Log("Player 1 wins!");
        player1WinObject.SetActive(true);  // Show Player 1's winning object
        player2WinObject.SetActive(false); // Hide Player 2's winning object
        //Oz
        sFX.PlaySFX(sFX.Win);
        //Oz
        
        
    }
    else
    {
        Debug.Log("Player 2 wins!");
        player1WinObject.SetActive(false); // Hide Player 1's winning object
        player2WinObject.SetActive(true);  // Show Player 2's winning object
        //Oz
        sFX.PlaySFX(sFX.Win);
        //Oz
        
    }
}

    
}

