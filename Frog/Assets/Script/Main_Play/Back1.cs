using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back1 : MonoBehaviour
{    
    float Target_Offset;

    float BackImgSpeed;
    float startx = 0.0f;

    public GameObject Player;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Player.transform.position.y >= -1.93f)
        {
            BackImgSpeed = 0.2f;
            Target_Offset += Time.deltaTime * BackImgSpeed;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Target_Offset, 0);
        }
        else if (Player.transform.position.y <= -1.93f)
        {
            BackImgSpeed = 0.0f;
            Target_Offset += Time.deltaTime * BackImgSpeed;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Target_Offset, 0);
        }
        */

        if (Player.transform.position.y != -2.0f)
        {
            
            BackImgSpeed = 0.2f;
            Target_Offset += Time.deltaTime * BackImgSpeed;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Target_Offset, 0);
        }
        else if (Player.transform.position.y == -2.0f)
        {
            BackImgSpeed = 0.0f;
            Target_Offset += Time.deltaTime * BackImgSpeed;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Target_Offset, 0);
        }



        float BackPos = startx - Player.transform.position.x * BackImgSpeed;
        if (BackPos > 0.0f)
            BackPos = 0.0f;
        else if (BackPos < 0.0f)
            BackPos = 0;

        transform.position = new Vector3(Player.transform.position.x + BackPos, 0.0f, 10f);
    }
}
