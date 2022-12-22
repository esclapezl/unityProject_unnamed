using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridScript : MonoBehaviour
{
    public bool gridState;
    public bool isReversed;

    public Animator gridAnimator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,1); //pour que ls objets passent devant
        reset();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnEnable()
    {
        eventManager.OnReset += reset;
        eventManager.SwitchBlocks += switchBlock;
    }

    void onDisable()
    {
        eventManager.OnReset -= reset;
        eventManager.SwitchBlocks -= switchBlock;
    }

    private void switchBlock()
    {
        gridState = !gridState;
        gridAnimator.SetBool("gridState",gridState);

        if(!this.gridState)
        {
            this.gameObject.tag="closedSwitch";
        }
        else
        {
            this.gameObject.tag="hole";
        }
    }

    private void reset()
    {
        gridAnimator.SetBool("gridState",isReversed);
        gridState = isReversed;
        switch(isReversed)
        {
            case true:
            this.gameObject.tag="hole";
            break;

            case false:
            this.gameObject.tag="closedSwitch";
            break;
        }
    }
}
