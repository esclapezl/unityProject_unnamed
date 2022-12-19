using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceCubeScript : MonoBehaviour
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
                StartCoroutine(iceCubeSlide(new Vector3(0,-1,0),downHitbox));
            }
            else if(downHitbox.isColliding 
            && downHitbox.objectTrigger.tag=="player" 
            && (!upHitbox.isColliding 
            || tagsPassableObstacle.Contains(upHitbox.objectTrigger.tag))
            && Input.GetButtonDown("up"))
            {
                StartCoroutine(iceCubeSlide(new Vector3(0,1,0),upHitbox));
            }
            else if(leftHitbox.isColliding 
            && leftHitbox.objectTrigger.tag=="player" 
            && (!rightHitbox.isColliding 
            || tagsPassableObstacle.Contains(rightHitbox.objectTrigger.tag))
            && Input.GetButtonDown("right"))
            {
                StartCoroutine(iceCubeSlide(new Vector3(1,0,0),rightHitbox));
            }
            else if(rightHitbox.isColliding 
            && rightHitbox.objectTrigger.tag=="player" 
            && (!leftHitbox.isColliding 
            || tagsPassableObstacle.Contains(leftHitbox.objectTrigger.tag))
            && Input.GetButtonDown("left"))
            {
                StartCoroutine(iceCubeSlide(new Vector3(-1,0,0),leftHitbox));
            }
        }
       
    }

    IEnumerator iceCubeSlide(Vector3 v, hitboxScript h)
    {
        while((!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag)) 
        && !(this.gameObject.tag=="crateInHole"))
        {
            if(h.objectTrigger != null && h.objectTrigger.tag=="hole"){
                h.objectTrigger.tag="crateInHole";
                this.gameObject.tag="crateInHole";
                this.GetComponent<BoxCollider2D> ().enabled = false;
                transform.position += new Vector3(0,0,1);
            }
            transform.position = transform.position + v;     
            yield return new WaitForSeconds(0.05f);  
        }            
    }

    
}
