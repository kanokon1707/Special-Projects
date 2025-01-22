using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EggGameHandler : MonoBehaviour
{
    [Header("Player Egg Pools")]
    [SerializeField] private Image[] player1SmallEggs;
    [SerializeField] private Image[] player1MediumEggs;
    [SerializeField] private Image[] player1LargeEggs;
    
    [SerializeField] private Image[] player2SmallEggs;
    [SerializeField] private Image[] player2MediumEggs;
    [SerializeField] private Image[] player2LargeEggs;

    [Header("Turn Handling")]
    [SerializeField] private GameObject player1TurnPanel;
    [SerializeField] private GameObject player2TurnPanel;

    private int currentPlayer = 1; // 1 = Player 1's turn, 2 = Player 2's turn

    void Start()
    {
        // Set initial UI based on the turn
        SetTurnUI();

        // Initialize the egg pools
        InitializeEggPools();
    }

    void SetTurnUI()
    {
        if (currentPlayer == 1)
        {
            player1TurnPanel.SetActive(true);
            player2TurnPanel.SetActive(false);
            DisablePlayer2Eggs();
        }
        else
        {
            player1TurnPanel.SetActive(false);
            player2TurnPanel.SetActive(true);
            DisablePlayer1Eggs();
        }
    }

    void InitializeEggPools()
    {
        // Set all eggs visible initially (for both players)
        SetEggsInteractable(player1SmallEggs, true);
        SetEggsInteractable(player1MediumEggs, true);
        SetEggsInteractable(player1LargeEggs, true);
        SetEggsInteractable(player2SmallEggs, true);
        SetEggsInteractable(player2MediumEggs, true);
        SetEggsInteractable(player2LargeEggs, true);
    }

    void SetEggsInteractable(Image[] eggs, bool interactable)
    {
        foreach (Image egg in eggs)
        {
            egg.color = interactable ? Color.white : Color.grey;
            // Optionally, you can disable the Button component to prevent dragging.
            var button = egg.GetComponent<Button>();
            if (button != null) button.interactable = interactable;
        }
    }

    void DisablePlayer1Eggs()
    {
        SetEggsInteractable(player1SmallEggs, false);
        SetEggsInteractable(player1MediumEggs, false);
        SetEggsInteractable(player1LargeEggs, false);
        SetEggsInteractable(player2SmallEggs, true);
        SetEggsInteractable(player2MediumEggs, true);
        SetEggsInteractable(player2LargeEggs, true);
    }

    void DisablePlayer2Eggs()
    {
        SetEggsInteractable(player1SmallEggs, true);
        SetEggsInteractable(player1MediumEggs, true);
        SetEggsInteractable(player1LargeEggs, true);
        SetEggsInteractable(player2SmallEggs, false);
        SetEggsInteractable(player2MediumEggs, false);
        SetEggsInteractable(player2LargeEggs, false);
    }

    public void OnEggDropped(int player, string eggSize, GameObject egg)
    {
        // Remove the egg from the pool and update the UI after it is dropped on the board
        if (player == 1)
        {
            RemoveEggFromPool(player1SmallEggs, eggSize);
        }
        else
        {
            RemoveEggFromPool(player2SmallEggs, eggSize);
        }

        // Update egg pool after drag-drop
        UpdateEggCountUI(player);

        // Switch turn after drop
        currentPlayer = (currentPlayer == 1) ? 2 : 1;
        SetTurnUI();
    }

    void RemoveEggFromPool(Image[] eggPool, string eggSize)
    {
        // Handle removal of eggs from the pool
        switch (eggSize)
        {
            case "small":
                RemoveEggFromArray(eggPool);
                break;
            case "medium":
                RemoveEggFromArray(eggPool);
                break;
            case "large":
                RemoveEggFromArray(eggPool);
                break;
        }
    }

    void RemoveEggFromArray(Image[] eggPool)
    {
        for (int i = 0; i < eggPool.Length; i++)
        {
            if (eggPool[i].enabled)
            {
                eggPool[i].enabled = false; // Disable the egg that was used
                break; // Exit the loop after disabling the first available egg
            }
        }
    }

    void UpdateEggCountUI(int player)
    {
        // Update the UI to show remaining eggs for the player
        // For example, update text or icons showing remaining eggs.
    }
}

