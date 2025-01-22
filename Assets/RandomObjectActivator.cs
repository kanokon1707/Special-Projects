using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectActivator : MonoBehaviour
{
    [Tooltip("Array of objects to randomly activate/deactivate.")]
    public GameObject[] objectsToActivate; // Array of objects to activate

    [Tooltip("Number of objects to randomly select and activate.")]
    public int numberOfObjectsToShow = 6; // Number of objects to show

    [Tooltip("Duration (in seconds) for each object's activation/deactivation.")]
    public float winkDuration = 1.0f; // Duration for each wink

    // Static list to store names of randomly selected objects
    public static List<string> selectedObjectNames = new List<string>();

    private void Start()
    {
        // Start the coroutine to handle activation and deactivation
        StartCoroutine(WinkRandomlySelectedObjectsLoop());
    }

    private IEnumerator WinkRandomlySelectedObjectsLoop()
    {
        // Ensure there are enough objects to meet the requested number
        if (objectsToActivate.Length < numberOfObjectsToShow)
        {
            Debug.LogWarning("Not enough objects to activate the requested number.");
            yield break; // Exit the coroutine
        }

        // Prepare a list of indices to randomly select from
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            availableIndices.Add(i);
        }

        // Clear previous selections and randomly select objects
        selectedObjectNames.Clear();
        for (int i = 0; i < numberOfObjectsToShow; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            GameObject selectedObject = objectsToActivate[availableIndices[randomIndex]];
            selectedObjectNames.Add(selectedObject.name); // Add the selected object's name
            availableIndices.RemoveAt(randomIndex); // Remove the selected index to avoid duplicates
        }

        // Infinite loop to handle activation and deactivation
        while (true)
        {
            // Activate all selected objects
            foreach (string objectName in selectedObjectNames)
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    if (obj.name == objectName)
                    {
                        obj.SetActive(true);
                    }
                }
            }

            // Wait for the specified wink duration
            yield return new WaitForSeconds(winkDuration);

            // Deactivate all selected objects
            foreach (string objectName in selectedObjectNames)
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    if (obj.name == objectName)
                    {
                        obj.SetActive(false);
                    }
                }
            }

            // Wait for the wink duration again before restarting the loop
            yield return new WaitForSeconds(winkDuration);
        }
    }
}
