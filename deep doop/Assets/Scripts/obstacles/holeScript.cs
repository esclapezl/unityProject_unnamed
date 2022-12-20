using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeScript : MonoBehaviour
{
    public hitboxScript hitbox;
    // Start is called before the first frame update
    void Start()
    {
        
        gameManager.setDepth(this.gameObject);
        transform.position += new Vector3(0,0,2); 
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag=="crateInHole")
        {
            this.GetComponent<BoxCollider2D> ().enabled = false;
        }
    }
}
