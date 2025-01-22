using UnityEngine;
using UnityEngine.UI;

public class XOGameHandler : MonoBehaviour
{
    [SerializeField] private Image[] player1Eggs; // References to Player 1's eggs (9 images)
    [SerializeField] private Image[] player2Eggs; // References to Player 2's eggs (9 images)
    [SerializeField] private Image[] boardSlots; // References to the board slots (9 slots)

    private int[] player1EggCounts = { 3, 3, 3 }; // [small, mid, large]
    private int[] player2EggCounts = { 3, 3, 3 }; // [small, mid, large]

    private bool isPlayer1Turn = true; // Player 1 starts by default

    void Start()
    {
        UpdateEggPool();
        UpdateTurnUI();
    }

    // Update egg pool UI for both players
    private void UpdateEggPool()
    {
        // Update Player 1's eggs
        for (int i = 0; i < player1Eggs.Length; i++)
        {
            bool isActive = player1EggCounts[i / 3] > 0 && isPlayer1Turn;
            player1Eggs[i].gameObject.SetActive(isActive);
            player1Eggs[i].color = isPlayer1Turn ? Color.white : Color.gray; // Gray out if not their turn
        }

        // Update Player 2's eggs
        for (int i = 0; i < player2Eggs.Length; i++)
        {
            bool isActive = player2EggCounts[i / 3] > 0 && !isPlayer1Turn;
            player2Eggs[i].gameObject.SetActive(isActive);
            player2Eggs[i].color = isPlayer1Turn ? Color.gray : Color.white; // Gray out if not their turn
        }
    }

    // Update the turn UI (and any other turn-specific visuals)
    private void UpdateTurnUI()
    {
        Debug.Log(isPlayer1Turn ? "Player 1's turn" : "Player 2's turn");
    }

    // Method to handle dragging an egg to the board
    public void OnEggDraggedToBoard(int eggType, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= boardSlots.Length) return; // Invalid slot index

        if (isPlayer1Turn && player1EggCounts[eggType] > 0)
        {
            PlaceEggOnBoard(1, eggType, slotIndex);
        }
        else if (!isPlayer1Turn && player2EggCounts[eggType] > 0)
        {
            PlaceEggOnBoard(2, eggType, slotIndex);
        }
    }

    // Place the egg on the board and update the game state
    private void PlaceEggOnBoard(int player, int eggType, int slotIndex)
    {
        // Update the board slot image
        boardSlots[slotIndex].sprite = (player == 1 ? player1Eggs[eggType * 3].sprite : player2Eggs[eggType * 3].sprite);
        boardSlots[slotIndex].gameObject.SetActive(true);

        // Update the egg pool
        if (player == 1)
        {
            player1EggCounts[eggType]--;
        }
        else
        {
            player2EggCounts[eggType]--;
        }

        // Update UI
        UpdateEggPool();

        // Switch turn
        isPlayer1Turn = !isPlayer1Turn;
        UpdateTurnUI();
    }
}
