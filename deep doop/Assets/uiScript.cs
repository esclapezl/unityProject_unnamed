using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiScript : MonoBehaviour
{
    public GameObject restartButton;
    // Update is called once per frame
    void Update()
    {
        if(levelSelectionScript.currentLevelNum == 0)
        {
            restartButton.SetActive(false);
        }
        else
        {
            restartButton.SetActive(true);
        }
    }
}
