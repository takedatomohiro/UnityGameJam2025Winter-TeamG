using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundSelector : MonoBehaviour
{
    public BackgroundLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectBackground(string backgroundName)
    {
        BackgroundData.selectedBackgroundName = backgroundName;
        Debug.Log("”wŒi‘I‘ð: " + backgroundName);
        loader.UpdateBackground();
    }
}
