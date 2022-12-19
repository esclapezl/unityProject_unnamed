using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    public static ArrayList tagsPassableObstacle = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        //ajouter les objets que this peut passer
        tagsPassableObstacle.Add("hole");
        tagsPassableObstacle.Add("crateInHole");
    }

    // Update is called once per frame
    void Update()
    {
        if(!(this.gameObject.tag=="crateInHole"))
        {
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(upHitbox.isColliding 
            && upHitbox.objectTrigger.tag =="player" 
            && (!downHitbox.isColliding 
            || tagsPassableObstacle.Contains(downHitbox.objectTrigger.tag))
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
