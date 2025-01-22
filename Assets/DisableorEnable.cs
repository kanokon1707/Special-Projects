using UnityEngine;
using UnityEngine.UI;

public class ToggleObject : MonoBehaviour
{
    public GameObject targetObject;
    private bool isActive;

    void Start()
    {
        isActive = targetObject.activeSelf;
    }

    public void Toggle()
    {
        isActive = !isActive;
        targetObject.SetActive(isActive);
    }
}
