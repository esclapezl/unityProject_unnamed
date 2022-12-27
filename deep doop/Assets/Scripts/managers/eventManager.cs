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

    
    public levelSelectionScript levelSelection;
    public uiScript ui;

    

    void Update()
    {
        if(Input.GetButtonDown("switch"))
        {
            if(SwitchBlocks != null)
            {
                SwitchBlocks();
            }
        } 

        if(Input.GetButtonDown("rotate"))
        {
            if(levelSelection.currentLevel.nbRotateLeft > 0)
            {
                rotateBlocks();
            }
            else
            {
                //animation d'impossibilité ?
            }
        } 
    }

    public void rotateBlocks()
    {
        levelSelection.currentLevel.nbRotateLeft--;
        ui.setRotate(true,levelSelection.currentLevel.nbRotateLeft);
        if(OnRotate != null)
        {
            OnRotate();
        }
    }

    public delegate void ResetAction();
    public static event ResetAction OnReset;
    public void restartLevel()
    {
        StartCoroutine(restartLevelCoroutine());
    }

    public restartTransitionScript restartScript;
    public IEnumerator restartLevelCoroutine()
    {
         //animation en avance pour laisser le temps a l'anim de s'effectuer
        restartScript.reset();
        yield return new WaitForSeconds(0.7f); //temps ou l'anim recouvre l'ecran entier
        if(OnReset != null)
        {
            OnReset();
        }
    }

    public exitTransitionScript exitScript;
    public void exitLevel()
    {
        levelSelection.currentLevel = levelSelection.levels[0];
        StartCoroutine(exitLevelCoroutine());
    }

    public IEnumerator exitLevelCoroutine()
    {
        if(levelSelection.levels[0] == levelSelection.currentLevel) //menu principal ou sors du jeu jsp encore
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
            levelSelection.currentLevel = levelSelection.levels[0];
            
        }
        
    }

    public void playerMove()
    {
        levelSelection.currentLevel.nbCoupsLeft--;
        ui.setCoups(levelSelection.currentLevel.nbCoupsLeft);
    }
}
