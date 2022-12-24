using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Camera.main.transform.position.x < this.transform.position.x
        || (Camera.main.transform.position.x > this.transform.position.x 
        && Camera.main.transform.position.x > 2.5f))
        {
            Camera.main.transform.position = new Vector3 (this.transform.position.x,0 ,-100);
        }
        
    }
}
