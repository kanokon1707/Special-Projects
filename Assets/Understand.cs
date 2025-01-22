using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Understand : MonoBehaviour
{
    public void UnderstandTutorial(){
        SceneManager.LoadScene("AreYouReady");
    }
}
