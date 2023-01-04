using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class polarityBlock : MonoBehaviour
{
    private string compositionId;
    public bool isOn = false;
    public int polarity = 1;

    public Animator animator;

    public GameObject hitboxesFolder;

    public polarityHitboxScript upLeftHitbox;
    public polarityHitboxScript upHitbox;
    public polarityHitboxScript upRightHitbox;
    public polarityHitboxScript leftHitbox;
    public polarityHitboxScript rightHitbox;
    public polarityHitboxScript downLeftHitbox;
    public polarityHitboxScript downHitbox;
    public polarityHitboxScript downRightHitbox;
    
    
    

    public bool[,] tile;

    
    // Start is called before the first frame update
    IEnumerator setSprite()
    {   
        
        gameObject.name = gameObject.transform.parent.gameObject.name;
        
        
        
        yield return null;
        //etablit les blocks du meme blocks a proximité

        //  _|_|_
        //  _|_|_
        //   | |
        
        polarityHitboxScript[] hitboxes = new polarityHitboxScript[]{upLeftHitbox,upHitbox,upRightHitbox,leftHitbox,rightHitbox,downLeftHitbox,downHitbox,downRightHitbox};
        tile = new bool[3,3];

        int i = 0;
        int j = 0;
        
        foreach(polarityHitboxScript hitbox in hitboxes)
        {
            tile[i,j] = (hitbox.objectTrigger != null && hitbox.objectTrigger.name == gameObject.name);
            
            if(tile[i,j] 
            || hitbox.name == "hul"
            || hitbox.name == "hur"
            || hitbox.name == "hdl"
            || hitbox.name == "hdr")
            {
                
                Destroy(hitbox.gameObject);
            }
            j++;
            if(j == 3)
            {
                j = 0;
                i++;
            }
            else if(i==1 && j==1)
            {
                tile[i,j] = true;
                j++;
            }
            
        }

        
        
        
        int[][][] blocks =  { new int[][]
            {new int[]{1,0,1},new int[]{0,1,0},new int[]{1,0,1}}, new int[][]
            {new int[]{1,0,1},new int[]{0,1,0},new int[]{1,2,1}}, new int[][]
            {new int[]{1,2,1},new int[]{0,1,1},new int[]{1,2,1}}, new int[][]
            {new int[]{1,1,1},new int[]{1,1,2},new int[]{1,2,0}}, new int[][]
            {new int[]{1,0,1},new int[]{0,1,2},new int[]{1,2,1}}, new int[][]
            {new int[]{1,2,1},new int[]{2,1,2},new int[]{1,2,1}}};

        int render = 0;
        int rot = 0;
        bool found = false;
        
        for(int k = 0; k<6;k++)
        {
            if(!found)
            {
                rot =  testAllDirections(blocks[k],tile);
                if(rot == -1)
                {
                    render++;
                    rot = 0;
                }
                else
                {
                    found = true;
                }
            }
        }
        

        animator.SetInteger("render",render);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,rot));
        hitboxesFolder.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));        
    }

    private int testAllDirections(int[][] rule, bool[,] tile)
    {
        int i = 0;
        bool match = comparePattern(rule,tile);
        if(!match)
        {
            i++;
            match = comparePattern(rule,rotate(tile));
            if(!match)
            {
                i++;
                match = comparePattern(rule,rotate(rotate(tile)));
                if(!match)
                {
                    i++;
                    match = comparePattern(rule,rotate(rotate((rotate(tile)))));
                }
            }
        }
        
        if(match)
        {
            return i*90;
        }
        else
        {
            return -1;
        }
    }

    private bool[,] rotate(bool[,] m)
    {
       
        bool[,] rm = new bool[3,3];
        rm[0,0] = m[2,0];
        rm[0,1] = m[1,0];
        rm[0,2] = m[0,0];
        rm[1,0] = m[2,1];
        rm[1,1] = m[1,1];
        rm[1,2] = m[0,1];
        rm[2,0] = m[2,2];
        rm[2,1] = m[1,2];
        rm[2,2] = m[0,2];

        return rm;
    }

    private void ToString(bool[,] m)
    {
        print("-------------------");
        for(int i = 0; i<3;i++)
        {
            string row = "";
            for(int j = 0; j<3;j++)
            {
                row += m[i,j];    
            }
            print(row);
        }
        print("-------------------");
    }


    //0 == tile interdite, 1 == peu importe, 2 == tile obligatoire
    private bool comparePattern(int[][] rule, bool[,] tile)
    {
        for(int i = 0 ; i<3 ; i++){
            for(int j = 0; j<3 ; j++){
                if(tile[i,j] && rule[i][j] == 0
                || !tile[i,j] && rule[i][j] == 2){
                    return false;
                }
            }
        }
        return true;
    }

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(setSprite());
    }

   


}
