using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersBook : MonoBehaviour
{
    public void NextPage()
    {
        SceneManager.LoadSceneAsync("characters book pg2");
    }

}