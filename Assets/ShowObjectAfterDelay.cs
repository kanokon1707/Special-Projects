using UnityEngine;

public class ShowObjectAfterDelay : MonoBehaviour
{
    public GameObject objectToShow;  // The object you want to show
    private float delayTime = 1.5f;  // Delay in seconds
    private float timer = 0f;        // Timer to track elapsed time

    void Update()
    {
        // Only proceed if the object is not yet shown
        if (objectToShow != null && !objectToShow.activeSelf)
        {
            timer += Time.deltaTime;  // Increment the timer by the time since last frame

            // Check if the delay has passed
            if (timer >= delayTime)
            {
                // Activate the object after the delay
                objectToShow.SetActive(true);
                Debug.Log($"Object shown after {delayTime} seconds.");
            }
        }
    }
}
