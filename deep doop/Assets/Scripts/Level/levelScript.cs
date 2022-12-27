using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class levelScript : MonoBehaviour
{
    [Header("n°")]
    public int level;

    [Space(10)]
    [Header("Level info")]

    [Space(5)]
    [Header("NbCoups (-1 si le joeur a coups illimité)")]
    public int nbCoups;
    public int nbCoupsLeft;

    [Space(5)]
    [Header("rotate")]
    public bool containsRotates;
    public int nbRotate;
    public int nbRotateLeft;

    [Space(5)]
    [Header("switch")]
    public bool containsSwitchs;
    public int nbSwitch;
    public int nbSwitchLeft;

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
        startLevel();
    }

    // Update is called once per frame
    public void startLevel()
    {
        if(levelSelection.currentLevel != null
        && levelSelection.currentLevel.level == level)
        {
            PlayerPrefs.SetInt("menuStartPos",level);
            nbRotateLeft = nbRotate;
            ui.setRotate(containsRotates,nbRotate);
            nbSwitchLeft = nbSwitch;
            ui.setSwitch(containsSwitchs,nbSwitch);
            nbCoupsLeft = nbCoups;
            ui.setCoups(nbCoups);
        }
    }

    void OnEnable()
    {
        eventManager.OnReset += startLevel;
    }

    void onDisable()
    {
        eventManager.OnReset -= startLevel;
    }   
}
