using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelectionScript : MonoBehaviour
{
    //public Button[] lvlButtons;
    
    public GameObject player;
    int levelAt = 2;
    int startPos;
    public GameObject[] miniLevels;

    public levelScript[] levels;
    public levelScript currentLevel;
    public static int currentLevelNum;

    private int lastLevelEntered = 0;

    public bgEffect bgScript;
   

    Camera cam;
   
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentLevel = levels[0];
        Vector3 startPos = miniLevels[lastLevelEntered].transform.position;
        player.transform.position = new Vector3(startPos.x,startPos.y,0);
        levelAt = PlayerPrefs.GetInt("levelAt",2);

        if(currentLevel != null)
        {
            ChangeToLevel(currentLevel.level);
        }
    }

    public void ChangeToLevel(int lvlNumber)
    {
        StartCoroutine(changeLevel(lvlNumber));
    }

    private IEnumerator changeLevel(int lvlNumber)
    {
        lastLevelEntered = currentLevel.level;
        currentLevel = levels[lvlNumber];

        yield return new WaitForSeconds(0.2f);

        cam.transform.position = new Vector3(currentLevel.cameraPos.transform.position.x,currentLevel.cameraPos.transform.position.y,-100);
        player.transform.position =  new Vector3(currentLevel.startingPos.transform.position.x,currentLevel.startingPos.transform.position.y,0);

        currentLevelNum = currentLevel.level;
        currentLevel.startLevel();
        bgScript.createBg();
    }
}

