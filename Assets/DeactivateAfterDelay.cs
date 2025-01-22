using UnityEngine;

public class DeactivateAfterDelay : MonoBehaviour
{
    [SerializeField] float delay = 3f;  // Time in seconds before deactivating the object

    void Start()
    {
        // Call DeactivateObject after the specified delay
        Invoke("DeactivateObject", delay);
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false);  // Deactivate the GameObject
    }
}
