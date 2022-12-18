using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeScript : MonoBehaviour
{
    public hitboxScript hitbox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitbox.objectTrigger != null && hitbox.objectTrigger.tag=="crateInHole")
        {
            this.gameObject.tag="crateInHole";
        }
    }
}
