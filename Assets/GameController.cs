using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image[] player1SmallEggs;  // Player 1's small eggs
    [SerializeField] private Image[] player1MediumEggs; // Player 1's medium eggs
    [SerializeField] private Image[] player1LargeEggs;  // Player 1's large eggs

    [SerializeField] private Image[] player2SmallEggs;  // Player 2's small eggs
    [SerializeField] private Image[] player2MediumEggs; // Player 2's medium eggs
    [SerializeField] private Image[] player2LargeEggs;  // Player 2's large eggs

    [SerializeField] private Image[] boardSlots;        // 9 board slots

    private bool isPlayer1Turn = true;  // Flag for turn management

    void Start()
    {
        UpdateEggDisplay();
    }

    void Update()
    {
        if (isPlayer1Turn)
        {
            EnablePlayer1Eggs();
            DisablePlayer2Eggs();
        }
        else
        {
            EnablePlayer2Eggs();
            DisablePlayer1Eggs();
        }
    }

    void EnablePlayer1Eggs()
    {
        foreach (var egg in player1SmallEggs) egg.gameObject.SetActive(true);
        foreach (var egg in player1MediumEggs) egg.gameObject.SetActive(true);
        foreach (var egg in player1LargeEggs) egg.gameObject.SetActive(true);
    }

    void DisablePlayer1Eggs()
    {
        foreach (var egg in player1SmallEggs) egg.gameObject.SetActive(false);
        foreach (var egg in player1MediumEggs) egg.gameObject.SetActive(false);
        foreach (var egg in player1LargeEggs) egg.gameObject.SetActive(false);
    }

    void EnablePlayer2Eggs()
    {
        foreach (var egg in player2SmallEggs) egg.gameObject.SetActive(true);
        foreach (var egg in player2MediumEggs) egg.gameObject.SetActive(true);
        foreach (var egg in player2LargeEggs) egg.gameObject.SetActive(true);
    }

    void DisablePlayer2Eggs()
    {
        foreach (var egg in player2SmallEggs) egg.gameObject.SetActive(false);
        foreach (var egg in player2MediumEggs) egg.gameObject.SetActive(false);
        foreach (var egg in player2LargeEggs) egg.gameObject.SetActive(false);
    }

    void UpdateEggDisplay()
    {
        // Based on whose turn it is, you disable the opponent's eggs and enable the current player's eggs
        foreach (var egg in player1SmallEggs) egg.gameObject.SetActive(isPlayer1Turn);
        foreach (var egg in player2SmallEggs) egg.gameObject.SetActive(!isPlayer1Turn);

        // Repeat for medium and large eggs
    }

    // Method to handle when a player drags an egg to a slot
    public void PlaceEggOnBoard(int slotIndex, Image egg)
    {
        if (boardSlots[slotIndex].sprite == null)  // Check if the slot is empty
        {
            boardSlots[slotIndex].sprite = egg.sprite;  // Place the egg sprite in the slot
            RemoveEggFromPool(egg);
        }
    }

    void RemoveEggFromPool(Image egg)
{
    if (isPlayer1Turn)
    {
        // Check and remove egg from Player 1's pools
        RemoveFromPlayerPool(player1SmallEggs, egg);
        RemoveFromPlayerPool(player1MediumEggs, egg);
        RemoveFromPlayerPool(player1LargeEggs, egg);
    }
    else
    {
        // Check and remove egg from Player 2's pools
        RemoveFromPlayerPool(player2SmallEggs, egg);
        RemoveFromPlayerPool(player2MediumEggs, egg);
        RemoveFromPlayerPool(player2LargeEggs, egg);
    }
}

void RemoveFromPlayerPool(Image[] eggPool, Image egg)
{
    for (int i = 0; i < eggPool.Length; i++)
    {
        if (eggPool[i] == egg) // Find the egg in the pool
        {
            eggPool[i].gameObject.SetActive(false); // Disable the egg
            eggPool[i] = null; // Remove it from the pool
            break; // Exit the loop once the egg is found and removed
        }
    }
}


    // Call this method when the player finishes their turn to switch turns
    public void EndTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
        UpdateEggDisplay();
    }
}

