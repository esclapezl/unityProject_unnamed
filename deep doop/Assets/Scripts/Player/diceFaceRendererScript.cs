using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceFaceRendererScript : MonoBehaviour
{
    public int faceActuelle;
    public int orientation;

    public Animator animator;

    void Start()
    {
        faceActuelle = 1;
         orientation = 0; // orientation == x 90°, ex orientation = 2 donne 2*90 soit 180° apres la position d'origine

        /* TEST DE
        orientation = 270;
        List<int> facesDispos = new List<int>();
        List<int> rotationFacesDispos = new List<int>();
        facesDispos.Add(4);  
                facesDispos.Add(6);
                facesDispos.Add(3);
                facesDispos.Add(1);   

                rotationFacesDispos.Add((270+orientation)%360);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((90+orientation)%360);
                rotationFacesDispos.Add(orientation);
        print(toString(facesDispos));
        facesDispos = decalerListe(facesDispos,270/90);
        print("resultat : "+toString(facesDispos));
        print("resultat attendu : {6,3,1,4}");
        */
    }

    void Update()
    {
        animator.SetInteger("face",faceActuelle);
    }

    public void setDiceFace(string direction) //orientation en 90x° //direction input joueur
    {
        /*                          |4| patron utilisé
                                |1|2|6|5|
                                    |3|
        */
        
        // defini la case des facesDisponibles à prendre
        int d;
        switch(direction)  
        {
            case ("up"):
                d=0;
                break;
            case "right":
                d=1;
                break;
            case "down":
                d=2;
                break;
            case "left":
                d=3;
                break;

            default:
            d=0;
            break;
        }
        
        //défini les faces dispos ainsi que les orientations des faces de destination par rapport à la face de départ correctement orienté
        //on ajoute la rotation de la face de départ pour correspondre au faces d'arrivées
        List<int> facesDispos = new List<int>();
        List<int> rotationFacesDispos = new List<int>();
        switch(faceActuelle)
        {
            case 1:
                //{4,2,3,5};
                facesDispos.Add(4);  
                facesDispos.Add(2);
                facesDispos.Add(3);
                facesDispos.Add(5);

                rotationFacesDispos.Add((180+orientation)%360);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((180+orientation)%360);
                rotationFacesDispos.Add(orientation);
                break;
            case 2:
                //{4,6,3,1};
                facesDispos.Add(4);  
                facesDispos.Add(6);
                facesDispos.Add(3);
                facesDispos.Add(1);   

                rotationFacesDispos.Add((270+orientation)%360);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((90+orientation)%360);
                rotationFacesDispos.Add(orientation);
                break;
            case 3:
                //{6,5,1,2};
                facesDispos.Add(6);  
                facesDispos.Add(5);
                facesDispos.Add(1);
                facesDispos.Add(2);

                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((90+orientation)%360);
                rotationFacesDispos.Add((180+orientation)%360);
                rotationFacesDispos.Add((270+orientation)%360);
                break;
            case 4:
                //{1,5,6,2};
                facesDispos.Add(1);  
                facesDispos.Add(5);
                facesDispos.Add(6);
                facesDispos.Add(2);

                rotationFacesDispos.Add((180+orientation)%360);
                rotationFacesDispos.Add((270+orientation)%360);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((90+orientation)%360);
                break;
            case 5:
                //{4,1,3,6};
                facesDispos.Add(4);  
                facesDispos.Add(1);
                facesDispos.Add(3);
                facesDispos.Add(6);

                rotationFacesDispos.Add((90+orientation)%360);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add((270+orientation)%360);
                rotationFacesDispos.Add(orientation);
                break;
            case 6:
                //{4,5,3,2};
                facesDispos.Add(4);  
                facesDispos.Add(5);
                facesDispos.Add(3);
                facesDispos.Add(2);

                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add(orientation);
                rotationFacesDispos.Add(orientation);
                
                break;
        }
  
        facesDispos = decalerListe(facesDispos,orientation/90);
        rotationFacesDispos = decalerListe(rotationFacesDispos,orientation/90);

        faceActuelle = facesDispos[d];
        orientation = rotationFacesDispos[d];
        transform.rotation = Quaternion.Euler(0f, 0f,rotationFacesDispos[d]);
    }

    private List<int> decalerListe(List<int> fd, int o)
    {

        List<int> tmp = new List<int>(fd);

        for(int i =0; i<4;i++)
        {
            
            int d = (i+o)%4; 
            tmp[d]=fd[i];
        }
        return tmp;
    }

    private string toString(List<int> fd)
    {
        string s ="{";
        for(int i =0; i<fd.Count;i++)
        {
            s +=fd[i];
        }
        s += "}";
        return s;
    }

}
