using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingCubeTrailScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(trailErrasing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator trailErrasing()
    {
        for(int i =0; i<10; i++)
        {
            this.GetComponent<SpriteRenderer>().color *= 0.9f;
            yield return new WaitForSeconds(0.05f);
        }          
        this.GetComponent<SpriteRenderer>().color *= 0;
        Destroy(gameObject);
    }
}
