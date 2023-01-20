using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    private ArrayList tagsPassableObstacle = new ArrayList();

    //facultatif 
    public monoDirectionScript mono;
    public slidingScript slideScript;


    //facultatif

    // Start is called before the first frame update
    void Start()
    {
        //ajouter les objets que this peut passer
        startingPos = transform.position;
        this.tagsPassableObstacle = gameManager.passableObjects();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if(!(this.gameObject.tag=="crateInHole"))
        {
            
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(Input.GetButtonDown("down")
            && movementCondition(downHitbox,upHitbox)
            && (mono == null || mono.movementCondition("down")) //si script mono setup, check les conditions du mono en plus
            ) 
            {
                if(slideScript != null)
                {
                    slideScript.slide(new Vector3(0,-1,0),downHitbox,tagsPassableObstacle);
                }
                else
                {
                    crateMovement(new Vector3(0,-1,0),downHitbox);
                }
                
            }
            else if(Input.GetButtonDown("up")
            && movementCondition(upHitbox,downHitbox)
            && (mono == null || mono.movementCondition("up")))
            {
                if(slideScript != null)
                {
                    slideScript.slide(new Vector3(0,1,0),upHitbox,tagsPassableObstacle);
                }
                else
                {
                    crateMovement(new Vector3(0,1,0),upHitbox);
                }
            }
            else if(Input.GetButtonDown("right")
            && movementCondition(rightHitbox,leftHitbox)
            && (mono == null || mono.movementCondition("right")))
            {
                if(slideScript != null)
                {
                    slideScript.slide(new Vector3(1,0,0),rightHitbox,tagsPassableObstacle);
                }
                else
                {
                    crateMovement(new Vector3(1,0,0),rightHitbox);
                }
            }
            else if(Input.GetButtonDown("left")
            && movementCondition(leftHitbox,rightHitbox)
            && (mono == null || mono.movementCondition("left")))
            {
                if(slideScript != null)
                {
                    slideScript.slide(new Vector3(-1,0,0),leftHitbox,tagsPassableObstacle);
                }
                else
                {
                    crateMovement(new Vector3(-1,0,0),leftHitbox);
                }
            }
        }
    }
    

    private bool movementCondition(hitboxScript h, hitboxScript oppositeH)
    {
        return (
        oppositeH.isColliding 
        && oppositeH.objectTrigger.tag =="player"
        && (!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag)));
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
            transform.position += new Vector3(0,0,1);
        }
        transform.position += v;

        gameManager.setDepth(this.gameObject);
        if((this.gameObject.tag=="crateInHole"))
        {
            transform.position += new Vector3(0,0,1);
        } 
    }
    */

    void OnEnable()
    {
        eventManager.OnReset += reset;
    }

    void onDisable()
    {
        eventManager.OnReset -= reset;
    }   

    public Vector3 startingPos;
    private void reset()
    {
        this.gameObject.tag="crate";
        transform.position = startingPos; //+ new Vector3(0.5f,0.5f,0.5f);
    }
}
