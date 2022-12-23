using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript : MonoBehaviour
{
    public int level;

    public delegate void goalAction();
    public static event goalAction OnGoalTouched;

    private bool activeAnimation = false;
    
    public hitboxScript hitbox;

    bool touched = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.level == level && !activeAnimation)
        {
            StartCoroutine(animateGoal());
        }

        // ATTENTION LA VARIABLE TOUCHED NEST PAS ENCORE REINITIALISEE, LE GOAL NE PEUT ETRE TOUCHE QUUNE FOIS
        if(hitbox.isColliding
        && hitbox.objectTrigger != null 
        && hitbox.objectTrigger.tag == "player"
        && touched == false)
        {
            touched = true;
            OnGoalTouched();
        }
    }

    IEnumerator animateGoal()
    {
        activeAnimation = true;

        int d = 0;
        while(gameManager.level == level)
        {
            d = (d+90)%360;
            transform.rotation = Quaternion.Euler(0f, 0f,d);
            yield return new WaitForSeconds(0.3f);
        }

        activeAnimation = false;
    }
}
