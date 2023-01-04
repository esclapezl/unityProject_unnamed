using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{

    public bool isColliding;
    public GameObject objectTrigger;

    List<GameObject> allCollsions;
    
    
    //public BoxCollider2D boxCollider;

    void Start()
    {
        allCollsions = new List<GameObject>();
    }

    
    void OnTriggerEnter2D (Collider2D hitInfo)
	{
        
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            allCollsions.Add(hitInfo.gameObject);
            isColliding = true;

            if(objectTrigger == null 
            || objectPriority(hitInfo.gameObject) < objectPriority(objectTrigger))
            {
                objectTrigger = hitInfo.gameObject;
            }
        }
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        
        if(!(hitInfo.gameObject.tag == "Untagged"))
        {
            allCollsions.Remove(hitInfo.gameObject);
            if(allCollsions.Count == 0)
            {
                isColliding = false;
                objectTrigger = null;
            }
            else if(objectTrigger == hitInfo.gameObject)
            {
                GameObject max = allCollsions[0];
                foreach(GameObject g in allCollsions)
                {
                    if(objectPriority(g) < objectPriority(max))
                    {
                        max = g;
                    }
                }
                objectTrigger = max;
            }
        }
        
    }

    int objectPriority(GameObject g) // renvois la priorité de l'objet, plus c'est bas plus c'est prioritaire
    {
        List<string> priorityList = new List<string>();
        priorityList.Add("player");
        priorityList.Add("crate");
        priorityList.Add("wall");
        priorityList.Add("ice");

        int i =0;
        foreach(string s in priorityList)
        {
            if(g.tag == s)
            {
                return i;
            }
            i++;
        }
        return i;
    }

    
}
