using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(this.gameObject.tag=="crateInHole"))
        {
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(upHitbox.isColliding 
            && upHitbox.objectTrigger.tag =="player" 
            && (!downHitbox.isColliding || downHitbox.objectTrigger.tag == "hole")
            && Input.GetButtonDown("down"))
            {
                if(downHitbox.objectTrigger != null && downHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                }
                transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            }
            else if(downHitbox.isColliding 
            && downHitbox.objectTrigger.tag=="player" 
            && (!upHitbox.isColliding || upHitbox.objectTrigger.tag == "hole")
            && Input.GetButtonDown("up"))
            {
                if(upHitbox.objectTrigger != null && upHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                }
                transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            }
            else if(leftHitbox.isColliding 
            && leftHitbox.objectTrigger.tag=="player" 
            && (!rightHitbox.isColliding || rightHitbox.objectTrigger.tag == "hole")
            && Input.GetButtonDown("right"))
            {
                if(rightHitbox.objectTrigger != null && rightHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                }
                transform.position = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
            }
            else if(rightHitbox.isColliding 
            && rightHitbox.objectTrigger.tag=="player" 
            && (!leftHitbox.isColliding || leftHitbox.objectTrigger.tag == "hole")
            && Input.GetButtonDown("left"))
            {
                if(leftHitbox.objectTrigger != null && leftHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                }
                transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
            }
        }
       
    }
}
