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

    private ArrayList tagsPassableObstacle = new ArrayList();
    

    // Start is called before the first frame update
    void Start()
    {
        setDirection();
        this.tagsPassableObstacle = gameManager.passableObjects();
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

        if(Input.GetButtonDown("rotate"))
        {
            rotate("cw");
        }    
    
        setDirection();
        if(!(this.gameObject.tag=="crateInHole"))
        {
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(Input.GetButtonDown("down")
            && movementCondition(downHitbox,upHitbox,"down"))
            {
                crateMovement(new Vector3(0,-1,0),downHitbox);
            }
            else if(Input.GetButtonDown("up")
            && movementCondition(upHitbox,downHitbox,"up"))
            {
                crateMovement(new Vector3(0,1,0),upHitbox);
            }
            else if(Input.GetButtonDown("right")
            && movementCondition(rightHitbox,leftHitbox,"right"))
            {
                crateMovement(new Vector3(1,0,0),rightHitbox);
            }
            else if(Input.GetButtonDown("left")
            && movementCondition(leftHitbox,rightHitbox,"left"))
            {
                crateMovement(new Vector3(-1,0,0),leftHitbox);
            }
        }
    }

    private bool movementCondition(hitboxScript h, hitboxScript oppositeH, string d)
    {
        return (
        oppositeH.isColliding 
        && oppositeH.objectTrigger.tag =="player" 
        && (!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag))
        && direction == d);
    }


    private void crateMovement(Vector3 v, hitboxScript h)
    {
        if(h.objectTrigger != null && h.objectTrigger.tag=="hole")
        {
            this.GetComponent<SpriteRenderer>().color *= 0.7f;
            Color tmp = this.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            this.GetComponent<SpriteRenderer>().color = tmp;

            transform.position += new Vector3(0,0,1);
            h.objectTrigger.tag="crateInHole";
            this.gameObject.tag="crateInHole";
            this.GetComponent<BoxCollider2D> ().enabled = false;
        }
        transform.position += v;
        gameManager.setDepth(this.gameObject);
    }

    private void rotate(string sens)
    {
        List<string> rotation = new List<string>();
        rotation.Add("down");
        rotation.Add("left");
        rotation.Add("up");
        rotation.Add("right");
        int currentDirection = rotation.IndexOf(direction);
        if(sens == "cw") //clockWise
        {
            currentDirection++;
            if(currentDirection == 4)
            {
                currentDirection = 0;
            }
        }
        if(sens == "ccw") //counter clockWise
        {
            currentDirection--;
            if(currentDirection == -1)
            {
                currentDirection = 3;
            }
        }
        direction = rotation[currentDirection];
        
        


    }
}
