using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushable : MonoBehaviour
{
    
    [HideInInspector] public hitboxScript rightHitbox;
    [HideInInspector] public hitboxScript leftHitbox;
    [HideInInspector] public hitboxScript upHitbox;
    [HideInInspector] public hitboxScript downHitbox;

    hitboxScript concernedHitbox(Vector3 v)
    {
        hitboxScript h;
        if(v == Vector3.up)
        {
            h = upHitbox;
        }
        else if(v == Vector3.down)
        {
            h = downHitbox;
        }
        else if(v == Vector3.right)
        {
            h = rightHitbox;
        }
        else //left
        {
            h = leftHitbox;
        }

        return h;
    }

    void Start()
    {
        foreach(Transform Child in transform)
        {
            if(Child.name == "Hitboxes")
            {
                foreach(Transform ChildChild in Child)
                {
                    if(ChildChild.name == "leftHitbox")
                    {
                        leftHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "rightHitbox")
                    {
                        rightHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "upHitbox")
                    {
                        upHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "downHitbox")
                    {
                        downHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                }
            }
        }
    }
    
    public bool canBePushed(Vector3 v)
    {
        hitboxScript h = concernedHitbox(v);

        //plusieurs cas
        /*
        obj n'as pas de collisions
        obj collisione avec un objet surmontable
        obj collisione avec un objet poussable

        */
        if(!h.isColliding 
        || (h.objectTrigger != null && gameManager.passableObjects().Contains(h.objectTrigger.tag))
        || (h.objectTrigger != null && h.objectTrigger.GetComponent<pushable>() != null && !h.objectTrigger.GetComponent<pushable>().canBePushed(v))
        )
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public void push(Vector3 v)
    {
        hitboxScript h = concernedHitbox(v);
        //pousse l'objet en collision si poussable
        if(h.objectTrigger != null && h.objectTrigger.GetComponent<pushable>() != null && !h.objectTrigger.GetComponent<pushable>().canBePushed(v))
        {
            h.objectTrigger.GetComponent<pushable>().push(v);
        }

        //est poussé
        transform.position += v;
    }

    
}
