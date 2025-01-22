using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersBook2 : MonoBehaviour
{
    public void BackPage()
    {
        SceneManager.LoadSceneAsync("characters book");
    }
}
