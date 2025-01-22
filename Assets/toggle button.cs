using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class togglebutton : MonoBehaviour
{
    public Image onButton;
    public Image offButton;
    
    int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleOn()
{
    index=1;
    onButton.gameObject.SetActive(true);
    offButton.gameObject.SetActive(false);
}
public void ToggleOff()
{
index = 0;
onButton.gameObject.SetActive(false);
offButton.gameObject.SetActive(true);
}
}
