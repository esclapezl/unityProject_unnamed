using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class levelScript : MonoBehaviour
{
    [HideInInspector]
    public int level;

    [Space(5)]
    [Header("NbCoups (-1 si le joeur a coups illimité)")]
    public int nbCoups;
    public int nbCoupsLeft;

    [Space(5)]
    [Header("rotate")]
    public bool containsRotates;
    //public int nbRotate;
    //public int nbRotateLeft;

    [Space(5)]
    [Header("switch")]
    public bool containsSwitchs;
    //public int nbSwitch;
    //public int nbSwitchLeft;

    [Space(5)]
    [Header("polarity")]
    public bool containsPolarity;
    //public int nbSwitch;
    //public int nbSwitchLeft;

    [Space(5)]
    [Header("start")]
    public GameObject startingPos;
    public GameObject cameraPos;
    public string objective;

    [Space(10)]
    public levelSelectionScript levelSelection;
    public uiScript ui;


    
    
    
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        while(gameObject.name != "Level_"+level.ToString()
        && gameObject.name != "Level_selection")
        {
            level++;
        }
        startLevel();
    }

    // Update is called once per frame
    public void startLevel()
    {
        //active les fleches de tuto si lvl 1
        if(levelSelection.currentLevel != null
        && levelSelection.currentLevel.level == 1)
        {
            foreach(Transform child in levelSelection.player.transform)
            {
                if(child.name == "tutorialArrows")
                {
                    child.GetComponent<tutorialArrowsScript>().hasMoved = false;
                    child.GetComponent<tutorialArrowsScript>().startTutorial();
                }
            }
        }
        
        if(levelSelection.currentLevel != null
        && levelSelection.currentLevel.level == level)
        {
            PlayerPrefs.SetInt("menuStartPos",level);

            /*
            nbRotateLeft = nbRotate;
            ui.setRotate(containsRotates,nbRotate);
            nbSwitchLeft = nbSwitch;
            ui.setSwitch(containsSwitchs,nbSwitch);
            */
            nbCoupsLeft = nbCoups;
            ui.setCoups(nbCoups);
            ui.setPolarity(containsPolarity,isOn,polarity);

            polarity = 1;
            isOn = false;
            ui.setPolarity(containsPolarity,false,1);
        }
    }

    [HideInInspector] public int polarity = 1;
    bool isOn = false;
    void changePolarity()
    {
        if(levelSelection.currentLevel == this
        && containsPolarity)
        {
            if(!isOn){
                isOn = true;
            }
            else{
                polarity *= -1;
            }

            ui.setPolarity(containsPolarity,isOn,polarity);
            foreach(Transform child in Camera.main.transform)
            {
                if(child.name == "particles")
                {
                    child.GetComponent<particleScript>().changePolarity(polarity);
                }
            }
        }
        
    }

    void OnEnable()
    {
        eventManager.OnReset += startLevel;
        eventManager.OnPolarity += changePolarity;
    }

    void onDisable()
    {
        eventManager.OnReset -= startLevel;
        eventManager.OnPolarity -= changePolarity;
    }   

}
