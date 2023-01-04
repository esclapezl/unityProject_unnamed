using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickableHitbox : MonoBehaviour
{
    public int distanceFromObject;
    public bool isColliding;
    public GameObject objectTrigger;
    public GameObject objectTriggerComing;
    public Vector3 oldPosition;
    public kickableHitbox h1;
    public pushable host;

    void Awake()
    {
        if(gameObject.name == "uh1"
        || gameObject.name == "lh1"
        || gameObject.name == "dh1"
        || gameObject.name == "rh1")
        {
            distanceFromObject = 1;
            host = transform.parent.transform.parent.GetComponent<pushable>();
        }
        else
        {
            distanceFromObject = 2;
            h1 = transform.parent.GetComponent<kickableHitbox>();
        }
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            objectTrigger = hitInfo.gameObject;
            isColliding = true;

            if(distanceFromObject == 2)
            {
                h1.objectTriggerComing = objectTrigger;
                h1.oldPosition = objectTrigger.transform.position;
            }
            else if(distanceFromObject == 1)
            {
                if(objectTriggerComing != null
                && objectTriggerComing == objectTrigger
                && objectTrigger.transform.position != oldPosition) //l'objet va taper 
                {
                    Vector3 v;
                    if(gameObject.name == "uh1")
                    {
                        v = new Vector3(0,-1,0);
                    }
                    else if(gameObject.name == "rh1")
                    {
                        v = new Vector3(-1,0,0);
                    }
                    else if(gameObject.name == "lh1")
                    {
                        v = new Vector3(1,0,0);
                    }
                    else //dh1
                    {
                        v = new Vector3(0,1,0);
                    }
                    StartCoroutine(host.kick(v));
                    
                }
                
                objectTriggerComing = null;
                oldPosition = new Vector3(0,0,0);
                
            }
        }
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            objectTrigger = null;
            isColliding = false;
            
        }
    }

}
