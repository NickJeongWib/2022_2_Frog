using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{    
    float Target_Offset;

    public float BackImgSpeed = 0.5f;

    GameObject Player;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Target_Offset += Time.deltaTime * BackImgSpeed;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Target_Offset, 0);

    }
}
