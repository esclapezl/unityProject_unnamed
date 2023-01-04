using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableBlockScript : MonoBehaviour
{
    public IEnumerator breakObject()
    {
        yield return new WaitForSeconds(0.05f);
        //anim
        transform.GetChild(0).gameObject.SetActive(false);
        
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
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
