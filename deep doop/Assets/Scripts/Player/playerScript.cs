using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [Space(5)]
    [Header("HitBoxes")]
    public hitboxScript rightHitbox;
    public hitboxScript leftHitbox;
    public hitboxScript upHitbox;
    public hitboxScript downHitbox;
    public hitboxScript centerHitbox;

    [Space(5)]
    
    public ArrayList tagsPassableObstacle = new ArrayList();

    [Space(5)]
    
    public diceFaceRendererScript dfr; 

    [Space(5)]
    
    public levelSelectionScript levelSelection;
    public eventManager eventManager;

    public GameObject trail;
    
    //public Vector3[] madeActions;

    // Start is called before the first frame update
    void Start()
    {
        //ajouter les objets que this peut passer
        tagsPassableObstacle.Add("crateInHole");
        tagsPassableObstacle.Add("closedSwitch");
        tagsPassableObstacle.Add("ice");
        tagsPassableObstacle.Add("goal");
        gameManager.setDepth(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        //bouger le perso sur la grille
        if (Input.GetButtonDown("up"))
        { 
            if(movementCondition(upHitbox))
            {
                StartCoroutine(move(new Vector3(0,1,0),upHitbox,"up"));
            }
            else
            {
                    
            }
                
        }
        else if(Input.GetButtonDown("down"))
        {
            if(movementCondition(downHitbox))
            {
                StartCoroutine(move(new Vector3(0,-1,0),downHitbox,"down"));
            }
            else
            {
                    
            }
        }
        else if(Input.GetButtonDown("left"))
        {
            if(movementCondition(leftHitbox))
            {
                StartCoroutine(move(new Vector3(-1,0,0),leftHitbox,"left"));
            }
            else
            {
                    
            }
        }
        else if(Input.GetButtonDown("right"))
        {
            if(movementCondition(rightHitbox))
            {
                StartCoroutine(move(new Vector3(1,0,0),rightHitbox,"right"));
            }
            else
            {
                    
            }
        }
    }

    public bool animationEnded = true;

    private bool movementCondition(hitboxScript h)
    {
        return ((!h.isColliding
            || tagsPassableObstacle.Contains(h.objectTrigger.tag))
            && animationEnded
            && levelSelection.currentLevel.nbCoupsLeft != 0);
    }

    private bool slideCondition(hitboxScript h)
    {
        return (h.objectTrigger != null 
        && h.objectTrigger.tag == "ice"
        && animationEnded
        && levelSelection.currentLevel.nbCoupsLeft != 0);
    }

    IEnumerator move(Vector3 v, hitboxScript h, string direction)
    {
        //desactive les fleches tuto
        if(levelSelection.currentLevel.level == 1)
        {
            foreach(Transform child in transform)
            {
                if(child.name == "tutorialArrows")
                {
                    child.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0); 
                    child.GetComponent<tutorialArrowsScript>().hasMoved = true;
                }
            }
        }
        
        eventManager.playerMove();
        
        animationEnded = false;
        dfr.setDiceFace(direction);
        //transform.position += v;
        //yield return new WaitForSeconds(0.05f); 
        while((!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag))
        && !(centerHitbox.isColliding && centerHitbox.objectTrigger.tag == "goal"))
        {
            Instantiate(trail, transform.position, Quaternion.identity);
            transform.position = transform.position + v;  
            yield return new WaitForSeconds(0.025f);   
        }
        animationEnded = true;  

        /* ANCIEN MOUVEMENT 1 MVMT = 1 UNITE
        eventManager.playerMove();

        animationEnded = false;
        dfr.setDiceFace(direction);
        transform.position += v;
        yield return new WaitForSeconds(0.03f); // durée de l'animation
        animationEnded = true;
        */
    }


    public IEnumerator slideCoroutine(Vector3 v, hitboxScript h, string direction)
    {
        eventManager.playerMove();
        
        animationEnded = false;
        dfr.setDiceFace(direction);
        transform.position += v;
        yield return new WaitForSeconds(0.05f); 
        while((!h.isColliding || tagsPassableObstacle.Contains(h.objectTrigger.tag)) 
        && (centerHitbox.objectTrigger != null && centerHitbox.objectTrigger.tag == "ice"))
        {
            transform.position = transform.position + v;  
            yield return new WaitForSeconds(0.05f);  
        }
        animationEnded = true;            
    }



    //reset
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
        dfr.faceActuelle = 1;
        dfr.orientation = 0;
        transform.position = new Vector3(levelSelection.currentLevel.startingPos.transform.position.x,levelSelection.currentLevel.startingPos.transform.position.y,0);
    }



    

}

    