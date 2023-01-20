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

    hitboxScript oppositeConcernedHitbox(Vector3 v)
    {
        hitboxScript h;
        if(v == Vector3.up)
        {
            h = downHitbox;
        }
        else if(v == Vector3.down)
        {
            h = upHitbox;
        }
        else if(v == Vector3.right)
        {
            h = leftHitbox;
        }
        else //left
        {
            h = rightHitbox;
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
                    
                    if(ChildChild.name == "leftHitbox"
                    || ChildChild.name == "lh1")
                    {
                        leftHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "rightHitbox"
                    || ChildChild.name == "rh1")
                    {
                        rightHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "upHitbox"
                    || ChildChild.name == "uh1")
                    {
                        upHitbox = ChildChild.GetComponent<hitboxScript>();
                    }
                    else if(ChildChild.name == "downHitbox"
                    || ChildChild.name == "dh1")
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
        return(!h.isColliding 
        || (h.objectTrigger != null && gameManager.passableObjects().Contains(h.objectTrigger.tag))
        || (h.objectTrigger != null && h.objectTrigger.GetComponent<pushable>() != null && !h.objectTrigger.GetComponent<pushable>().canBePushed(v))
        || (h.objectTrigger != null && oppositeConcernedHitbox(v).objectTrigger != null && h.objectTrigger.name == oppositeConcernedHitbox(v).objectTrigger.name && isPolarityBlock(h.objectTrigger.name) ) //cas particulier ou l'obj est coincé dans un meme block polarisé
        );
        


    }

    bool isPolarityBlock(string objectName)
    {
        if(objectName == "walls")
        {
            return false;
        }

        for(int i = 0;i<20;i++)
        {
            if(objectName == "polarityBlock_"+i.ToString())
            {
                return true;
            }
        }
        return false;
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

    public GameObject trail;
    public IEnumerator kick(Vector3 v)
    {
        gameManager.actionFinished = false;
        hitboxScript h = concernedHitbox(v);
        yield return new WaitForSeconds(0.05f); //le temps que le block qui la tapé s'arrete
        while(!h.isColliding)
        {
            Instantiate(trail, transform.position, Quaternion.identity);
            transform.position += v; 
            yield return new WaitForSeconds(0.05f);   
        }     
        gameManager.actionFinished = true;       
    }

    

    
}
