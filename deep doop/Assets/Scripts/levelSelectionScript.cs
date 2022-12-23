using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelectionScript : MonoBehaviour
{
    public Button[] lvlButtons;
    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt",2);
        for(int i =0; i<lvlButtons.Length;i++)
        {
            if(i+2 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }

    public void ChangeToScene(int sceneNumber)
    {
        SceneManager.LoadScene (sceneNumber+1);
    }

    // Update is called once per frame
    
}
