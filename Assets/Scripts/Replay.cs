using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
   public void ReplayLevel1()
   {
        SceneManager.LoadSceneAsync("Level1Remember");
   }
   public void ReplayLevel2()
   {
        SceneManager.LoadSceneAsync("Level2Remember");
   }
   public void ReplayLevel3()
   {
        SceneManager.LoadSceneAsync("Level3Remember");
   }
   public void ReplayLevel4()
   {
        SceneManager.LoadSceneAsync("Level4Remember");
   }
}
