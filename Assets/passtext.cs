using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
   

    public void OnSubmit()
    {
        

        // Load the next scene
        SceneManager.LoadScene("Home");
    }
}
