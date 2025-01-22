using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moneycounter : MonoBehaviour
{
    private Text text;
    private void Start(){
        
        UpdateMoneyText();
    }
    private void Awake()
    {
        text = GetComponent<Text>();

        // Initialize the text to display the current money value when the game starts
        UpdateMoneyText();
    }

    private void Update()
    {
        // You can still use key presses or other events to change the money value
        
         

        // Always update the text in real-time (every frame)
        UpdateMoneyText();
    }

    // Method to update the text UI with the current money value
    private void UpdateMoneyText()
    {
        text.text = Savemaneger.Instance.money.ToString();
    }
}
