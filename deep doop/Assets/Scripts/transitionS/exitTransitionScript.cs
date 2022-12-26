using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitTransitionScript : MonoBehaviour
{
    public Camera cam;
    float camHeight;
    float camWidth;
    

    public GameObject barreNoire;
    private GameObject barre;
    

    IEnumerator bandesAnimExit()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        for(int i =0;i<camWidth+4;i+=4)
        {
            barre = Instantiate(barreNoire, new Vector3(cam.transform.position.x-(camWidth/2)+i, cam.transform.position.y+(camHeight), -20), Quaternion.identity);
            barre.transform.parent = gameObject.transform;
            barre.transform.localScale += new Vector3(4,camHeight,0);
            yield return new WaitForSeconds(0.20f/(camWidth/4));
            StartCoroutine(animateBar(barre));
        }
    }

    

    IEnumerator animateBar(GameObject barre)
    {
        Vector3 StartingPos = barre.transform.position;
        Vector3 EndingPos = barre.transform.position - new Vector3(0,camHeight,0);

        float moveDuration = 0.5f;
        float timeElapsed = 0;
        while(timeElapsed < moveDuration)
        {
            barre.transform.position = Vector3.Lerp(StartingPos,EndingPos, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        barre.transform.position = EndingPos;

        StartingPos = barre.transform.position;
        EndingPos = barre.transform.position - new Vector3(0,camHeight,0);
        yield return new WaitForSeconds(0.2f);

        moveDuration = 0.5f;
        timeElapsed = 0;
        while(timeElapsed < moveDuration)
        {
            barre.transform.position = Vector3.Lerp(StartingPos,EndingPos, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        barre.transform.position = EndingPos;
        Destroy(barre);
    }

    public void exit()
    {
        StartCoroutine(bandesAnimExit());
    }
}
