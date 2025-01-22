using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   public void NextToLevel2()
   {
        SceneManager.LoadSceneAsync("Level2Remember");
   }
   public void NextToLevel3()
   {
        SceneManager.LoadSceneAsync("Level3Remember");
   }
   public void NextToLevel4()
   {
        SceneManager.LoadSceneAsync("Level4Remember");
   }
}
