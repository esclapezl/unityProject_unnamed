using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public eventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(exitLevel);
    }

    // Update is called once per frame
    void exitLevel()
    {
        eventManager.exitLevel();
    }
}
