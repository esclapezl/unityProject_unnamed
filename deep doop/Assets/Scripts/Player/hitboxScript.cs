using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{

    public bool isColliding;
    public GameObject objectTrigger;
    public string tag;
    
    public BoxCollider2D bc;

    
    void OnTriggerStay2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged")
        && (objectTrigger == null))
        {
            isColliding = true;
            objectTrigger = hitInfo.gameObject;
            tag = objectTrigger.tag;
        }
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged")
        && (hitInfo.gameObject.tag == objectTrigger.tag))
        {
            isColliding = false;
            objectTrigger = null;
            tag = null;
        }
        
    }
}
