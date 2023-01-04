using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class eventManager : MonoBehaviour
{
    

    public delegate void SwitchAction();
    public static event SwitchAction SwitchBlocks;

    public delegate void RotateBlocksAction();
    public static event RotateBlocksAction OnRotate;

    public delegate void PolarityAction();
    public static event PolarityAction OnPolarity;
    
    public levelSelectionScript levelSelection;
    public uiScript ui;

    

    void Update()
    {
        if(Input.GetButtonDown("switch"))
        {
            if(levelSelection.currentLevel.containsSwitchs
            && levelSelection.currentLevel.nbCoupsLeft != 0
            && SwitchBlocks != null)
            {
                SwitchBlocks();
                playerMove();
            }
            else
            {

            }
            
        } 

        if(Input.GetButtonDown("rotate"))
        {
            if(levelSelection.currentLevel.containsRotates
            && levelSelection.currentLevel.nbCoupsLeft != 0
            && OnRotate != null)
            {
                OnRotate();
                playerMove();
            }
            else
            {
                //animation d'impossibilité ?
            }
        } 

        if(Input.GetButtonDown("changePolarity"))
        {
            if(levelSelection.currentLevel.containsPolarity
            && levelSelection.currentLevel.nbCoupsLeft != 0
            && OnPolarity != null
            && gameManager.actionFinished)
            {
                OnPolarity();
                playerMove();
            }
        }
        else
        {

        }

    }

    /*
    public void rotateBlocks()
    {
        levelSelection.currentLevel.nbRotateLeft--;
        //ui.setRotate(true,levelSelection.currentLevel.nbRotateLeft);
        if(OnRotate != null)
        {
            OnRotate();
        }
    }
    */

    public delegate void ResetAction();
    public static event ResetAction OnReset;
    public void restartLevel()
    {
        if(gameManager.actionFinished)
        {
            StartCoroutine(restartLevelCoroutine());
        }
        
    }

    public restartTransitionScript restartScript;
    public IEnumerator restartLevelCoroutine()
    {
        gameManager.actionFinished = false;
         //animation en avance pour laisser le temps a l'anim de s'effectuer
        restartScript.reset();
        yield return new WaitForSeconds(0.7f); //temps ou l'anim recouvre l'ecran entier
        if(OnReset != null)
        {
            OnReset();
        }
        gameManager.actionFinished = true;
    }

    public exitTransitionScript exitScript;
    public void exitLevel()
    {
        if(gameManager.actionFinished)
        {
            levelSelection.currentLevel = levelSelection.levelList[0];
            StartCoroutine(exitLevelCoroutine());
        }
    }

    public IEnumerator exitLevelCoroutine()
    {
        gameManager.actionFinished = false;
        if(levelSelection.levelList[0] == levelSelection.currentLevel) //menu principal ou sors du jeu jsp encore
        {
            exitScript.exit();
            yield return new WaitForSeconds(0.65f); //temps ou l'anim recouvre l'ecran entier
            // direction vers menu principal
        }
        else
        {
            exitScript.exit();
            yield return new WaitForSeconds(0.65f); //temps ou l'anim recouvre l'ecran entier
            levelSelection.ChangeToLevel(0);
            levelSelection.currentLevel = levelSelection.levelList[0];
        }
        gameManager.actionFinished = true;
        
    }

    public void playerMove()
    {
        levelSelection.currentLevel.nbCoupsLeft--;
        ui.setCoups(levelSelection.currentLevel.nbCoupsLeft);
    }
}
