using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerLevel4 : MonoBehaviour
{
    public float WaitSec;
    private int WaitSecInt;
    public TMP_Text text;

    private void FixedUpdate()
    {
        if(WaitSec > 0){
            WaitSec -= Time.fixedDeltaTime;
            WaitSecInt = (int)WaitSec;
            text.text = WaitSecInt.ToString();
        }
        else
        {
            SceneManager.LoadSceneAsync("Level4");
        }
    }
}

