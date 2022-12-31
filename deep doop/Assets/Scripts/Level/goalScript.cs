using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalScript : MonoBehaviour
{
    public levelScript level;

    //public delegate void goalAction();
    //public static event goalAction OnGoalTouched;

    private bool activeAnimation = false;
    
    public hitboxScript hitbox;

    public bool touched = false;

    public levelSelectionScript levelSelection;

    // Start is called before the first frame update
    void Awake()
    {
        level = transform.parent.gameObject.GetComponent<levelScript>();
        levelSelection = level.levelSelection;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelSelectionScript.currentLevelNum == level.level && !activeAnimation)
        {
            StartCoroutine(animateGoal());
        }
        else if(levelSelectionScript.currentLevelNum == level.level && touched == true) 
        {
            touched = false;
        }

        
        if(hitbox.isColliding
        && hitbox.objectTrigger != null 
        && hitbox.objectTrigger.tag == "player"
        && touched == false)
        {
            touched = true;
            //this level +2 = prochain level dans l'ensemble des scenes
            if(PlayerPrefs.GetInt("levelAt") < level.level+1)
            {
                PlayerPrefs.SetInt("levelAt",level.level+1);
            }
            levelSelection.ChangeToLevel(level.level+1);
        }
    }

    IEnumerator animateGoal()
    {
        activeAnimation = true;
        int d = 0;
        while(levelSelection.currentLevel.level == level.level)
        {
            d = (d+90)%360;
            transform.rotation = Quaternion.Euler(0f, 0f,d);
            yield return new WaitForSeconds(0.3f);
        }

        activeAnimation = false;
    }
}
