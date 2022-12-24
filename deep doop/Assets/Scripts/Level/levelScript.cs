using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelScript : MonoBehaviour
{
    public int level;

    public Text textNbRotation;
    public int nbRotate;
    public int nbRotateLeft;

    public Text textNbSwitch;
    public int nbSwitch;
    public int nbSwitchLeft;

    public GameObject playerPos;
    public string objective;

    
    
    // Start is called before the first frame update
    void Start()
    {
        nbRotateLeft = nbRotate;
        textNbRotation.text = nbRotateLeft.ToString();
        nbSwitchLeft = nbSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        nbRotateLeft = nbRotate;
        textNbRotation.text = nbRotateLeft.ToString();
        nbSwitchLeft = nbSwitch;
    }
}
