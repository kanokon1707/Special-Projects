using UnityEngine;
using UnityEngine.UI; // Required for UI buttons

public class SimpleReplace : MonoBehaviour
{
    public GameObject gameObject1;   // The object to replace
    private GameObject spawnedCharacter;

    // References to UI buttons
    public Button showButton;
    public Button confirmButton;
    public Button cancelButton;

    private void Start()
    {
        // Load SaveManager data
        Savemaneger.Instance.Load();
        int currentCharacterID = Savemaneger.Instance.currentCharacterID;

        // Assign current character in GameManager
        GameManager.instance.currentCharacter = GameManager.instance.characters[currentCharacterID];
        Debug.Log($"Spawning character with ID: {currentCharacterID}");

        // Validate prefab before proceeding
        if (GameManager.instance.currentCharacter.prefab == null)
        {
            Debug.LogError("Current character prefab is null! Cannot spawn character.");
            return;
        }

        if (gameObject1 != null)
        {
            // Save the position and rotation of the object to be replaced
            Vector3 oldPosition = gameObject1.transform.position;
            Quaternion oldRotation = gameObject1.transform.rotation;

            // Destroy the old object
            Destroy(gameObject1);

            // Instantiate the new prefab at the same position and rotation
            spawnedCharacter = Instantiate(GameManager.instance.currentCharacter.prefab, oldPosition, oldRotation);

            // Ensure the spawned character is correctly placed in the hierarchy
            spawnedCharacter.transform.SetParent(null, true); // Set parent to null to detach from Canvas

            // Set the size of the spawned character to 160x170
            SetCharacterSize(spawnedCharacter, 130f, 130f);

            // Set sorting layer and order in layer
            SetSortingLayer(spawnedCharacter, "Top", 300);

            // Initially hide the character
            spawnedCharacter.SetActive(false);

            // Debugging parent and sorting layer information
            Debug.Log($"Spawned character: {spawnedCharacter.name}, Parent: {spawnedCharacter.transform.parent?.name}");
        }

        // Assign button listeners
        if (showButton != null)
            showButton.onClick.AddListener(ShowCharacter);

        if (confirmButton != null)
            confirmButton.onClick.AddListener(HideCharacter);

        if (cancelButton != null)
            cancelButton.onClick.AddListener(HideCharacter);
    }

    private void SetCharacterSize(GameObject character, float width, float height)
    {
        if (character != null)
        {
            // Assuming the character's scale starts as 1,1,1
            character.transform.localScale = new Vector3(width / 100f, height / 100f, character.transform.localScale.z);
            Debug.Log($"Set character size to {width}x{height}");
        }
    }

    private void SetSortingLayer(GameObject character, string sortingLayer, int orderInLayer)
    {
        Renderer renderer = character.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = sortingLayer;
            renderer.sortingOrder = orderInLayer;
            Debug.Log($"Set sorting layer: {sortingLayer}, order: {orderInLayer} for {character.name}");
        }
        else
        {
            Debug.LogWarning($"No Renderer found on {character.name}. Sorting layer and order not set.");
        }
    }

    // Method to show the spawned character
    private void ShowCharacter()
    {
        if (spawnedCharacter != null)
        {
            spawnedCharacter.SetActive(true);
            Debug.Log("Character is now visible.");
        }
        else
        {
            Debug.LogWarning("No character to show!");
        }
    }

    // Method to hide the spawned character
    private void HideCharacter()
    {
        if (spawnedCharacter != null)
        {
            spawnedCharacter.SetActive(false);
            Debug.Log("Character is now hidden.");
        }
        else
        {
            Debug.LogWarning("No character to hide!");
        }
    }
}
