using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : MonoBehaviour
{
    public GameObject settingCanvas; // Assign the SettingCanvas in the Inspector
    public Button settingButton;     // Assign the settingbttn in the Inspector
    public Button closeButton;       // Assign the Close button in the Inspector

    void Start()
    {
        // Ensure the canvas starts hidden
        if (settingCanvas != null)
            settingCanvas.SetActive(false);

        // Add a listener for the setting button click
        if (settingButton != null)
            settingButton.onClick.AddListener(ToggleCanvas);

        // Add a listener for the close button click
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseCanvas);
    }

    void ToggleCanvas()
    {
        // Toggle the canvas visibility
        if (settingCanvas != null)
            settingCanvas.SetActive(!settingCanvas.activeSelf);
    }

    void CloseCanvas()
    {
        // Close the canvas
        if (settingCanvas != null)
            settingCanvas.SetActive(false);
    }
}
