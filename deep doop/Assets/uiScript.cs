using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiScript : MonoBehaviour
{
    public GameObject restartButton;
    // Update is called once per frame
    void Update()
    {
        if(levelSelectionScript.currentLevelNum == 0)
        {
            restartButton.SetActive(false);
        }
        else
        {
            restartButton.SetActive(true);
        }
    }

    public GameObject rotateIcon;
    public Text textNbRotation;

    public void setRotate(bool etat, int nbRotate)
    {

        rotateIcon.SetActive(etat);
        textNbRotation.enabled = etat;
        if(etat){
            textNbRotation.text = nbRotate.ToString();
        }
    }

    
    public GameObject switchIcon;
    public Text textNbSwitch;
    public void setSwitch(bool etat, int nbSwitch)
    {
        switchIcon.SetActive(etat);
        textNbSwitch.enabled = etat;
        if(etat){
            textNbSwitch.text = nbSwitch.ToString();
        }
    }

    public GameObject actionIcon;
    public Text textNbCoups;
    public void setCoups(int nbCoups)
    {
        actionIcon.SetActive(nbCoups >= 0);
        textNbCoups.enabled = (nbCoups >= 0);
        if(nbCoups >= 0){
            textNbCoups.text = nbCoups.ToString();
        }
    }
}
