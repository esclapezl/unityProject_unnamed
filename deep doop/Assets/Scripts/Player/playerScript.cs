using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
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
        //bouger le perso sur la grille
        if (Input.GetButtonDown("up"))
        { 
            if(!upHitbox.isColliding
            || (upHitbox.objectTrigger != null && upHitbox.objectTrigger.tag == "crateInHole"))    
            {   //peut se déplacer
                transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            }
            else    //la hitboxe est en collision
            {
                //animation de blocage
            }
          
        }
        else if(Input.GetButtonDown("down"))
        {
            if(!downHitbox.isColliding
            || (downHitbox.objectTrigger != null && downHitbox.objectTrigger.tag == "crateInHole"))    //peut se déplacer
            {
                transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            }
            else
            {

            }
        }
        else if(Input.GetButtonDown("left"))
        {
            if(!leftHitbox.isColliding
            || (leftHitbox.objectTrigger != null && leftHitbox.objectTrigger.tag == "crateInHole")) 
            {
                transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
            }
            else
            {

            }
        }
        else if(Input.GetButtonDown("right"))
        {
            if(!rightHitbox.isColliding
            || (rightHitbox.objectTrigger != null && rightHitbox.objectTrigger.tag == "crateInHole")) 
            {
                transform.position = new Vector3(transform.position.x+1, transform.position.y, transform.position.z);
            }
            else
            {

            }
           
        }
    }
}