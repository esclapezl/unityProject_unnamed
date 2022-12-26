using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restartTransitionScript : MonoBehaviour
{
    public Camera cam;
    float camHeight;
    float camWidth;
    

    public GameObject barreNoire;
    private GameObject barreBas;
    private GameObject barreHaut;
    
    void bandeRandomsAnim()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        for(int i =0;i<camWidth+2;i+=2)
        {
            barreBas = Instantiate(barreNoire, new Vector3(cam.transform.position.x-(camWidth/2)+i, cam.transform.position.y-(camHeight), -20), Quaternion.identity);
            barreHaut = Instantiate(barreNoire, new Vector3(cam.transform.position.x-(camWidth/2)+i, cam.transform.position.y+(camHeight), -20), Quaternion.identity);
            
            barreBas.transform.parent = gameObject.transform;
            barreHaut.transform.parent = gameObject.transform;

            barreBas.transform.localScale += new Vector3(2,camHeight,0);
            barreHaut.transform.localScale += new Vector3(2,camHeight,0);
           
            StartCoroutine(animateBar(barreBas,barreHaut));
        }
    
    }

    IEnumerator animateBar(GameObject barreBas, GameObject barreHaut)
    {
        
        float meetingPoint = Random.Range(camHeight*0.2f,camHeight*0.8f);

        Vector3 downStartingPos = barreBas.transform.position;
        Vector3 upStartingPos = barreHaut.transform.position;
        Vector3 downEndingPos = barreBas.transform.position += new Vector3(0,camHeight-meetingPoint,0);
        Vector3 upEndingPos = barreHaut.transform.position -= new Vector3(0,meetingPoint,0);

        float moveDuration = 0.5f;
        float timeElapsed = 0;
        while(timeElapsed < moveDuration)
        {
            barreBas.transform.position = Vector3.Lerp(downStartingPos,downEndingPos, timeElapsed / moveDuration);
            barreHaut.transform.position = Vector3.Lerp(upStartingPos,upEndingPos, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        barreBas.transform.position = downEndingPos;
        barreHaut.transform.position = upEndingPos;

        yield return new WaitForSeconds(0.2f);

        moveDuration=0.5f;
        timeElapsed = 0;
        while(timeElapsed < moveDuration)
        {
            barreBas.transform.position = Vector3.Lerp(downEndingPos,downStartingPos, timeElapsed / moveDuration);
            barreHaut.transform.position = Vector3.Lerp(upEndingPos,upStartingPos, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        barreBas.transform.position = downStartingPos;
        barreHaut.transform.position = upStartingPos;

        Destroy(barreBas);
        Destroy(barreHaut);
    }

    public void reset()
    {
        bandeRandomsAnim();
    }

}
