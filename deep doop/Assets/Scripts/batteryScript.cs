using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryScript : MonoBehaviour
{
    public int actionGiven;
    public int batteryDisplayed;
    public levelScript level;
    private hitboxScript hitbox;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform Child in transform)
        {
            if(Child.name == "hitbox")
            {
                hitbox = Child.GetComponent<hitboxScript>();
            }
        }
        level = transform.parent.gameObject.GetComponent<levelScript>();
        setSprite();
        
        
        
    }

    void setSprite()
    {
        
        if(actionGiven > (float)level.nbCoups * 0.6f)
        {
            batteryDisplayed = 2;
        }
        else if(actionGiven > (float)level.nbCoups  * 0.3f)
        {
            batteryDisplayed = 1;
        }
        else
        {
            batteryDisplayed = 0;
        }
        gameObject.GetComponent<Animator>().SetInteger("batteryDisplayed", batteryDisplayed);
    }

    // Update is called once per frame
    void Update()
    {
        if(hitbox.isColliding
        && hitbox.objectTrigger.tag == "player")
        {
            level.nbCoupsLeft += actionGiven;
            level.ui.setCoups(level.nbCoupsLeft);
            //animation 
            gameObject.SetActive(false);
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
        gameObject.SetActive(true);
        setSprite();
    }
}
