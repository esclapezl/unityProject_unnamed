using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgTileScript : MonoBehaviour
{
    private int level;
    void Start()
    {
        level = levelSelectionScript.currentLevelNum;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelSelectionScript.currentLevelNum != level)
        {
            Destroy(gameObject);
        }
    }
}
