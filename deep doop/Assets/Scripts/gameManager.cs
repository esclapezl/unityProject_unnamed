using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static ArrayList tagsPassableObstacle = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        tagsPassableObstacle.Add("hole");
        tagsPassableObstacle.Add("crateInHole");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
