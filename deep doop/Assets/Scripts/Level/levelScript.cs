using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelScript : MonoBehaviour
{
    [Header("n°")]
    public int level;

    [Space(10)]
    [Header("Level info")]

    [Space(5)]
    [Header("NbCoups")]
    public Text textNbCoups;
    public int nbCoups;
    public int nbCoupsLeft;


    [Space(5)]
    [Header("rotate")]
    public bool containsRotates;
    public GameObject rotateIcon;
    public Text textNbRotation;
    public int nbRotate;
    public int nbRotateLeft;

    [Space(5)]
    [Header("switch")]
    public bool containsSwitchs;
    public GameObject switchIcon;
    public Text textNbSwitch;
    public int nbSwitch;
    public int nbSwitchLeft;

    [Space(5)]
    [Header("start")]
    public GameObject startingPos;
    public GameObject cameraPos;
    public string objective;


    [Space(10)]
    public levelSelectionScript levelSelection;

    
    
    // Start is called before the first frame update
    void Start()
    {
        if(levelSelection.currentLevel != null
        && levelSelection.currentLevel.level == level)
        {
            startLevel();
        }
    }

    // Update is called once per frame
    public void startLevel()
    {
        PlayerPrefs.SetInt("menuStartPos",level);

            nbRotateLeft = nbRotate;
            if(containsRotates)
            {
                rotateIcon.SetActive(true);
                textNbRotation.enabled = true;
                textNbRotation.text = nbRotateLeft.ToString();
            }
            else
            {
                rotateIcon.SetActive(false);
                textNbRotation.enabled = false;
            }

            nbSwitchLeft = nbSwitch;
            if(containsSwitchs)
            {
                switchIcon.SetActive(true);
                textNbSwitch.enabled = true;
                textNbSwitch.text = nbSwitchLeft.ToString();
            }
            else
            {
                switchIcon.SetActive(false);
                textNbSwitch.enabled = false;
            }

            nbCoupsLeft = nbCoups;
            if(nbCoups <= 0)
            {
                print("true "+ level +" ");
                textNbCoups.enabled = true;
                textNbCoups.text = nbCoupsLeft.ToString();
            }
            else
            {
                textNbCoups.enabled = false;
            }
    }

    void OnEnable()
    {
        eventManager.OnReset += reset;
    }

    void onDisable()
    {
        eventManager.OnReset -= reset;
    }   

    private void reset()
    {
        if(levelSelectionScript.currentLevelNum==level)
        {
            nbRotateLeft = nbRotate;
            if(textNbRotation != null)
            {
                textNbRotation.text = nbRotateLeft.ToString();
            }

            nbSwitchLeft = nbSwitch;
            if(textNbSwitch != null)
            {
                textNbSwitch.text = nbSwitchLeft.ToString();
            }

            nbCoupsLeft = nbCoups;
            if(textNbCoups != null)
            {
                textNbCoups.text = nbCoupsLeft.ToString();
            }
            
        }
        
    }
}
