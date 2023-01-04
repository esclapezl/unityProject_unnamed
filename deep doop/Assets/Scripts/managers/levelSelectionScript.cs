using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelectionScript : MonoBehaviour
{
    //public Button[] lvlButtons;
    
    public GameObject player;
    int levelAt = 2; //player progression
    int startPos;
    public Transform miniLevels;
    [HideInInspector] public List<Transform> miniLevelList;

    public Transform levels;
    [HideInInspector] public List<levelScript> levelList;
    public levelScript currentLevel;
    public static int currentLevelNum;

    private int lastLevelEntered = 0;

    [HideInInspector] public bgEffect bgScript;
   

    Camera cam;
   
    // Start is called before the first frame update

    void Awake()
    {
        //selectionne automatiquement tous les levels dans l'ordre dans lequel ils ont été mit dans l'éditeur je crois
        levelList = new List<levelScript>();
        foreach(Transform level in levels)
        {
            levelList.Add(level.GetComponent<levelScript>());
        }

        miniLevelList = new List<Transform>();
        foreach(Transform levelObject in miniLevels)
        {
            miniLevelList.Add(levelObject);
        }
    }

    void Start()
    {
        cam = Camera.main;
        if(currentLevel == null)
        {
            currentLevel = levelList[0];
        }

        if(currentLevel == levelList[0])
        {
            Vector3 startPos = miniLevelList[lastLevelEntered].transform.position;
            player.transform.position = new Vector3(startPos.x,startPos.y,0);
        }
        
        levelAt = PlayerPrefs.GetInt("levelAt",2);
        
        ChangeToLevel(currentLevel.level);
        
    }

    public void ChangeToLevel(int lvlNumber)
    {
        StartCoroutine(changeLevel(lvlNumber));
    }

    private IEnumerator changeLevel(int lvlNumber)
    {
        lastLevelEntered = currentLevel.level;
        StartCoroutine(currentLevel.levelOff());
        

        currentLevel = levelList[lvlNumber];
        cam.transform.position = new Vector3(currentLevel.cameraPos.transform.position.x,currentLevel.cameraPos.transform.position.y,-100);
        yield return new WaitForSeconds(0.05f);
        player.transform.position =  new Vector3(currentLevel.startingPos.transform.position.x,currentLevel.startingPos.transform.position.y,0);

        //player.madeActions = Vector3[nbCoups-1];

        currentLevelNum = currentLevel.level;
        currentLevel.startLevel();
        bgScript.createBg();
    }
}

