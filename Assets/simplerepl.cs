using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class simplerepl : MonoBehaviour
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
            spawnedCharacter = Instantiate(GameManager.instance.currentCharacter.characterskinselected, oldPosition, oldRotation);

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
    RectTransform rectTransform = character.GetComponent<RectTransform>();
    if (rectTransform != null)
    {
        rectTransform.sizeDelta = new Vector2(width, height);
        Debug.Log($"Set character size to {width}x{height} using RectTransform.");
    }
    else
    {
        character.transform.localScale = new Vector3(width / 100f, height / 100f, character.transform.localScale.z);
        Debug.Log($"Set character size to {width}x{height} using localScale.");
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
        CanvasRenderer canvasRenderer = character.GetComponent<CanvasRenderer>();
        if (canvasRenderer != null)
        {
            Debug.Log($"CanvasRenderer found on {character.name}. Sorting might need additional handling.");
        }
        else
        {
            Debug.LogWarning($"No Renderer or CanvasRenderer found on {character.name}. Sorting not set.");
        }
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
