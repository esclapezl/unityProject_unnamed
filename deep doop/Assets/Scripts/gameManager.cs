using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
   
    public static bool globalSwitchValue = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("switch"))
        {
            switchBlocks();
        }  
         
    }

    void switchBlocks()
    {
        globalSwitchValue = !globalSwitchValue;
    }


    public static ArrayList passableObjects() // les objets que les objets peuvent traverser
    {
        ArrayList tagsPassableObstacle = new ArrayList();
        tagsPassableObstacle.Add("hole");
        tagsPassableObstacle.Add("crateInHole");
        tagsPassableObstacle.Add("closedSwitch");
        return tagsPassableObstacle;
    }

    public static ArrayList playerPassableObjects() // les objets que le joueur peut traverser
    {
        ArrayList tagsPlayerPassableObstacle = new ArrayList();
        tagsPlayerPassableObstacle.Add("crateInHole");
        tagsPlayerPassableObstacle.Add("closedSwitch");
        return tagsPlayerPassableObstacle;
    }

    public static void setDepth(GameObject g)
    {
        g.transform.position = new Vector3(g.transform.position.x,g.transform.position.y,Mathf.Floor(g.transform.position.y));
    }


}
