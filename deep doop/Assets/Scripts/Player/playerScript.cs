using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    public ArrayList tagsPassableObstacle = new ArrayList();

    public bool animationEnded = true;

    // Start is called before the first frame update
    void Start()
    {
        //ajouter les objets que this peut passer
        this.tagsPassableObstacle = gameManager.playerPassableObjects();
        gameManager.setDepth(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        //bouger le perso sur la grille
        if (Input.GetButtonDown("up"))
        { 
            if(movementCondition(upHitbox))
            {
                StartCoroutine(move(new Vector3(0,1,0)));
            }
            //peut se déplacer
            
            
            else    //la hitboxe est en collision
            {
                //animation de blocage
            }
          
        }
        else if(Input.GetButtonDown("down"))
        {
            if(movementCondition(downHitbox))    //peut se déplacer
            {
                StartCoroutine(move(new Vector3(0,-1,0)));
            }
            else
            {

            }
        }
        else if(Input.GetButtonDown("left"))
        {
            if(movementCondition(leftHitbox)) 
            {
                StartCoroutine(move(new Vector3(-1,0,0)));
            }
            else
            {

            }
        }
        else if(Input.GetButtonDown("right"))
        {
            if(movementCondition(rightHitbox)) 
            {
                StartCoroutine(move(new Vector3(1,0,0)));
            }
            else
            {

            }
           
        }
    }

    private bool movementCondition(hitboxScript h)
    {
        return ((!h.isColliding
            || tagsPassableObstacle.Contains(h.objectTrigger.tag))
            && animationEnded)  ;
    }

    IEnumerator move(Vector3 v)
    {
        animationEnded = false;
        // jouer l'animation
        transform.position += v;
        gameManager.setDepth(this.gameObject);
        yield return new WaitForSeconds(0.03f); // durée de l'animation
        animationEnded = true;
    }
}