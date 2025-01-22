using UnityEngine;
using UnityEngine.UI;

public class ObjectSwitcher : MonoBehaviour
{
    // Public references to the default and new GameObjects
    public GameObject[] defaultGameObject;
    public GameObject[] newGameObject;

    // To keep track of the currently selected GameObject
    private GameObject currentDefault;
    private GameObject currentNew;
    
    public Button[] confirmButtons;

    // This will keep track of the selected GameObject index
    private int selectedGameObjectIndex = -1;

    void Start()
    {
        // Ensure the default objects are active, and the new ones are inactive
        for (int i = 0; i < defaultGameObject.Length; i++)
        {
            defaultGameObject[i].SetActive(true);
            newGameObject[i].SetActive(false);
        }

        currentDefault = null;
        currentNew = null;

        // Add listeners to the confirm buttons to confirm the selected object
        for (int i = 0; i < confirmButtons.Length; i++)
        {
            int index = i;  // Capture the current index of the button
            confirmButtons[i].onClick.AddListener(() => OnConfirmButtonClicked(index));  // Pass the index to the click handler
        }
    }

    // This method will handle the confirm button action
    public void OnConfirmButtonClicked(int buttonIndex)  // Accept the index of the clicked confirm button
{
    if (selectedGameObjectIndex >= 0)
    {
        // Declare the selected skin prefab to be assigned to the character's skin
        GameObject selectedSkinPrefab = null;
        Savemaneger.Instance.characterskinselected[selectedGameObjectIndex] = true;
        
        // Check if the selected index is odd or even
        if (selectedGameObjectIndex % 2 != 0)  // Odd index
        {
            selectedSkinPrefab = GameManager.instance.currentCharacter.characterskin2; // Assign characterskin1 for odd index
        }
        else  // Even index
        {
            selectedSkinPrefab = GameManager.instance.currentCharacter.characterskin1; // Assign characterskin2 for even index
        }

        // Add the selected skin to GameManager's currentCharacter.characterskinselected
        GameManager.instance.currentCharacter.characterskinselected = selectedSkinPrefab;
        Savemaneger.Instance.currentSkinPrefab = selectedSkinPrefab;

        // Call SaveManager to set skin ownership based on the selected GameObject
        Savemaneger.Instance.SetSkinOwnership(selectedGameObjectIndex, true);
        Debug.Log("Skin " + selectedGameObjectIndex + " confirmed!");
        
        // Disable the achievement button corresponding to the clicked confirm button index
        AchievementManager.Instance.DisableAchievementButton(buttonIndex);  // Use the passed index to disable the correct achievement button
        Savemaneger.Instance.Save();
    }
    else
    {
        Debug.Log("No GameObject selected!");
    }
}


    // Method to handle switching for the clicked GameObject
    public void OnDefaultObjectClicked(GameObject clickedDefault, GameObject correspondingNew, int index)
    {
        // Update the selectedGameObjectIndex
        selectedGameObjectIndex = index;

        // Reset the previously selected default GameObject
        if (currentDefault != null && currentNew != null)
        {
            currentDefault.SetActive(true);
            currentNew.SetActive(false);
        }

        // Update the clicked GameObject
        clickedDefault.SetActive(false);
        correspondingNew.SetActive(true);

        // Update the current references
        currentDefault = clickedDefault;
        currentNew = correspondingNew;
    }

    // Method to identify the clicked GameObject by its index
    public void OnSpecificDefaultClicked(int index)
    {
        if (index >= 0 && index < defaultGameObject.Length)
        {
            OnDefaultObjectClicked(defaultGameObject[index], newGameObject[index], index);
        }
    }
}
