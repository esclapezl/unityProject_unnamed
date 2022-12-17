using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxScript : MonoBehaviour
{

    public bool isColliding;
    
    public BoxCollider2D bc;

    
    void OnTriggerEnter2D (Collider2D hitInfo)
	{
        isColliding = true;
    }

    void OnTriggerExit2D (Collider2D hitInfo)
	{
        isColliding = false;
    }
}
