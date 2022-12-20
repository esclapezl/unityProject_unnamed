using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchBlockScript : MonoBehaviour
{
    public string switchColor;
    private bool switchState;

    Color c;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.setDepth(this.gameObject);
        switch(switchColor)
        {
            case "red":
            c = Color.red;
            this.GetComponent<SpriteRenderer>().color = Color.black;
            this.switchState = false;
            this.gameObject.tag="hole";
            break;

            case "blue":
            this.GetComponent<SpriteRenderer>().color = c = Color.blue ;
            this.switchState = true;
            this.gameObject.tag="closedSwitch";
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((this.switchColor == "red" && gameManager.globalSwitchValue != this.switchState)
        || (this.switchColor == "blue" && gameManager.globalSwitchValue == this.switchState))
        {   
            this.switchState = !this.switchState;
            if(!this.switchState)
            {
                this.GetComponent<SpriteRenderer>().color = c;
                this.gameObject.tag="closedSwitch";

                gameManager.setDepth(this.gameObject); // remet l'objet a sa profondeur correcte
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = Color.black;
                this.gameObject.tag="hole";

                transform.position += new Vector3(0,0,2);  //eloigne le trou en profondeur
            }
        }
    }
}
