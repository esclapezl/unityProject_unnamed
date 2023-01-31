using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class momentumHitbox : MonoBehaviour
{
    public int distanceFromObject;
    public bool isColliding;
    public GameObject objectTrigger;
    public GameObject objectTriggerComing;
    public Vector3 oldPosition;
    public momentumHitbox h1;
    public GameObject host;
    private string type;

    void Awake()
    {
        if(gameObject.name == "uh1"
        || gameObject.name == "lh1"
        || gameObject.name == "dh1"
        || gameObject.name == "rh1")
        {
            distanceFromObject = 1;

        }
        else
        {
            distanceFromObject = 2;
            h1 = transform.parent.GetComponent<momentumHitbox>();
        }

        if(host.GetComponent<pushable>() != null) //objet poussable
        {
            type = "pushable";
        }
        else if(host.GetComponent<breakableBlockScript>() != null)
        {
            type = "breakableBlock";
        }
        else if(host.GetComponent<buttonScript>() != null)
        {
            type = "button";
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
                    hitAction();   
                }
                
                objectTriggerComing = null;
                
                
            }
        }
    }


    void OnTriggerExit2D (Collider2D hitInfo)
	{
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            
            objectTrigger = null;
            isColliding = false;
            if(distanceFromObject == 2)
            {
                StartCoroutine(h1.forget());
            }
            else //distance == 1
            {
                if(hitInfo.gameObject == objectTriggerComing)
                {
                    objectTriggerComing = null;
                }
                
            }
            
        }
    }

    IEnumerator forget()
    {
        yield return null;
        yield return new WaitForSeconds(0.1f);
        objectTriggerComing = null;
    }

    void hitAction()
    {
        if(type == "pushable") //objet poussable
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

            StartCoroutine(host.GetComponent<pushable>().kick(v));
            oldPosition = new Vector3(0,0,0);
        }
        else if(type == "breakableBlock") // objet cassable
        {
            host.GetComponent<breakableBlockScript>().breakObject();
        }
        else if(type == "button") //boutton
        {
            host.GetComponent<buttonScript>().toggleButton();
        }
    }

}
