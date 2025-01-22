using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement; // Add this for scene management

public class Timers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;
    [SerializeField] string nextSceneName; // Scene name to load after time is up
    
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int second = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0}", second);

        // Check if time has run out, then load the next scene
        if (remainingTime == 0)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Make sure a valid scene name is provided before trying to load it
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }
}

