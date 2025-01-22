using UnityEngine;
using UnityEngine.UI;

public class ResultScreenHandler : MonoBehaviour
{
    [SerializeField] private Image player1Image; // Reference to the first player's image
    [SerializeField] private Image player2Image; // Reference to the second player's image

    void Start()
    {
        // Retrieve the winner from PlayerPrefs
        string winner = PlayerPrefs.GetString("winner", "No result");

        // Log the winner (for debugging purposes)
        Debug.Log("The winner is: " + winner);

        // Display the appropriate images based on the winner
        if (winner == "First Roll" || winner == "Tie")
        {
            // Show the first player's image
            player1Image.gameObject.SetActive(true);
            player2Image.gameObject.SetActive(false);

            // Player 1 will start
            PlayerPrefs.SetString("firstPlayer", "Player 1");
        }
        else if (winner == "Second Roll")
        {
            // Show the second player's image
            player1Image.gameObject.SetActive(false);
            player2Image.gameObject.SetActive(true);

            // Player 2 will start
            PlayerPrefs.SetString("firstPlayer", "Player 2");
        }
        else
        {
            // In case of an invalid result, display nothing (or handle as needed)
            player1Image.gameObject.SetActive(false);
            player2Image.gameObject.SetActive(false);
        }
    }
}
