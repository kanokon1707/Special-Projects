using UnityEngine;
using UnityEngine.UI;

public class DisplayInput : MonoBehaviour
{
    public Text displayText;

    void Start()
    {
        // Load player data
        Savemaneger.Instance.Load();
        
        // Display the player's name in the Text UI
        displayText.text = Savemaneger.Instance.playerName;
    }
}

