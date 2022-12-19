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
                direction = "down";
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
                crateMovement(new Vector3(0,-1,0),downHitbox);
            }
            else if(downHitbox.isColliding 
            && downHitbox.objectTrigger.tag=="player" 
            && (!upHitbox.isColliding 
            || tagsPassableObstacle.Contains(upHitbox.objectTrigger.tag))
            && direction == "up"
            && Input.GetButtonDown("up"))
            {
                crateMovement(new Vector3(0,1,0),rightHitbox);
            }
            else if(leftHitbox.isColliding 
            && leftHitbox.objectTrigger.tag=="player" 
            && (!rightHitbox.isColliding 
            || tagsPassableObstacle.Contains(rightHitbox.objectTrigger.tag))
            && direction == "right"
            && Input.GetButtonDown("right"))
            {
                crateMovement(new Vector3(1,0,0),rightHitbox);
            }
            else if(rightHitbox.isColliding 
            && rightHitbox.objectTrigger.tag=="player" 
            && (!leftHitbox.isColliding 
            || tagsPassableObstacle.Contains(leftHitbox.objectTrigger.tag))
            && direction == "left"
            && Input.GetButtonDown("left"))
            {
                crateMovement(new Vector3(-1,0,0),leftHitbox);
            }
        }
       
    }

    private void crateMovement(Vector3 v, hitboxScript h)
    {
        if(h.objectTrigger != null && h.objectTrigger.tag=="hole")
        {
            h.objectTrigger.tag="crateInHole";
            this.gameObject.tag="crateInHole";
            this.GetComponent<BoxCollider2D> ().enabled = false;
        }
        transform.position += v;
    }
}
