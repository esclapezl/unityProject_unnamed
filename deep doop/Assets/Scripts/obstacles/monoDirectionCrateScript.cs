using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoDirectionCrateScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    public Transform directionIndicator;
    public string direction;
    public int directionValue;

    public static ArrayList tagsPassableObstacle = new ArrayList();
    

    // Start is called before the first frame update
    void Start()
    {
        setDirection();
        tagsPassableObstacle.Add("hole");
        tagsPassableObstacle.Add("crateInHole");
    }

    void setDirection()
    {
        switch(direction)
        {
            case "up":
                directionValue = 180;
                break;
        
            case "down":
                directionValue = 0;
                break;

            case "left":
                directionValue = 270;
                break;

            case "right":
                directionValue = 90;
                break;

            default:
                directionValue = 0;
                break;
        }
        directionIndicator.rotation = Quaternion.Euler(0f, 0f,directionValue);
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        if(!(this.gameObject.tag=="crateInHole"))
        {
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(upHitbox.isColliding 
            && upHitbox.objectTrigger.tag =="player" 
            && (!downHitbox.isColliding 
            || tagsPassableObstacle.Contains(downHitbox.objectTrigger.tag))
            && direction == "down"
            && Input.GetButtonDown("down"))
            {
                if(downHitbox.objectTrigger != null && downHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                    this.GetComponent<BoxCollider2D> ().enabled = false;

                }
                transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            }
            else if(downHitbox.isColliding 
            && downHitbox.objectTrigger.tag=="player" 
            && (!upHitbox.isColliding 
            || tagsPassableObstacle.Contains(upHitbox.objectTrigger.tag))
            && direction == "up"
            && Input.GetButtonDown("up"))
            {
                if(upHitbox.objectTrigger != null && upHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                    this.GetComponent<BoxCollider2D> ().enabled = false;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            }
            else if(leftHitbox.isColliding 
            && leftHitbox.objectTrigger.tag=="player" 
            && (!rightHitbox.isColliding 
            || tagsPassableObstacle.Contains(rightHitbox.objectTrigger.tag))
            && direction == "right"
            && Input.GetButtonDown("right"))
            {
                if(rightHitbox.objectTrigger != null && rightHitbox.objectTrigger.tag=="hole")
                {
                    rightHitbox.objectTrigger.tag="crateInHole";
                    this.gameObject.tag="crateInHole";
                    this.GetComponent<BoxCollider2D> ().enabled = false;
                }
                transform.position = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
            }
            else if(rightHitbox.isColliding 
            && rightHitbox.objectTrigger.tag=="player" 
            && (!leftHitbox.isColliding 
            || tagsPassableObstacle.Contains(leftHitbox.objectTrigger.tag))
            && direction == "left"
            && Input.GetButtonDown("left"))
            {
                if(leftHitbox.objectTrigger != null && leftHitbox.objectTrigger.tag=="hole")
                {
                    this.gameObject.tag="crateInHole";
                    this.GetComponent<BoxCollider2D> ().enabled = false;
                }
                transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
            }
        }
       
    }
}
