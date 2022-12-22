using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingCubeScript : MonoBehaviour
{
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

    private ArrayList tagsPassableObstacle = new ArrayList();

    public GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        //ajouter les objets que this peut passer
        this.tagsPassableObstacle = gameManager.passableObjects();
        gameManager.setDepth(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!(this.gameObject.tag=="crateInHole"))
        {
            //bouger la caisse sur la grille si perso derriere et qu'il pousse et que rien derriere
            if(Input.GetButtonDown("down")
            && movementCondition(downHitbox,upHitbox))
            {
                StartCoroutine(slidingCubeSlide(new Vector3(0,-1,0),downHitbox));
            }
            else if(Input.GetButtonDown("up")
            && movementCondition(upHitbox,downHitbox))
            {
                StartCoroutine(slidingCubeSlide(new Vector3(0,1,0),upHitbox));
            }
            else if(Input.GetButtonDown("right")
            && movementCondition(rightHitbox,leftHitbox))
            {
                StartCoroutine(slidingCubeSlide(new Vector3(1,0,0),rightHitbox));
            }
            else if(Input.GetButtonDown("left")
            && movementCondition(leftHitbox,rightHitbox))
            {
                StartCoroutine(slidingCubeSlide(new Vector3(-1,0,0),leftHitbox));
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

    IEnumerator slidingCubeSlide(Vector3 v, hitboxScript h)
    {
        while((!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag)) 
        && !(this.gameObject.tag=="crateInHole"))
        {
            if(h.objectTrigger != null && h.objectTrigger.tag=="hole"){
                h.objectTrigger.tag="crateInHole";
                this.gameObject.tag="crateInHole";
                this.GetComponent<BoxCollider2D> ().enabled = false;
            }

            Instantiate(trail, transform.position, Quaternion.identity);

            transform.position = transform.position + v;  
            
            gameManager.setDepth(this.gameObject);
            
            if((this.gameObject.tag=="crateInHole"))
            {
                transform.position += new Vector3(0,0,1);
            } 
            yield return new WaitForSeconds(0.05f);  
        }            
    }

    
}

