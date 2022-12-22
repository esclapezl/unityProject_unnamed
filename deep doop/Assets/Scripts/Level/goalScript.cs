using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript : MonoBehaviour
{
    public int level;
    public delegate void goalAction();
    private bool activeAnimation = false;
    //public static event goalAction OnGoalTouched;


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
