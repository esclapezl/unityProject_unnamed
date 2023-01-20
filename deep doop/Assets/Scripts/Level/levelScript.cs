using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class levelScript : MonoBehaviour
{
    [HideInInspector]
    public int level;
    public string levelName;

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

    public int levelHeight;
    public int levelWidth;

    public IEnumerator levelOn()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public IEnumerator levelOff()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(Transform child in transform)
        {
            if(child.GetComponent<polarityWholeBlockScript>() == null
            && child.GetComponent<batteryScript>() == null
            && child.GetComponent<pushable>() == null)
            {
                //child.gameObject.SetActive(false);
            }
            
        }
    }
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        level = 0;
        while(gameObject.name != "Level_"+level.ToString()
        && gameObject.name != "Level_selection")
        {
            level++;
        }

        if(levelSelection.currentLevel != this)
        {
            StartCoroutine(levelOff());
        }
        
        
    }

    // Update is called once per frame
    public void startLevel()
    {
        if(levelSelection.currentLevel == this)
        {
            StartCoroutine(levelOn());
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
