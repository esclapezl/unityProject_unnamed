using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polarityWholeBlockScript : MonoBehaviour
{
    //A DECIDER /!\
    // SOIT LE BLOCK SARRETE DES QUIL RENCONTRE UN OBJET
    // SOIT IL POUSSE LOBJET ET CONTINUE SON CHEMIN QUAND LOBJET NEST PLUS LA

    

    public bool isSetUp = false;
    public bool isOn;
    public int polarity = 1;
    public string axis;

    public ArrayList tagsPassableObstacle = new ArrayList();
    Vector3 v;


    public List<polarityHitboxScript> upHitboxes;
    public List<polarityHitboxScript> downHitboxes;
    public List<polarityHitboxScript> leftHitboxes;
    public List<polarityHitboxScript> rightHitboxes;

    List<bool> pushableObjectsInCollisison; //permet de savoir si au moins un objet n'est pas poussable pour eviter de tester pour rien

    public static int usedByNBlocks = 0;

    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
        pushableObjectsInCollisison = new List<bool>(); 

        upHitboxes = new List<polarityHitboxScript>();
        downHitboxes = new List<polarityHitboxScript>();
        leftHitboxes = new List<polarityHitboxScript>();
        rightHitboxes = new List<polarityHitboxScript>();
        //ajouter les objets que this peut passer
        //startingPos = transform.position;
        //set les parametres de base pour le reset
        StartCoroutine(setUp());
        this.tagsPassableObstacle = gameManager.passableObjects();

    }

    void Update()
    {
        if(pushableObjectsInCollisison.Count > 0
        && !pushableObjectsInCollisison.Contains(false)) //tous les objets en collisions sont poussables
        {
            if(!isCollidingDirection(v))
            {
                gameManager.actionFinished = false;
                usedByNBlocks++;
                StartCoroutine(slideCoroutine(v));
                pushableObjectsInCollisison.Clear();
            }

        }
    }

    IEnumerator setUp()
    {
        yield return new WaitForSeconds(1f);
        isSetUp=true;
        
        foreach (Transform Child in transform) 
        {
            if (Child.GetComponent<polarityBlock>().upHitbox != null) 
            {
                upHitboxes.Add(Child.GetComponent<polarityBlock>().upHitbox);
            }
            if (Child.GetComponent<polarityBlock>().downHitbox != null) 
            {
                downHitboxes.Add(Child.GetComponent<polarityBlock>().downHitbox);
            }
            if (Child.GetComponent<polarityBlock>().leftHitbox != null) 
            {
                leftHitboxes.Add(Child.GetComponent<polarityBlock>().leftHitbox);
            }
            if (Child.GetComponent<polarityBlock>().rightHitbox != null) 
            {
                rightHitboxes.Add(Child.GetComponent<polarityBlock>().rightHitbox);
            }
        }
    }

    bool isCollidingDirection(Vector3 v)
    {
        List<polarityHitboxScript> hitboxes;
        if(v == Vector3.left)
        {
            hitboxes = leftHitboxes;
        }
        else if(v == Vector3.right)
        {
            hitboxes = rightHitboxes;
        }
        else if(v == Vector3.up)
        {   
            hitboxes = upHitboxes;
        }
        else //down
        {
            hitboxes = downHitboxes;
        }

        foreach(polarityHitboxScript h in hitboxes)
        {
            if(h.isColliding)
            {
                //objet poussable
                if(h.objectTrigger != null && h.objectTrigger.GetComponent<pushable>() != null)
                {
                    //essayer de le pousser
                    if((h.objectTrigger != null 
                    && h.objectTrigger.GetComponent<pushable>() != null 
                    && h.objectTrigger.GetComponent<pushable>().canBePushed(v)))
                    {
                        h.objectTrigger.GetComponent<pushable>().push(v);
                        return false; //l'objet a été poussé par la struct, la struct peut continuer son chemin
                    }
                    else
                    {
                        pushableObjectsInCollisison.Add(true); //l'objet est poussable mais pas par la struct
                    }
                }
                else
                {
                    pushableObjectsInCollisison.Add(false); //l'objet n'est pas poussable
                }
                return true; //l'objet n'as pas été poussé ou n'est pas poussable 
            }
        }
        return false;
    }

    
    void changePolarity()
    {
        if(transform.parent.GetComponent<levelScript>() != null
        && transform.parent.GetComponent<levelScript>().level == transform.parent.GetComponent<levelScript>().levelSelection.currentLevel.level)
        {

        
        
            //pour que tous les blocks aient bien le temps de finir leur mvmt
            gameManager.actionFinished = false;
            usedByNBlocks++;


            pushableObjectsInCollisison.Clear(); //si un objet bloquait la structure, il n'est plus pertinent vu que la direction a changé
            if(!isOn){
                isOn = true; 
                foreach (Transform Child in transform) 
                {
                    Child.GetComponent<polarityBlock>().isOn = true;
                    Child.GetComponent<polarityBlock>().animator.SetBool("isOn",true);
                    Child.GetComponent<polarityBlock>().animator.SetInteger("polarity",polarity);
                }
            }
            else{
                polarity *= -1;
                foreach (Transform Child in transform) 
                {
                    Child.GetComponent<polarityBlock>().polarity = polarity;
                    Child.GetComponent<polarityBlock>().animator.SetInteger("polarity",polarity);
                }
            }

            if(axis == "x"){
                v = new Vector3(polarity,0,0);
            }
            else if(axis =="y"){
                v = new Vector3(0,polarity,0);
            }

            StartCoroutine(slideCoroutine(v));
        }
        
    }

    //recoit les infos de toutes les hitboxes
    
    public IEnumerator slideCoroutine(Vector3 v)
    {
        while(!isCollidingDirection(v))
        {
            transform.position = transform.position + v;  
            yield return new WaitForSeconds(0.05f);
        }      
        Camera.main.GetComponent<cameraShake>().shake();      
        
        //pour que tous les blocks aient bien le temps de finir leur mvmt sans que d'actions ne soient faites
        usedByNBlocks--;
        if(usedByNBlocks == 0)
        {
            gameManager.actionFinished = true;
        }
    }

    void OnEnable()
    {
        eventManager.OnPolarity += changePolarity;
        eventManager.OnReset += reset;
    }

    void onDisable()
    {
        eventManager.OnPolarity -= changePolarity;
        eventManager.OnReset -= reset;
    }   

    private void reset()
    {
        transform.position = startingPos;
        isOn = false;
        polarity = 1;

        foreach (Transform Child in transform) 
        {
            Child.GetComponent<polarityBlock>().isOn = false;
            Child.GetComponent<polarityBlock>().animator.SetBool("isOn",isOn);
            Child.GetComponent<polarityBlock>().animator.SetInteger("polarity",polarity);
        }
    }
}
