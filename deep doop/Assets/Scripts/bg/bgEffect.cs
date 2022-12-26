using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgEffect : MonoBehaviour
{
    public GameObject bgTile;
    public Camera cam;
    float camHeight;
    float camWidth;

    int nbTilesX;
    int nbTilesY;
    // Start is called before the first frame update

    void Start()
    {
        createBg();
    }

    public void createBg()
    {
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        for(int i = 0;i<(camWidth/2)+1;i++)
        {
            for(int j =0; j<(camHeight/2)+1;j++)
            {
                bgTile = Instantiate(bgTile,new Vector3(cam.transform.position.x-camWidth/2+2*i+1,cam.transform.position.y-camHeight/2+2*j+1,20),Quaternion.identity);
                bgTile.transform.parent = gameObject.transform;
                StartCoroutine(translateTile(bgTile));
                nbTilesY = j+1;
            }
            nbTilesX = i+1;
        }

    }

    IEnumerator translateTile(GameObject tile)
    {
        yield return new WaitForSeconds(0.1f);
        while(tile != null)
        {
            if(tile != null
            && tile.transform.position.x >
            cam.transform.position.x+camWidth/2+1)
            {
                tile.transform.position -= new Vector3(nbTilesX*2,0,0);
            }

            if(tile != null
            && tile.transform.position.y <
            cam.transform.position.y-camHeight/2-1)
            {
                tile.transform.position += new Vector3(0,nbTilesY*2,0);
            }

            if(tile != null)
            {   
                tile.transform.Translate((Vector3.down+ Vector3.right) *Time.deltaTime);
            }
            yield return null;
        }
    }

  
    
}
