using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class eventManager : MonoBehaviour
{
    public delegate void ResetAction();
    public static event ResetAction OnReset;

    public delegate void SwitchAction();
    public static event SwitchAction SwitchBlocks;

    //public delegate void ChangeLevelAction();
    //public static event ChangeLevelAction OnChangeLevel;

    public delegate void RotateBlocksAction();
    public static event RotateBlocksAction OnRotate;

    public levelScript currentLevel;

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
            if(currentLevel.nbRotateLeft > 0)
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
        if(OnRotate != null)
        {
            currentLevel.nbRotateLeft--;
            currentLevel.textNbRotation.text = currentLevel.nbRotateLeft.ToString();
            OnRotate();
        }
    }

    public void restartLevel()
    {
        if(OnReset != null)
        {
            OnReset();
        }
    }

    public void exitLevel()
    {
        SceneManager.LoadScene (1);
    }

    void OnEnable()
    {
        //goalScript.OnGoalTouched += changeLevel;
    }

    void onDisable()
    {
        //goalScript.OnGoalTouched -= changeLevel;
    }

    /*
    private void changeLevel()
    {
        
        gameManager.level++;
        print("level"+gameManager.level);
        if(OnChangeLevel != null)
        {
            OnChangeLevel();
        }
    }
    */
}
