using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraWidth : MonoBehaviour
{
    int lvl;
    public levelSelectionScript levelSelection;
    void Start()
    {
        lvl = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelSelection.currentLevel.level != lvl)
        {
            lvl = levelSelection.currentLevel.level;

            if(levelSelection.currentLevel.levelWidth != 0 || levelSelection.currentLevel.levelHeight != 0)
            {
                int levelWidth = levelSelection.currentLevel.levelWidth+1;
                int levelHeight = levelSelection.currentLevel.levelHeight+1;

                //produit en croix
                float cameraWidth = levelWidth*5.49f/20;
                float cameraHeight = levelHeight*5.49f/11;

                if(cameraHeight > cameraWidth)
                {
                    Camera.main.orthographicSize = cameraHeight;
                }
                else
                {
                    Camera.main.orthographicSize = cameraWidth;
                }
            }
            else
            {
                Camera.main.orthographicSize = 5.49f;
            }
        }
    }
}
