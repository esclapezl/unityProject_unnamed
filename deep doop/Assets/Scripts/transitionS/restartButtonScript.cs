using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class restartButtonScript : MonoBehaviour
{
    public Button button;
    public eventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(restartLevel);
    }

    // Update is called once per frame
    void restartLevel()
    {
        eventManager.restartLevel();
    }
}
