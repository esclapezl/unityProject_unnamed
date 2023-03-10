using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelectorScript : MonoBehaviour
{
    public bool locked;
    private int level;
    public Animator animator;
    public SpriteRenderer levelNum;
    public hitboxScript hitbox;
    public levelSelectionScript selectionScript;
    // Start is called before the first frame update
    void Start()
    {
        int i = 1;
        while("level_icon_"+i.ToString() != gameObject.name)
        {
            i++;
        }
        level = i;

        
        locked = true;
        animator.SetBool("locked",locked);
        levelNum.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("levelAt")>level)
        {
            locked = false;
            animator.SetBool("locked",locked);
            levelNum.enabled = true;
        }

        if(Input.GetButtonDown("Submit")
        && hitbox.objectTrigger != null
        && hitbox.objectTrigger.tag == "player"
        && !locked)
        {
            selectionScript.ChangeToLevel(level);
        }
    }
}
