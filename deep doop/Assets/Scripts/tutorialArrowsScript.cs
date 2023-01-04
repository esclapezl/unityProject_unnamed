using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialArrowsScript : MonoBehaviour
{
    public levelSelectionScript levelSelection;
    SpriteRenderer sp;
    
    public void startTutorial()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        if(levelSelection.currentLevel.level == 1)
        {
            StartCoroutine(tutorialArrows());
        }
    }

    public bool hasMoved = false;
    IEnumerator tutorialArrows()
    {
        yield return new WaitForSeconds(01f);
        if(!hasMoved && levelSelection.currentLevel.level == 1)
        {
            sp.color = new Color (1, 1, 1, 1); 
        }
        while(!hasMoved && levelSelection.currentLevel.level == 1)
        {
            sp.color = new Color (1, 1, 1, 1); 
            yield return new WaitForSeconds(1f);
            if(!hasMoved && levelSelection.currentLevel.level == 1) 
            {
                sp.color = new Color (1, 1, 1, 0); 
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
