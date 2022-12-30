using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polarityHitboxScript : MonoBehaviour
{

    public bool isColliding;
    public GameObject objectTrigger;
    private int parentPolarity;
    
    //public BoxCollider2D boxCollider;

    
    void OnTriggerStay2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged")
        && (objectTrigger == null))
        {
            objectTrigger = hitInfo.gameObject;
            isColliding = true;
        }
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged")
        && (objectTrigger != null)
        && (hitInfo.gameObject.tag == objectTrigger.tag))
        {
            
            objectTrigger = null;
            isColliding = false;
        }
        
    }

    /*
    public polarityHoleBlockScript parent;
    void setWholeCollisison()
    {
        if(gameObject.name == "hu")
        {
            parent.isCollidingUp.Add(isColliding);
        } 
        else if(gameObject.name == "hd")
        {
            parent.isCollidingDown.Add(isColliding);
        } 
        else if(gameObject.name == "hl")
        {
            parent.isCollidingLeft.Add(isColliding);
        } 
        else if(gameObject.name == "hr")
        {
            parent.isCollidingRight.Add(isColliding);
        } 

    }
    */

    
    
}
