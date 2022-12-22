using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public delegate void ResetAction();
    public static event ResetAction OnReset;

    public delegate void SwitchAction();
    public static event SwitchAction SwitchBlocks;

    void Update()
    {
        if (Input.GetButtonDown("reset"))
        {
            if(OnReset != null)
            {
                OnReset();
            }
        } 
        if(Input.GetButtonDown("switch"))
        {
            
            if(SwitchBlocks != null)
            {
                SwitchBlocks();
            }
            
        } 
    }
}
