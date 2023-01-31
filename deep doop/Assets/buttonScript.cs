using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public eventManager eM;
    public Animator animator;
    private levelScript level;
    private int polarity = 1;
    // Start is called before the first frame update
    void Start()
    {
        level = transform.parent.GetComponent<levelScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleButton() //activé par la hitbox quand elle detecte un choc
    {
        eM.changePolarity();
    }

    void OnEnable()
    {
        eventManager.OnPolarity += changePolarity;
        eventManager.OnReset += reset;
    }

    void onDisable()
    {
        eventManager.OnPolarity -= changePolarity;
        eventManager.OnReset -= reset;
    }   

    void changePolarity()
    {
        if(eM.levelSelection.currentLevel == level)
        {
            polarity *= -1;
            animator.SetInteger("polarity",polarity);
        }
        
    }

    void reset()
    {
        polarity = 1;
        animator.SetInteger("polarity",polarity);
    }


}
