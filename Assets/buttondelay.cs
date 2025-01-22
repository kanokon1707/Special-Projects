using UnityEngine;

public class ButtonDelay : MonoBehaviour
{
    public GameObject button;

    void Start()
    {
        if (button == null)
        {
            Debug.LogError("Button is not assigned!");
            return;
        }

        Invoke("ShowButton", 8f);
    }

    void ShowButton()
    {
        button.SetActive(true);
        Debug.Log("Button is now visible!");
    }
}


