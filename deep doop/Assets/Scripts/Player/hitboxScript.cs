using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{

    public bool isColliding;
    public GameObject objectTrigger;
    
    public BoxCollider2D bc;

    
    void OnTriggerStay2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            isColliding = true;
            objectTrigger = hitInfo.gameObject;
        }
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        isColliding = false;
        objectTrigger = null;
    }
}
