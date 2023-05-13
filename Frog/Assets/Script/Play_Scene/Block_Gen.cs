using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Block_Gen : MonoBehaviour
{
    
    public GameObject[] Block;
    public GameObject Cube_A;
    public GameObject Cube_B;

    public float BlockSpeed = 3.0f;

    public GameObject Player;

    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Move()
    {
        Cube_A.transform.Translate(Vector3.left * BlockSpeed * Time.deltaTime);
        Cube_B.transform.Translate(Vector3.left * BlockSpeed * Time.deltaTime);
       

        if(Cube_B.transform.position.x < -5.0f)
        {
            Destroy(Cube_A);
            Cube_A = Cube_B;
            
            Make();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Make()
    {
        Cube_B = Instantiate(Cube_A, new Vector3(Cube_A.transform.position.x + 7, 0.0f, 0), transform.rotation) as GameObject;
        
    }

    void OnTriggerEnter(Collider other)
    {
       
    }

}


