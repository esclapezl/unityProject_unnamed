using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingScript : MonoBehaviour
{
    //CE SCRIPT EST UNE EXTENSION AU SCRIPT DE CAISSE
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;

   

    public GameObject trail;

    public void slide(Vector3 v, hitboxScript h,ArrayList tagsPassables)
    {
        StartCoroutine(slideCoroutine(v,h,tagsPassables));
    }
    public IEnumerator slideCoroutine(Vector3 v, hitboxScript h,ArrayList tagsPassables)
    {
        while((!h.isColliding || tagsPassables.Contains(h.objectTrigger.tag)) 
        && !(this.gameObject.tag=="crateInHole"))
        {
            if(h.objectTrigger != null && h.objectTrigger.tag=="hole"){
                h.objectTrigger.tag="crateInHole";
                this.gameObject.tag="crateInHole";
                this.GetComponent<BoxCollider2D> ().enabled = false;
            }

            Instantiate(trail, transform.position, Quaternion.identity);

            transform.position = transform.position + v;  
            
            
            if((this.gameObject.tag=="crateInHole"))
            {
                transform.position += new Vector3(0,0,1);
            } 
            yield return new WaitForSeconds(0.05f);  
        }            
    }

    
}

