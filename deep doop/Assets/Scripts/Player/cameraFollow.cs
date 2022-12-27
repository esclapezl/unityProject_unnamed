using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    float minX;
    void Start()
    {
        minX = Camera.main.transform.position.x;
    }

    public levelSelectionScript levelSelection;
    public GameObject player;
    public GameObject bg;
    // Update is called once per frame
    void Update()
    {
        if(levelSelection.currentLevel.level == 0
        && (Camera.main.transform.position.x < player.transform.position.x
        || (Camera.main.transform.position.x > player.transform.position.x 
        && Camera.main.transform.position.x > minX)))
        {
            Camera.main.transform.position = new Vector3 (player.transform.position.x,0 ,-100);
            bg.transform.position = new Vector3 (player.transform.position.x,0 ,20);
        }
        
    }
}
