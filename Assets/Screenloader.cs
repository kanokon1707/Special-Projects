using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(String sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
