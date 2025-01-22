using UnityEngine;
using UnityEngine.UI;  // Required for UI components

public class HideObjectOnClick : MonoBehaviour
{
    public GameObject objectToHide;  // The object you want to hide
    public Button hideButton;        // The button you will click to hide the object

    void Start()
    {
        // Make sure to add a listener to the button to call the HideObject function when clicked
        hideButton.onClick.AddListener(HideObject);
    }

    void HideObject()
    {
        // Disable the object (hide it)
        objectToHide.SetActive(false);
    }
}

