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
        StartCoroutine(tutorialArrows());
    }

    public bool hasMoved = false;
    IEnumerator tutorialArrows()
    {
        yield return new WaitForSeconds(01f);
        if(!hasMoved)
        {
            sp.color = new Color (1, 1, 1, 1); 
        }
        while(!hasMoved)
        {
            sp.color = new Color (1, 1, 1, 1); 
            yield return new WaitForSeconds(1f);
            if(!hasMoved)
            {
                sp.color = new Color (1, 1, 1, 0); 
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
