using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoDirectionScript : MonoBehaviour
{
    //CE SCRIPT EST UNE EXTENSION AU SCRIPT DE CAISSE
    public Transform directionIndicator;
    public string direction;
    private int directionValue;

    public bool reversed;
    

    // Start is called before the first frame update
    void Start()
    {
        startingRot = direction;
        setDirection();
    }

    void setDirection()
    {
        switch(direction)
        {
            case "up":
                directionValue = 180;
                break;
        
            case "down":
                directionValue = 0;
                break;

            case "left":
                directionValue = 270;
                break;

            case "right":
                directionValue = 90;
                break;

            default:
                directionValue = 0;
                direction = "down";
                break;
        }
        directionIndicator.rotation = Quaternion.Euler(0f, 0f,directionValue);
    }

    // Update is called once per frame
    

    public bool movementCondition(string d)
    {
        return (
        direction == d);
    }

    private void rotate()
    {
        List<string> rotation = new List<string>();
        rotation.Add("down");
        rotation.Add("left");
        rotation.Add("up");
        rotation.Add("right");
        int currentDirection = rotation.IndexOf(direction);
        
        if(!reversed) //clockWise
        {
            currentDirection =(currentDirection+1)%4;
            
        }
        else //counter clockWise
        {
            currentDirection =((currentDirection-1)+4)%4;
        }
        direction = rotation[currentDirection];
    }

    void OnEnable()
    {
        eventManager.OnReset += reset;
        eventManager.OnRotate += rotate;
    }

    void onDisable()
    {
        eventManager.OnReset -= reset;
        eventManager.OnRotate -= rotate;
    }   



    private string startingRot;
    private void reset()
    {
        direction = startingRot;
        setDirection();
    }
}
