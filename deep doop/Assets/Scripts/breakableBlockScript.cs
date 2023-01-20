using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableBlockScript : MonoBehaviour
{
    SpriteRenderer sp;
    BoxCollider2D bc;
    void Awake()
    {
        sp =  transform.GetChild(0).GetComponent<SpriteRenderer>();
        bc = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    public IEnumerator breakObject()
    {
        yield return new WaitForSeconds(0.05f);
        //anim
        sp.color = new Color(0f,0f,0f,0f);
        bc.enabled = false;
        
    }

    void OnEnable()
    {
        eventManager.OnReset += reset;
    }

    void onDisable()
    {
        eventManager.OnReset -= reset;
    }   

    private void reset()
    {
        sp.color = new Color(1f,1f,1f,1f);
        bc.enabled = true;
    }
}
